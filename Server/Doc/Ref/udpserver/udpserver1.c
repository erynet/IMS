// simple UDP server - test server for "CDU-java" project
// receive packet - 

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> 		// close()
#include <string.h> 		// memset()
#include <sys/shm.h>    	// shared memory
#include <math.h>       	//
#include <time.h>       	// ctime, asctime
#include <syslog.h>
#include "../common/mem.h"
#include "cduStruct.h"
#include "udpserver.h"

int testModel = 0;
//-----------------------------------------------------
int initUDPserver();
void fill_upsTotalStructure();
void fillActiveAlarmStructure();
void fillAlarmHistoryStructure();
void convertTimeStrToStr(LogItem *logItem, char *s);
void SetTestValForUPS(int variation);

//-----------------------------------------------------
struct sockaddr_in cliAddr, servAddr;
void * shared_memory = (void *)-1;
int sd; // some socket

// --- ups data ---
J_UPS_TOTAL   upsTotal;
TRACE_ANSWER traceTable[TRACE_COUNT];


struct _ACTIVE_ALARM {
  int           requestIndex;    
  int           count;
  ALARM_ENTRY   alarm[MAX_ALARM];
} activeAlarm;

struct _ALARM_HISTORY {
  int           requestIndex;  
  int           count;
  ALARM_ENTRY   alarm[MAX_ALARM_HISTORY];
}alarmHistory;

ALARM_ANSWER      alarmHistoryAnswer;
ALARM_DEL_ANSWER  alarmDelAnswer;
TRACE_START_STOP_ANSWER   traceStartStopAnswer;

//-----------------------------------------------------
int main(int argc, char *argv[])
{
  int n, cliLen, messageCounter;
  int lengthToSend;
  void *pDataToSend;
  time_t now;
  struct tm *l1_time, l2_time;  
  UPSDATAREQUEST request;
  
    openlog("[etus][udpserver]", 0, LOG_USER);

    if (argc == 2)
    {
	testModel = atoi(argv[1]);
	printf("[udpserver] Starting test mode... test model = %d\n", testModel);
    }	

  //printf("upsTotal size = %d\n", sizeof(upsTotal));
  //printf("alarmHistory size = %d\n", sizeof(alarmHistory));
  //printf("activeAlarm size = %d\n", sizeof(activeAlarm));
  //return;
    
    // ----- init UDP server -----
    if(initUDPserver())
    {
	syslog(LOG_USER|LOG_ERR, "%s", "Server init error.");
	return 1;
    }
   	printf("init UDP server OK\n");

    // ----- init shared memory access ----
    if ((shared_memory = getSharedMem(KEY_SHARED_MEM1, SEGSIZE1)) == NULL)
    {
	syslog(LOG_USER|LOG_ERR, "%s", "Shared memory init error.");
	return 1;
    }
   	printf("getSharedMem OK\n");
    
    syslog(LOG_USER|LOG_NOTICE, "Server has started and listening on port %d... \n", LOCAL_SERVER_PORT);
    
  // ----- init to zerro ----
  memset(&upsTotal,0,sizeof(upsTotal));
  memset(&traceTable[0].trace[0],0,sizeof(traceTable[0].trace));  traceTable[0].traceIndex=0;  
  memset(&traceTable[1].trace[0],0,sizeof(traceTable[1].trace));  traceTable[1].traceIndex=1;  
  memset(&traceTable[2].trace[0],0,sizeof(traceTable[2].trace));  traceTable[2].traceIndex=2;  
  memset(&traceTable[3].trace[0],0,sizeof(traceTable[3].trace));  traceTable[3].traceIndex=3;
  memset(&traceTable[4].trace[0],0,sizeof(traceTable[4].trace));  traceTable[4].traceIndex=4;
  memset(&traceTable[5].trace[0],0,sizeof(traceTable[5].trace));  traceTable[5].traceIndex=5;    
  activeAlarm.requestIndex=0;
  activeAlarm.count=0;
  alarmHistory.requestIndex=0;
  alarmHistory.count=0;
  

  // --- set test var ---
#ifdef Debug_sim  
  SetTestValForUPS(1);
  MSG("Test variables - are setted. \n");
  // --- init time ----
  now = time((time_t *)NULL);
  l1_time = localtime(&now);
  memcpy(&l2_time,l1_time,sizeof(l2_time));
#endif  

	printf("server falls into infinite loop\n");

  // ----- server falls into infinite loop -----
    messageCounter=0;
    while(1)
    {
	// --- clear request ---
        request.requestValidator=0;
        request.requestIndex    =-1;
        // ---- receive message ----
        cliLen = sizeof(cliAddr);
        n = recvfrom(sd, (void*)&request, sizeof(request), 0, (struct sockaddr *) &cliAddr, &cliLen);
        if(n<0)
        {
    	    MSG("\n%s: cannot receive data \n",argv[0]);
            messageCounter--; // predecrement
            continue;
        }
   	printf("recvfrom OK\n");
	messageCounter++;
        // ----  print received message number ---
        MSG("\n Message from %s:UDP%u: message number: %d \n", (const char *)inet_ntoa(cliAddr.sin_addr),ntohs(cliAddr.sin_port),messageCounter);
        // --- analyse is it request for data from shared memory? ---
        if((n!=sizeof(request)) || (request.requestValidator!=getValidator))
        {
    	    MSG("It is not valid request - by size \n");                
            continue; // it is not request for data!
        }
        MSG("Message: correct request! ID=%d \n",request.requestID);
        // ---- parse request & preparing answer ----
        lengthToSend=requestParser(&request,&pDataToSend);
        if(lengthToSend<0)
	{
    	    MSG("Wrong requestID %x \n" , request.requestID);
    	    continue;
        }
   	printf("requestParser OK\n");
      // ---- send answer to requestor ----
      {
        int * pI;
        pI=(int*)pDataToSend;
        pI[0]=request.requestIndex;
        upsTotal.alarmCount=activeAlarm.count;  // current active counter
      }
      n = sendto(sd, pDataToSend, lengthToSend, 0, (struct sockaddr *) &cliAddr, sizeof(cliAddr));
      if(n<0)
        {
          MSG("%s: cannot send answer to client \n",argv[0]);
          continue; // ?
        }
   	printf("sendto OK\n");
      MSG("Data has been sent to requestor: %d bytes \n",n);
#ifdef Debug_sim       
      // --- does it time to change simulated data? ---
      now = time((time_t *)NULL);
      l1_time = localtime(&now);
      if(l1_time->tm_min != l2_time.tm_min){
        SetTestValForUPS(l1_time->tm_min);
        memcpy(&l2_time,l1_time,sizeof(l2_time));
      }
#endif      
    }// server infinite loop 

  return 0;
}


