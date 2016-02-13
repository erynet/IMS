#include <sys/types.h>
//#include "log.h"

#ifndef _eti_Struct_
#define _eti_Struct_

#define MAX_LEN	100
#define TABLE_SIZE	3
#define MAX_ALARM_TABLE_SIZE	24
#define TOTALFLTSTSBITS 	150
#define MAX_LOG		50 
typedef unsigned char	DisplayString[MAX_LEN];
typedef unsigned char byte;
typedef int	Integer;
typedef long	Long;

/////////////////////////////////////////////////
///////
///////   upsIdent
///////
/////////////////////////////////////////////////

typedef struct upsIdent
{
	DisplayString	upsIdentManufacturer;
	DisplayString	upsIdentModel;
	DisplayString	upsIdentUPSSoftwareVersion;
	DisplayString	upsIdentAgentSoftwareVersion;
	DisplayString	upsIdentName;
	DisplayString	upsIdentAttachedDevices;
} UPSIDENT;

/////////////////////////////////////////////////
////////
////////  upsBattery
////////
/////////////////////////////////////////////////

typedef struct upsBattery
{
	Integer	upsBatteryStatus;
			// BatteryStatus
			// 1 --> unknown
			// 2 --> batteryNormal
			// 3 --> batteryLow
			// 4 --> batteryDepleted
	Integer	upsSecondsOnBattery;			// UNITS : seconds
	Integer	upsEstimatedMinutesRemaining;   // UNITS : minutes
	Integer	upsEstimatedChargeRemaining;	// UNITS : percent
	Integer	upsBatteryVoltage;				// UNITS : 0.1 Volt DC
	Integer	upsBatteryCurrent;				// UNITS : 0.1 Amp DC
	Integer	upsBatteryTemperature;			// UNITS : degress Centigrade
	Integer	eti_batteryCondition;				// CDP 추가변수
	Integer	eti_batteryCharge;				// CDP 추가변수
	Integer	eti_DCLinkVoltage;				// CDP 추가변수
} UPSBATTERY;

/////////////////////////////////////////////////
////////
////////  upsInput
////////
/////////////////////////////////////////////////

typedef struct upsInputTable
{
	Integer	upsInputFrequency;	// UNITS : 0.1 Hertz
	Integer	upsInputVoltage;	// UNITS : RMS Voltage
	Integer	upsInputCurrent;	// UNITS : 0.1 RMS Amp
	Integer	upsInputTruePower;	// UNITS : Watts
} UPSINPUTTABLE;

typedef struct upsInput
{
	Integer			upsInputLineBads;
	Integer			upsInputNumLines;
	UPSINPUTTABLE	upsInputEntry[TABLE_SIZE];
} UPSINPUT;

/////////////////////////////////////////////////
/////////
/////////  upsOutput
/////////
/////////////////////////////////////////////////

typedef struct upsOutputTable
{
	Integer	upsOutputVoltage;			// UNITS : RMS Voltage
	Integer	upsOutputCurrent;			// UNITS : 0.1 RMS Amp
	Integer	upsOutputPower;				// UNITS : Watts
	Integer	upsOutputPercentLoad;		// UNITS : percent
	Integer	upsOutputFrequency;		// UNITS : 0.1 Hertz
	Integer	eti_loadOutputCurrentPeak;	// CDP 추가변수
} UPSOUTPUTTABLE;

typedef struct upsOutput
{
	Integer			upsOutputSource;
					// OutputSource
					// 1 --> other
					// 2 --> none
					// 3 --> normal
					// 4 --> bypass
					// 5 --> battery
					// 6 --> booster
					// 7 --> reducer
	Integer			upsOutputNumLines;
	UPSOUTPUTTABLE	upsOutputEntry[TABLE_SIZE];
} UPSOUTPUT;

/////////////////////////////////////////////////
////////
////////  upsBypass
////////
/////////////////////////////////////////////////

typedef struct upsBypassTable
{
	Integer	upsBypassVoltage;		// UNITS : RMS Voltage
	Integer	upsBypassCurrent;		// UNITS : 0.1 RMS Amp
	Integer	upsBypassPower;		// UNITS : Watts
	Integer	upsBypassFrequency;	// UNITS : 0.1 Hertz
} UPSBYPASSTABLE;

typedef struct upsBypass
{
	Integer			upsBypassNumLines;
	UPSBYPASSTABLE	upsBypassEntry[TABLE_SIZE];
} UPSBYPASS;

