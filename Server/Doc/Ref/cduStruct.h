/***************************************************************************
                          cduStruct.h  -  description
                             -------------------
    begin                : Sun Oct 12 2003
    copyright            : (C) 2003 by root
    email                : grigory@eti21.com
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 ***************************************************************************/

// definition of data structures for requests & answers
#define TABLE_SIZE          3
#define TRACE_COUNT         6
#define TRACE_SIZE          200
#define MAX_ALARM           1000
#define MAX_ALARM_HISTORY   10*MAX_ALARM


//typedef unsigned char   DisplayString[MAX_LEN];
//typedef unsigned char   byte;
//typedef int             Integer;
//typedef long            Long;
   
//-----------------------------------------
// ------ Requests -----------
#define getValidator        (0x55441166)

#define getUpsTotal           (0x00000000)
#define getUpsActiveAlarm     (0x00000001)
#define getUpsAlarmHistory    (0x00000002)
#define getUpsDelHistoryAlarm (0x00000003)

#define startUpsTrace_x       (0x00000004)
#define stopUpsTrace_x        (0x00000005)
#define getUpsTrace_x         (0x00000006)

#define setTestMimicState     (0x00000101)


typedef struct _UPSDATAREQUEST{
  Integer   requestValidator;
  Integer   requestIndex;
  Integer   requestID;    
  Integer   alarmFrom;
  Integer   alarmCount;
} UPSDATAREQUEST,*pUPSDATAREQUEST;

// --------- Mimic test ---------------
#define mtvBatteryInput   0x00000001
#define mtvBatteryInput_2 0x00000002
#define mtvMainInput      0x00000004
#define mtvMainInput_2    0x00000008
#define mtvBaypassInput   0x00000010
#define mtvBaypassInput_2 0x00000020

#define mtvCB1            0x00000040
#define mtvCB4            0x00000080
#define mtvCB4A           0x00000100
#define mtvMC1            0x00000200
#define mtvMC1A           0x00000400
#define mtvMC2            0x00000800
#define mtvMC2A           0x00001000
#define mtvMC3            0x00002000
#define mtvMC5            0x00004000
#define mtvAC_DC          0x00008000
#define mtvDC_AC          0x00010000

#define mtvInvOp          0x00020000
#define mtvBypOp          0x00040000
#define mtvMBSOp          0x00080000



// --------- Answers ---------------
//----------------------------------------
//getUpsTotal

typedef struct _MIMIC_STATE
{
  // ---- state of switchs -----
  Integer		BatteryInput;
  Integer		MainInput;
  Integer		BaypassInput;  
      
  Integer		MC1;
  Integer		MC1A;
  Integer		MC2;
  Integer		MC2A;
  Integer		MC3;
  Integer		MC5;
  Integer		CB1;
  Integer		CB4;
  Integer		CB4A;

  Integer		AC_DC;
  Integer		DC_AC;

  Integer		Synchronous;
  Integer		Fault;

  Integer		InverterOperation;
  Integer		BypassOperation;
  Integer		MBSOperation;  
  
} J_MIMIC_STATE, * pMIMIC_STATE;

#define MIMIC_SWITCH_OFF  0
#define MIMIC_SWITCH_ON   1
#define MIMIC_SYNC_OK     1
#define MIMIC_FAULT_NO    0
#define MIMIC_FAULT_1     1
#define MIMIC_FAULT_2     2

typedef struct _UPSBATTERY
{
  Integer		upsBatteryStatus;
  Integer		upsSecondsOnBattery;			    // UNITS : seconds
  Integer		upsEstimatedMinutesRemaining;	// UNITS : minutes
  Integer		upsEstimatedChargeRemaining;	// UNITS : percent
  Integer		upsBatteryVoltage;		        // UNITS : 0.1 Volt DC
  Integer		upsBatteryCurrent;		        // UNITS : 0.1 Amp DC
  Integer		eti_batteryCondition;
  Integer		eti_batteryCharge;
  Integer		eti_DCLinkVoltage;
} J_UPSBATTERY,*pUPSBATTERY;