//-----------------------------------------------
// --- parse request ---
int requestParser(UPSDATAREQUEST* pRequest, void **ppData)
{
	
  switch(pRequest->requestID){
    case getUpsTotal:{
      *ppData=&upsTotal;
#ifdef Debug_sim
		printf("Test value setting\n");
	  SetTestValForUPS(1);
#else
		printf("Fill share memory value setting\n");
      fill_upsTotalStructure(); // fill upsTotal structure from shared memory
#endif
      return sizeof(upsTotal);      
    }
    
    case getUpsTrace_x:{
      if(pRequest->alarmFrom>(TRACE_COUNT-1)){
        return -1; // bad trace data number
      }
      *ppData=&traceTable[pRequest->alarmFrom];
      traceTable[pRequest->alarmFrom].traceIndex=pRequest->alarmFrom;
      return sizeof(TRACE_ANSWER);
    }

    case startUpsTrace_x:{
//      if(pRequest->alarmFrom>(TRACE_COUNT-1)){
//        return -1; // bad trace data number
//      }
      *ppData=&traceStartStopAnswer;
      traceStartStopAnswer.traceIndex=pRequest->alarmFrom;
      traceStartStopAnswer.answer=TRACE_START_OK;
      return sizeof(TRACE_START_STOP_ANSWER);
    }

    case stopUpsTrace_x:{
//      if(pRequest->alarmFrom>(TRACE_COUNT-1)){
//        return -1; // bad trace data number
//      }
      *ppData=&traceStartStopAnswer;
      traceStartStopAnswer.traceIndex=pRequest->alarmFrom;
      traceStartStopAnswer.answer=TRACE_STOP_OK;
      return sizeof(TRACE_START_STOP_ANSWER);
    }
    case getUpsActiveAlarm:{
      int length;
      //void * tmp = *ppData+4;
      *ppData=&activeAlarm;
      //tmp = &activeAlarm;
      // --- fill activeAlarm structure ---
      fillActiveAlarmStructure();      
      // --- how many to return ---
      if(0==activeAlarm.count) return 4+4; // Idex + alarm count
      length=4+4+activeAlarm.count*sizeof(ALARM_ENTRY);
      //printf("pRequest->alarmCount = %d\n", pRequest->alarmCount);
      if(activeAlarm.count > pRequest->alarmCount){
        length=4+4+pRequest->alarmCount*sizeof(ALARM_ENTRY);
      }
      return length;
    }
    case getUpsAlarmHistory: {
      int length;
      *ppData=&alarmHistoryAnswer;
      // --- fill alarm history structure ----
      fillAlarmHistoryStructure();
      // --- prepare answer ---
      alarmHistoryAnswer.alarmCount=0;  // preset
      if((0==alarmHistory.count) || (pRequest->alarmFrom >= alarmHistory.count)) return 4+4; // empty list -> Idex + alarm count
      if(pRequest->alarmCount > MAX_ALARM){
        pRequest->alarmCount=MAX_ALARM;   // max transportation size
      }
      // --- copy to return struct ---
      length=alarmHistory.count-pRequest->alarmFrom;
      if(length > pRequest->alarmCount){
        length=pRequest->alarmCount;
      }
      alarmHistoryAnswer.alarmCount=length;
      memcpy(&alarmHistoryAnswer.alarm[0],&alarmHistory.alarm[pRequest->alarmFrom],length*sizeof(ALARM_ENTRY));
      return length*sizeof(ALARM_ENTRY)+4+4;
    }

// for history circular buffer is used -> so this command doesn't supported
//    case getUpsDelHistoryAlarm: {
//      int length;
//      *ppData=&alarmDelAnswer;
//      length=alarmHistory.count - pRequest->alarmFrom;
//      if(length<=0){
//        alarmDelAnswer.alarmCount=0;
//        return sizeof(ALARM_DEL_ANSWER);   // nothing to del
//      }
//      if(length <= pRequest->alarmCount){
//        alarmHistory.count=alarmHistory.count-length;
//        alarmDelAnswer.alarmCount=length;
//        return sizeof(ALARM_DEL_ANSWER);   // del all tail
//      }
//      alarmDelAnswer.alarmCount=pRequest->alarmCount;
//      alarmHistory.count=alarmHistory.count-pRequest->alarmCount;
//      length=length-pRequest->alarmCount;
//      memmove(&alarmHistory.alarm[pRequest->alarmFrom],
//              &alarmHistory.alarm[pRequest->alarmFrom+pRequest->alarmCount],
//              (length)*sizeof(ALARM_ENTRY));
//      return sizeof(ALARM_DEL_ANSWER);   // del asked
//    }
  }
  return -1;  // bad pRequest->requestIndex
}