/////////////////////////////////////////////////
/////////
/////////  upsInverterOutput -- CDP 추가그룹
/////////
/////////////////////////////////////////////////
// siva
/*
typedef struct upsInverterTable
{
	Integer	eti_inverterOutputFrequency;
	Integer	eti_inverterOutputVoltage;
	Integer	eti_inverterOutputCurrent;
	Integer	eti_inverterOutputPower;
} UPSINVERTERTABLE;

typedef struct upsInverter
{
	Integer				eti_inverterOutputNumLines;
	UPSINVERTERTABLE	eti_inverterOutputEntry[TABLE_SIZE];
} UPSINVERTER;
*/

//siva new
typedef struct upsInverterTable
{
	Integer	eti_inverterOutputFrequency;
	Integer	eti_inverterOutputVoltage;
	Integer	eti_inverterOutputCurrent;
} UPSINVERTERTABLE;

typedef struct upsInverter
{
	UPSINVERTERTABLE	eti_inverterOutputEntry[TABLE_SIZE];
	Integer				eti_inverterIGBTPack1Temp;
	Integer				eti_inverterIGBTPack2Temp;
	Integer				eti_inverterIGBTPack3Temp;
	Integer				eti_inverterIGBTPack4Temp;
	Integer				eti_inverterIGBTPack5Temp;
	Integer				eti_inverterIGBTPack6Temp;
} UPSINVERTER;

// new
/////////////////////////////////////////////////
////////
////////  upsAlarm
////////
/////////////////////////////////////////////////

typedef struct upsAlarmTable
{
	Integer		upsAlarmDescr;
				// 현재 알람이 발생한 No를 이곳에 입력시킨다.
				// upsAlarmBatteryBad 		: 1
				// upsAlarmOnBattery  		: 2
				// upsAlarmLowBattery 		: 3
				// upsAlarmDepletedBattery 	: 4
				// upsAlarmTempBad 		: 5
				// upsAlarmInputBad 		: 6
				// upsAlarmOutputBad 		: 7
				// upsAlarmOutputOverload 	: 8
				// upsAlarmOnBypass 		: 9
				// upsAlarmBypassBad 		: 10
				// upsAlarmOutputOffAsRequested : 11
				// upsAlarmUpsOffAsRequested 	: 12
				// upsAlarmChargerFailed 	: 13
				// upsAlarmUpsOutputOff 	: 14
				// upsAlarmSystemOff 		: 15
				// upsAlarmFanFailure		: 16
				// upsAlarmFuseFailure 		: 17
				// upsAlarmGeneralFault 	: 18
				// upsAlarmDiagnosticTestFailed : 19
				// upsAlarmCommunicationLost 	: 20
				// upsAlarmAwaitingPower 	: 21
				// upsAlarmShutdownPending 	: 22
				// upsAlarmShutdownImminent 	: 23
				// upsAlarmTestInprogress 	: 24
	DisplayString	upsAlarmTime;
} UPSALARMTABLE;

typedef struct upsAlarm
{
	Long				upsAlarmsPresent;
					// 알람이 발생한 갯수를 입력시킨다.
	UPSALARMTABLE	upsAlarmEntry[MAX_ALARM_TABLE_SIZE];
					// Alarm Table Size 는 upsAlarmPresent에 의하여 수시로 변한다.
} UPSALARM;

/////////////////////////////////////////////////
/////////
/////////  upsTest
/////////
/////////////////////////////////////////////////

typedef struct upsTest
{
	DisplayString	upsTestId;
	Integer		upsTestSpinLock;
	Integer		upsTestResultsSummary;
				// TestResultsSummary
				// 1 --> donePass
				// 2 --> doneWarning
				// 3 --> doneError
				// 4 --> inProgress
				// 5 --> noTestInitiated
	DisplayString	upsTestResultsDetail;
	Long			upsTestStartTime;
	Integer		upsTestElapsedTime;
} UPSTEST;

/////////////////////////////////////////////////
/////////
/////////  upsControl
/////////
/////////////////////////////////////////////////

typedef struct upsControl
{
	Integer	upsShutdownType;
			// ShurdownType
			// 1 --> output
			// 2 --> system
	Integer	upsShutdownAfterDelay;
	Integer	upsStartupAfterDelay;
	Integer	upsRebootWithDuration;
	Integer	upsAutoRestart;
			// AutoRestart
			// 1 --> on
			// 2 --> off
} UPSCONTROL;

/////////////////////////////////////////////////
/////////
/////////  upsConfig
/////////
/////////////////////////////////////////////////