typedef struct _UPSINPUTTABLE
{
  Integer		upsInputFrequency;		// UNITS : 0.1 Hertz
  Integer		upsInputVoltage;		  // UNITS : RMS Voltage
  Integer		upsInputCurrent;		  // UNITS : 0.1 RMS Amp
  Integer		upsInputTruePower;		// UNITS : Watts
} J_UPSINPUTTABLE,*pUPSINPUTTABLE;
typedef struct _UPSINPUT
{
  Integer			upsInputLineBads;
  Integer			upsInputNumLines;
/*  Integer		upsInputFrequency1;		// UNITS : 0.1 Hertz
  Integer		upsInputVoltage1;		  // UNITS : RMS Voltage
  Integer		upsInputCurrent1;		  // UNITS : 0.1 RMS Amp
  Integer		upsInputTruePower1;		// UNITS : Watts
  Integer		upsInputFrequency2;		// UNITS : 0.1 Hertz
  Integer		upsInputVoltage2;		  // UNITS : RMS Voltage
  Integer		upsInputCurrent2;		  // UNITS : 0.1 RMS Amp
  Integer		upsInputTruePower2;		// UNITS : Watts
  Integer		upsInputFrequency3;		// UNITS : 0.1 Hertz
  Integer		upsInputVoltage3;		  // UNITS : RMS Voltage
  Integer		upsInputCurrent3;		  // UNITS : 0.1 RMS Amp
  Integer		upsInputTruePower3;		// UNITS : Watts*/
  J_UPSINPUTTABLE	upsInputEntry[TABLE_SIZE-1];
  Integer mainVoltage1;
  Integer mainVoltage2;
  Integer mainVoltage3;
  Integer ConVdcP;
  Integer ConVdcN;
  Integer dcdcL1Current;
  Integer dcdcL2Current;
			
} J_UPSINPUT,*pUPSINPUT;


typedef struct _UPSOUTPUTTABLE
{
  Integer		upsOutputVoltage;			      // UNITS : RMS Voltage
  Integer		upsOutputCurrent;			      // UNITS : 0.1 RMS Amp
  Integer		upsOutputPower;				      // UNITS : Watts
  Integer		upsOutputPercentLoad;		    // UNITS : percent
  Integer		upsOutputFrequency;			    // UNITS : 0.1 Hertz
} J_UPSOUTPUTTABLE,*pUPSOUTPUTTABLE;
typedef struct _UPSOUTPUT
{
  Integer		upsOutputSource;
  Integer		upsOutputNumLines;
  Integer		upsOutputVoltage1;			      // UNITS : RMS Voltage
  Integer		upsOutputCurrent1;			      // UNITS : 0.1 RMS Amp
  Integer		upsOutputPower1;				      // UNITS : Watts
  Integer		upsOutputPercentLoad1;		    // UNITS : percent
  Integer		upsOutputFrequency1;			    // UNITS : 0.1 Hertz   
  Integer		upsOutputVoltage2;			      // UNITS : RMS Voltage
  Integer		upsOutputCurrent2;			      // UNITS : 0.1 RMS Amp
  Integer		upsOutputPower2;				      // UNITS : Watts
  Integer		upsOutputPercentLoad2;		    // UNITS : percent
  Integer		upsOutputFrequency2;			    // UNITS : 0.1 Hertz  
  Integer		upsOutputVoltage3;			      // UNITS : RMS Voltage
  Integer		upsOutputCurrent3;			      // UNITS : 0.1 RMS Amp
  Integer		upsOutputPower3;				      // UNITS : Watts
  Integer		upsOutputPercentLoad3;		    // UNITS : percent
  Integer		upsOutputFrequency3;			    // UNITS : 0.1 Hertz
//  J_UPSOUTPUTTABLE	upsOutputEntry[TABLE_SIZE-1];
} J_UPSOUTPUT,*pUPSOUTPUT;