//-----------------------------------------------
int initUDPserver()
{
int rc;
  // --- socket creation ---
  sd=socket(AF_INET, SOCK_DGRAM, 0);
  if(sd<0){
    printf("cannot open socket \n");
    return(1);
  }
  // ------- bind local server port --------
  servAddr.sin_family = AF_INET;
  servAddr.sin_addr.s_addr = htonl(INADDR_ANY);
  servAddr.sin_port = htons(LOCAL_SERVER_PORT);
  rc = bind (sd, (struct sockaddr *) &servAddr,sizeof(servAddr));
  if(rc<0){
    printf("cannot bind port number %d \n", LOCAL_SERVER_PORT);
    return(1);
  }
  return 0;	// success
  
}


//----------------------------------------------- 

void fill_upsTotalStructure()
{
  char* ModelInfo;
  struct UPS *m = shared_memory;
  
  // ---- copy data from shared memory to answer ----
  memcpy(&upsTotal.upsBattery,&(m->upsobjects.upsbattery),sizeof(upsTotal.upsBattery));

  printf("upsBattery upsEstimatedChargeRemaining %d\n", upsTotal.upsBattery.upsEstimatedChargeRemaining);
  printf("upsBattery upsBatteryVoltage %d\n", upsTotal.upsBattery.upsBatteryVoltage);
  printf("upsBattery upsBatteryCurrent %d\n", upsTotal.upsBattery.upsBatteryCurrent);
  printf("upsBattery upsSecondsOnBattery %d\n", upsTotal.upsBattery.upsSecondsOnBattery);
  printf("upsBattery eti_DCLinkVoltage %d\n", upsTotal.upsBattery.eti_DCLinkVoltage);
  printf("upsBattery upsEstimatedMinutesRemaining %d\n", upsTotal.upsBattery.upsEstimatedMinutesRemaining);

  memcpy(&upsTotal.upsInput,&(m->upsobjects.upsinput),sizeof(upsTotal.upsInput));
  memcpy(&upsTotal.upsOutput,&(m->upsobjects.upsoutput),sizeof(upsTotal.upsOutput));
  memcpy(&upsTotal.upsBypass,&(m->upsobjects.upsbypass),sizeof(upsTotal.upsBypass));
  memcpy(&upsTotal.upsInverter,&(m->upsobjects.upsinverter),sizeof(upsTotal.upsInverter));


  upsTotal.upsMimicState.BatteryInput = m->upsobjects.upsled.LEDBattery;
  upsTotal.upsMimicState.MainInput    = m->upsobjects.upsled.LEDInput;
  upsTotal.upsMimicState.BaypassInput = m->upsobjects.upsled.LEDBypass;

  //upsTotal.upsMimicState.CB1    = m->upsobjects.upscbmc.CBMCCB1;
  upsTotal.upsMimicState.CB4    = m->upsobjects.upscbmc.CBMCCB4;
//  upsTotal.upsMimicState.CB4A   = m->upsobjects.upscbmc.CBMCCB4a;
  upsTotal.upsMimicState.MC1    = m->upsobjects.upscbmc.CBMCMC1;
  upsTotal.upsMimicState.MC1A   = m->upsobjects.upscbmc.CBMCMC1a;
  upsTotal.upsMimicState.MC2    = m->upsobjects.upscbmc.CBMCMC2;
  //upsTotal.upsMimicState.MC2A   = m->upsobjects.upscbmc.CBMCMC2a;
  upsTotal.upsMimicState.MC3    = m->upsobjects.upscbmc.ISSU;
  upsTotal.upsMimicState.MC5    = m->upsobjects.upscbmc.CBMCCB2;

  upsTotal.upsMimicState.InverterOperation  = m->upsobjects.upsled.LEDInverterSupply;
  upsTotal.upsMimicState.BypassOperation    = m->upsobjects.upsled.LEDBypassSupply;
  upsTotal.upsMimicState.MBSOperation       = m->upsobjects.upsled.LEDMaintenanceSupply;

  upsTotal.upsMimicState.AC_DC  = m->upsobjects.upsled.LEDRectifier;
  upsTotal.upsMimicState.DC_AC  = m->upsobjects.upsled.LEDInverter;

  upsTotal.upsMimicState.Fault        = m->upsobjects.upsled.LEDFault;
  upsTotal.upsMimicState.Synchronous  = m->upsobjects.upsled.LEDSync;

  upsTotal.outputLoadTotal  = m->upsobjects.upsled.LEDLoadOutputPercent;
  
  /* checking ETUS Model */
  
  ModelInfo = (char*)m->upsobjects.upsident.upsIdentModel;
      
  if (testModel == 0)
  {  
    if(!strncmp(ModelInfo, "ETUS(M)", 7))
	upsTotal.systemInfo = 5;	
    else if(!strncmp(ModelInfo, "ETUS(KT)", 8))
	upsTotal.systemInfo = 4;
    else if(!strncmp(ModelInfo, "ETUS31", 6))
       upsTotal.systemInfo = 3;
    else if(!strncmp(ModelInfo, "ETUS II", 7))
       upsTotal.systemInfo = 2;
    else if(!strncmp(ModelInfo, "ETUS", 4))
       upsTotal.systemInfo = 1;
    else
       upsTotal.systemInfo = 1;
  }
  else upsTotal.systemInfo = testModel;
  
  //printf(" \n System Info: %d \n", upsTotal.systemInfo);
					   
}