typedef struct upsConfig
{
	Integer	upsConfigInputVoltage;
	Integer	upsConfigInputFreq;
	Integer	upsConfigOutputVoltage;
	Integer	upsConfigOutputFreq;
	Integer	upsConfigOutputVA;
	Integer	upsConfigOutputPower;
	Integer	upsConfigLowBattTime;
	Integer	upsConfigAudibleStatus;
			// ConfigAudibleStatus
			// 1 --> disabled
			// 2 --> enabled
			// 3 --> muted
	Integer	upsConfigLowVoltageTransferPoint;
	Integer	upsConfigHighVoltageTransferPoint;
} UPSCONFIG;

//20031121 kenny
/////////////////////////////////////////////////
//////////
//////////  upsLed
//////////
/////////////////////////////////////////////////

typedef struct upsLed
{
	Integer	LEDBypass;
	Integer	LEDInput;
	Integer	LEDBattery;
	Integer	LEDRectifier;
	Integer	LEDInverter;
	Integer	LEDInverterSupply;
	Integer	LEDBypassSupply;		//upsAlarmOnBypass;
	Integer	LEDMaintenanceSupply;
	Integer	LEDOutput;
	Integer	LEDSync;
	Integer	LEDFault;
	Integer	LEDAudio;
	Integer	LEDLoadOutputPercent;
	Integer	LEDEstimatedCharge;
} UPSLED;
//kenny

//20031203 kenny
/////////////////////////////////////////////////
//////////
//////////  upsCBMC
//////////
/////////////////////////////////////////////////

typedef struct upsCBMC
{
	Integer	CBMCCB4;
	Integer	CBMCCB1;
	Integer	CBMCMC1a;
	Integer	CBMCMC1;
	Integer	CBMCMC5;
	Integer	CBMCMC3;
	Integer	CBMCMC2;
	Integer	CBMCMC2a;
	Integer	CBMCCB4a;
} UPSCBMC;
//kenny

//20040106 seth
/////////////////////////////////////////////////
//////////
//////////  upsFaultStatus
//////////
/////////////////////////////////////////////////

typedef struct upsFault
{
	int numOfFlts;
	char fltBits[TOTALFLTSTSBITS];
} UPSFAULT;
//seth

typedef struct 
{
	int	faultID;		//fault ID
	char date[8+1];		//date
	char time[6+1];		//time
} LogItem;

//20040115 seth
/////////////////////////////////////////////////
//////////
//////////  upsFaultStatus
//////////
/////////////////////////////////////////////////

typedef struct upsLog
{
	int iHead;
	LogItem item[MAX_LOG];
} UPSLOG;
//seth

/////////////////////////////////////////////////
//////////
//////////  upsObjects
//////////
/////////////////////////////////////////////////

typedef struct upsObjects
{
	UPSIDENT	upsident;
	UPSBATTERY	upsbattery;
	UPSINPUT	upsinput;
	UPSOUTPUT	upsoutput;
	UPSBYPASS	upsbypass;
	UPSALARM	upsalarm;
	UPSTEST		upstest;
	UPSCONTROL	upscontrol;
	UPSCONFIG	upsconfig;
	UPSINVERTER	upsinverter;
	UPSLED 		upsled;		//20031121 kenny
	UPSCBMC 	upscbmc;	//20031203 kenny
	UPSFAULT 	upsfault;	//20040106 seth
	UPSLOG 		upslog;	//20040119 seth
} UPSOBJECTS;

/////////////////////////////////////////////////
//////////
//////////  upsTraps
//////////
/////////////////////////////////////////////////

// 0 ----> trap 발생
// 1 ----> 정상
typedef struct upsTraps
{
	byte		upsTrapOnBattery;
	byte		upsTrapTestCompleted;
	byte		upsTrapAlarmEntryAdded;
	byte		upsTrapAlarmEntryRemoved;
} UPSTRAPS;

/////////////////////////////////////////////////
///////////
///////////  Shared Memory Main 구조체
///////////
/////////////////////////////////////////////////

struct UPS
{
	UPSOBJECTS	upsobjects;
	UPSTRAPS	upstraps;
};

/////////////////////////////////////////////////
///////////
///////////  Shared Memory Key
///////////
/////////////////////////////////////////////////

#define	KEY	((key_t)(2005))	// ((key_t)(2005))

/////////////////////////////////////////////////
///////////
///////////  Shared Memory Size
///////////
/////////////////////////////////////////////////

#define 	SEGSIZE	sizeof(struct UPS)

#endif