typedef struct _UPSBYPASSTABLE
{
  Integer		upsBypassVoltage;		// UNITS : RMS Voltage
  Integer		upsBypassCurrent;		// UNITS : 0.1 RMS Amp
  Integer		upsBypassPower;			// UNITS : Watts
  Integer		upsBypassFrequency;	// UNITS : 0.1 Hertz
} J_UPSBYPASSTABLE,*pUPSBYPASSTABLE;
typedef struct _UPSBYPASS
{
  Integer			    upsBypassNumLines;
  Integer		upsBypassVoltage1;		// UNITS : RMS Voltage
  Integer		upsBypassCurrent1;		// UNITS : 0.1 RMS Amp
  Integer		upsBypassPower1;			// UNITS : Watts
  Integer		upsBypassFrequency1;	// UNITS : 0.1 Hertz
  Integer		upsBypassVoltage2;		// UNITS : RMS Voltage
  Integer		upsBypassCurrent2;		// UNITS : 0.1 RMS Amp
  Integer		upsBypassPower2;			// UNITS : Watts
  Integer		upsBypassFrequency2;	// UNITS : 0.1 Hertz
  Integer		upsBypassVoltage3;		// UNITS : RMS Voltage
  Integer		upsBypassCurrent3;		// UNITS : 0.1 RMS Amp
  Integer		upsBypassPower3;			// UNITS : Watts
  Integer		upsBypassFrequency3;	// UNITS : 0.1 Hertz

//  J_UPSBYPASSTABLE	upsBypassEntry[TABLE_SIZE-1];
} J_UPSBYPASS,*pUPSBYPASS;



typedef struct _UPSINVERTERTABLE
{
  Integer		eti_inverterOutputFrequency;
  Integer		eti_inverterOutputVoltage;
  Integer		eti_inverterOutputCurrent;
} J_UPSINVERTERTABLE,*pUPSINVERTERTABLE;

typedef struct _UPSINVERTER
{
//  J_UPSINVERTERTABLE eti_inverterOutputEntry[TABLE_SIZE-1];
  Integer		eti_inverterOutputFrequency1;
  Integer		eti_inverterOutputVoltage1;
  Integer		eti_inverterOutputCurrent1;
  Integer		eti_inverterOutputFrequency2;
  Integer		eti_inverterOutputVoltage2;
  Integer		eti_inverterOutputCurrent2;
  Integer		eti_inverterOutputFrequency3;
  Integer		eti_inverterOutputVoltage3;
  Integer		eti_inverterOutputCurrent3;
  Integer nCurrent;						//	10	//141020
  Integer inverterVdcP;
  Integer inverterVdcN;
  Integer		eti_ConverterTemp;
  Integer		eti_InverterTemp;
  Integer		eti_BatteryTemp;
  Integer		eti_DCDCTemp;
  Integer		eti_ISSUTemp;
  Integer		eti_InnerTemp;
} J_UPSINVERTER,*pUPSINVERTER;

typedef struct _UPS_TOTAL
  {
    Integer         requestIndex;      
    J_MIMIC_STATE   upsMimicState;
    J_UPSBATTERY    upsBattery;
    J_UPSINPUT      upsInput;
    J_UPSOUTPUT     upsOutput;
    J_UPSBYPASS     upsBypass;
    J_UPSINVERTER   upsInverter;
    Integer         outputLoadTotal;
    Integer         alarmCount;
    Integer	    systemInfo;
  } J_UPS_TOTAL,*p_UPS_TOTAL;


//----------------------------------------
//getUpsTrace_x
typedef struct _TRACE_ANSWER{
  Integer       requestIndex;
  Integer       traceIndex;  
  double        trace[TRACE_SIZE];
}TRACE_ANSWER, *pTRACE_ANSWER;

#define TRACE_START_OK    0x00000000
#define TRACE_STOP_OK     0x00000001

typedef struct _TRACE_START_STOP_ANSWER{
  Integer       requestIndex;
  Integer       traceIndex;    
  Integer       answer;
}TRACE_START_STOP_ANSWER, *pTRACE_START_STOP_ANSWER;

//----------------------------------------
// getUpsAlarm

// time_t - 32 bits?
typedef struct _ALARM_ENTRY {
  //time_t      alarmTime;  // switched to use string instead of time
  char        alarmTime[19+1];  // YYYY-MM-DD HH:MM:SS
  Integer     alarmID;
} ALARM_ENTRY, *pALARM_ENTRY;

typedef struct _ALARM_ANSWER {
  Integer       requestIndex;        
  Integer       alarmCount;
  ALARM_ENTRY   alarm[MAX_ALARM];
}ALARM_ANSWER, *pALARM_ANSWER;

typedef struct _ALARM_DEL_ANSWER {
  Integer       requestIndex;
  Integer       alarmCount;
}ALARM_DEL_ANSWER, *pALARM_DEL_ANSWER;