//----------------------------------------------- 
void fillActiveAlarmStructure()
{
  struct UPS *m = shared_memory;
  int i;

  // --- take number ---
  activeAlarm.count=m->upsobjects.upsfault.numOfFlts;
  if(MAX_ALARM<activeAlarm.count){
    activeAlarm.count=MAX_ALARM;
  }

    //------------ TEST --------------------
    //printf("\n \n Active alarm count: %d \n",activeAlarm.count);
    //------------------------------------------------------

    
  // --- fill ---
  for(i=0;i<activeAlarm.count;i++){

//    //------------ TEST --------------------
    //printf("No:%d ID:%d  \n",i ,m->upsobjects.upsfault.fltBits[i]);
//    //------------------------------------------------------
    
    activeAlarm.alarm[i].alarmTime[0] =0; // any way this is not used - so just for clearence
    activeAlarm.alarm[i].alarmID=m->upsobjects.upsfault.fltBits[i];
    //printf("No:%d ID:%d  \n",i ,activeAlarm.alarm[i].alarmID);
  }
}

//-----------------------------------------------
void fillAlarmHistoryStructure()
{
  struct UPS *m = shared_memory;
  int i,j;


  //------------ TEST --------------------
  //printf("\n \n alarm History -> iHead:%d: \n",m->upsobjects.upslog.iHead);
  //------------------------------------------------------

        
  alarmHistory.count=m->upsobjects.upslog.iHead;
  if(alarmHistory.count <= 0) return;

  j=0;  
  //----- copy newest patr -----
  for(i=0; i<=alarmHistory.count-1;i++,j++){
//    //------------ TEST --------------------
//    printf("No:%d ID:%d Time:%s date:%s time_t:%s\n",i,m->upsobjects.upslog.item[i].faultID
//                                                    ,m->upsobjects.upslog.item[i].time
//                                                    ,m->upsobjects.upslog.item[i].date
//						    ,0);
                                                    //,ctime(&(alarmHistory.alarm[i].alarmTime)));
//    //------------------------------------------------------
//    alarmHistory.alarm[i].alarmTime=convertTimeToTime_t(&m->upsobjects.upslog.item[i]);

    convertTimeStrToStr(&m->upsobjects.upslog.item[i], alarmHistory.alarm[j].alarmTime); // src,dst
    alarmHistory.alarm[i].alarmID=m->upsobjects.upslog.item[j].faultID;
  }

  //---- copy oldest part if pesent ----
  if(alarmHistory.count>=MAX_LOG){ return;}
  if(m->upsobjects.upslog.item[alarmHistory.count].faultID==0){ return;}
  for(i=MAX_LOG-1;i>=alarmHistory.count;i--,j++){
//    //------------ TEST --------------------
//    printf("No:%d ID:%d Time:%s date:%s time_t:%s",i,m->upsobjects.upslog.item[i].faultID
//                                                    ,m->upsobjects.upslog.item[i].time
//                                                    ,m->upsobjects.upslog.item[i].date
//                                                    ,ctime(&(alarmHistory.alarm[i].alarmTime)));
//    //------------------------------------------------------
//    alarmHistory.alarm[i].alarmTime=convertTimeToTime_t(&m->upsobjects.upslog.item[i]);

    convertTimeStrToStr(&m->upsobjects.upslog.item[i], alarmHistory.alarm[j].alarmTime);
    alarmHistory.alarm[i].alarmID=m->upsobjects.upslog.item[j].faultID;
  }
  alarmHistory.count=MAX_LOG; //we have full list of alarms
}

//--------------------------------------------------
// expected 19+1 bytes array for result
void convertTimeStrToStr(LogItem *logItem, char *s)
{
  memset(s,0,19+1);

  memcpy(&s[0],&logItem->date[0],4);  // YYYY
  s[4]='-';
  memcpy(&s[5],&logItem->date[4],2);  // MM
  s[7]='-';
  memcpy(&s[8],&logItem->date[6],2);  // DD
  s[10]=' ';

  memcpy(&s[11],&logItem->time[0],2); // HH
  s[13]=':';
  memcpy(&s[14],&logItem->time[2],2); // MM
  s[16]=':';
  memcpy(&s[17],&logItem->time[4],2); // SS
}


////--------------------------------------------------
//time_t convertTimeToTime_t(LogItem *logItem)
//{
//  time_t result;
//  struct tm tm_time;
//  char tmp[5];
//
//
//  memset(tmp,0,sizeof(tmp));
//
//  memcpy(tmp,&logItem->time[4],2);
//  tm_time.tm_sec=atoi(tmp);
//
//  memcpy(tmp,&logItem->time[2],2);
//  tm_time.tm_min=atoi(tmp);
//
//  memcpy(tmp,&logItem->time[0],2);
//  tm_time.tm_hour=atoi(tmp);
//
//  memcpy(tmp,&logItem->date[6],2);
//  tm_time.tm_mday=atoi(tmp);
//
//  memcpy(tmp,&logItem->date[4],2);
//  tm_time.tm_mon=atoi(tmp);
//
//  memcpy(tmp,&logItem->date[0],4);
//  tm_time.tm_year=atoi(tmp)-1900;
//
//  result=mktime(&tm_time);
//
////  //------------ TEST --------------------
////  printf("tm_time:%s time_t:%s \n",asctime(&tm_time),ctime(&result));
////  //------------------------------------------------------
//
//
//  if(result==-1){
//    result=0;
//  }
//
//  return(result);
//
//}


//===================================================
// only for TEST
#ifdef Debug_sim
void SetTestValForUPS(int variation)
{
  int i;
  // ------ UPS Total -------------

  // --- MIMIC group ---
#if 1
  upsTotal.upsMimicState.BatteryInput = 1;
  upsTotal.upsMimicState.MainInput    = 1;
  upsTotal.upsMimicState.BaypassInput = 1;

  upsTotal.upsMimicState.CB1    = 1;
  upsTotal.upsMimicState.CB4    = 1;
  upsTotal.upsMimicState.CB4A   = 1;
  upsTotal.upsMimicState.MC1    = 1;
  upsTotal.upsMimicState.MC1A   = 1;
  upsTotal.upsMimicState.MC2    = 1;
  upsTotal.upsMimicState.MC2A   = 1;
  upsTotal.upsMimicState.MC3    = 1;
  upsTotal.upsMimicState.MC5    = 1;
  upsTotal.upsMimicState.AC_DC  = 1;
  upsTotal.upsMimicState.DC_AC  = 1;

  upsTotal.upsMimicState.Fault        = 3;
  upsTotal.upsMimicState.Synchronous  = 1;
#else
  upsTotal.upsMimicState.BatteryInput = 1 && (variation & 0x36);
  upsTotal.upsMimicState.MainInput    = 1 && (variation & 0x27);
  upsTotal.upsMimicState.BaypassInput = 1 && (variation & 0x05);

  upsTotal.upsMimicState.CB1    = 1 && (variation & 0x0f);
  upsTotal.upsMimicState.CB4    = 1 && (variation & 0x01);
  upsTotal.upsMimicState.CB4A   = 1 && (variation & 0x03);
  upsTotal.upsMimicState.MC1    = 1 && (variation & 0x01);
  upsTotal.upsMimicState.MC1A   = 1 && (variation & 0x10);
  upsTotal.upsMimicState.MC2    = 1 && (variation & 0x20);
  upsTotal.upsMimicState.MC2A   = 1 && (variation & 0x03);
  upsTotal.upsMimicState.MC3    = 1 && (variation & 0x06);
  upsTotal.upsMimicState.MC5    = 1 && (variation & 0x01);
  upsTotal.upsMimicState.AC_DC  = 1 && (variation & 0x0c);
  upsTotal.upsMimicState.DC_AC  = 1 && (variation & 0x07);

  upsTotal.upsMimicState.Fault        = (variation & 0x01)+(variation && 0x02);
  upsTotal.upsMimicState.Synchronous  = (variation & 0x01);
#endif

  // --- Battery group ---
  upsTotal.upsBattery.eti_batteryCondition=0;
  upsTotal.upsBattery.eti_batteryCharge=40+variation;
  upsTotal.upsBattery.eti_DCLinkVoltage=4449+variation;
  upsTotal.upsBattery.upsBatteryStatus=0;
  upsTotal.upsBattery.upsSecondsOnBattery=2222+variation;
  upsTotal.upsBattery.upsEstimatedMinutesRemaining=59+variation;
  upsTotal.upsBattery.upsEstimatedChargeRemaining=40+variation;
  upsTotal.upsBattery.upsBatteryVoltage=4500+variation;
  upsTotal.upsBattery.upsBatteryCurrent=0;
  //upsTotal.upsBattery.upsBatteryTemperature=36+variation;				//141020


//  // --- Input Group ---
  upsTotal.upsInput.upsInputLineBads=0;
  upsTotal.upsInput.upsInputNumLines=3;
  for(i=0;i<3;i++)
    {
      upsTotal.upsInput.upsInputEntry[i].upsInputFrequency=500+variation;
      upsTotal.upsInput.upsInputEntry[i].upsInputVoltage=2080+variation;
      upsTotal.upsInput.upsInputEntry[i].upsInputCurrent=250+variation;
      upsTotal.upsInput.upsInputEntry[i].upsInputTruePower=500+variation;
    }

//  // --- Load Output Group ---
  upsTotal.upsOutput.upsOutputSource=0;
  upsTotal.upsOutput.upsOutputNumLines=3;
  for(i=0;i<3;i++)
    {
      upsTotal.upsOutput.upsOutputEntry[i].upsOutputFrequency=500+variation;
      upsTotal.upsOutput.upsOutputEntry[i].upsOutputVoltage=1270+variation;
      upsTotal.upsOutput.upsOutputEntry[i].upsOutputCurrent=1000+variation;
//    upsTotal.upsOutput.upsOutputEntry[i].eti_loadOutputCurrentPeak=1100+variation;//141020
      upsTotal.upsOutput.upsOutputEntry[i].upsOutputPower=500+variation;
      upsTotal.upsOutput.upsOutputEntry[i].upsOutputPercentLoad=60+variation;
    }


  // --- Bypass Group ---
  upsTotal.upsBypass.upsBypassNumLines=3;
  for(i=0;i<3;i++)
    {
      upsTotal.upsBypass.upsBypassEntry[i].upsBypassFrequency=500+variation;
      upsTotal.upsBypass.upsBypassEntry[i].upsBypassVoltage=2080+variation;
      upsTotal.upsBypass.upsBypassEntry[i].upsBypassCurrent=250+variation;
      upsTotal.upsBypass.upsBypassEntry[i].upsBypassPower=500+variation;
    }


  // --- upsInverter ---
  upsTotal.upsInverter.eti_inverterIGBTPack1Temp=45+variation;
  upsTotal.upsInverter.eti_inverterIGBTPack2Temp=50+variation;
  upsTotal.upsInverter.eti_inverterIGBTPack3Temp=70+variation;
  upsTotal.upsInverter.eti_inverterIGBTPack4Temp=80+variation;
  upsTotal.upsInverter.eti_inverterIGBTPack5Temp=100+variation;
  upsTotal.upsInverter.eti_inverterIGBTPack6Temp=140+variation;
  for(i=0;i<3;i++)
    {
      upsTotal.upsInverter.eti_inverterOutputEntry[i].eti_inverterOutputCurrent=100+variation;
      upsTotal.upsInverter.eti_inverterOutputEntry[i].eti_inverterOutputFrequency=500+variation;
      upsTotal.upsInverter.eti_inverterOutputEntry[i].eti_inverterOutputVoltage=1270+variation;
    }


  // --- trace ----
  for(i=0;i<TRACE_COUNT;i++){
    traceTable[i].trace[i]=-100+variation;//sin(2*3.1415*i*2/200+00);
    traceTable[i].trace[i]=-50+variation; //sin(2*3.1415*i*2/200+20);
    traceTable[i].trace[i]=-0+variation;  //sin(2*3.1415*i*2/200+40);
    traceTable[i].trace[i]=50+variation;  //sin(2*3.1415*i*2/200+60);
    traceTable[i].trace[i]=100+variation; //sin(2*3.1415*i*2/200+80);
    traceTable[i].trace[i]=150+variation; //sin(2*3.1415*i*2/200+100);
  }

  // ---- alarm ----
  {
    time_t now;
    // --  generate ID --
    int id;
    id=0x10 + (variation & 0x07); // only 8 id possible
    // -- loock in active alarm table --
    for(i=0;i<activeAlarm.count; i++){
      if(activeAlarm.alarm[i].alarmID==id){
        break;
      }
    }
    if(i>=activeAlarm.count){
      // --- no such id in list -> put in list ---
      activeAlarm.alarm[activeAlarm.count].alarmID=id;
      activeAlarm.count++;
      // --- and put it in history list ---
      if(alarmHistory.count<MAX_ALARM_HISTORY){
        // there is place in alarmHistory list
        now=time((time_t *)NULL);
        alarmHistory.alarm[alarmHistory.count].alarmID=id;
   //hckim     alarmHistory.alarm[alarmHistory.count].alarmTime=now;
        alarmHistory.count++;
      }
    }
    else{
      // --- need del from active alarm ---
      activeAlarm.count--;
      memmove(&activeAlarm.alarm[i],&activeAlarm.alarm[i+1],(activeAlarm.count-i)*sizeof(ALARM_ENTRY));
    }

  }

}
#endif


