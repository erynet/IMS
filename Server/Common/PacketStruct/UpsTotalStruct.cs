using System;

namespace IMS.Server.Common.PacketStruct
{
    public class UpsTotalStruct
    {
        private Int32 requestIndex;

        #region upsMimicState
        public Int32 BatteryInput;
        public Int32 MainInput;
        public Int32 BaypassInput;

        public Int32 MC1;
        public Int32 MC1A;
        public Int32 MC2;
        public Int32 MC2A;
        public Int32 MC3;
        public Int32 MC5;
        public Int32 CB1;
        public Int32 CB4;
        public Int32 CB4A;

        public Int32 AC_DC;
        public Int32 DC_AC;

        public Int32 Synchronous;
        public Int32 Fault;

        public Int32 InverterOperation;
        public Int32 BypassOperation;
        public Int32 MBSOperation;
        #endregion

        #region upsBattery
        public Int32 upsBatteryStatus;
        public Int32 upsSecondsOnBattery;            // UNITS : seconds
        public Int32 upsEstimatedMinutesRemaining;   // UNITS : minutes
        public Int32 upsEstimatedChargeRemaining;    // UNITS : percent
        public Int32 upsBatteryVoltage;              // UNITS : 0.1 Volt DC
        public Int32 upsBatteryCurrent;              // UNITS : 0.1 Amp DC
        public Int32 eti_batteryCondition;
        public Int32 eti_batteryCharge;
        public Int32 eti_DCLinkVoltage;
        #endregion

        #region upsInput
        public Int32 upsInputLineBads;
        public Int32 upsInputNumLines;
        // J_UPSINPUTTABLE
        public Int32 mainVoltage1;
        public Int32 mainVoltage2;
        public Int32 mainVoltage3;
        public Int32 ConVdcP;
        public Int32 ConVdcN;
        public Int32 dcdcL1Current;
        public Int32 dcdcL2Current;
        #endregion

        #region upsOutput
        public Int32 upsOutputSource;
        public Int32 upsOutputNumLines;
        public Int32 upsOutputVoltage1;                // UNITS : RMS Voltage
        public Int32 upsOutputCurrent1;                // UNITS : 0.1 RMS Amp
        public Int32 upsOutputPower1;                      // UNITS : Watts
        public Int32 upsOutputPercentLoad1;          // UNITS : percent
        public Int32 upsOutputFrequency1;                // UNITS : 0.1 Hertz   
        public Int32 upsOutputVoltage2;                // UNITS : RMS Voltage
        public Int32 upsOutputCurrent2;                // UNITS : 0.1 RMS Amp
        public Int32 upsOutputPower2;                      // UNITS : Watts
        public Int32 upsOutputPercentLoad2;          // UNITS : percent
        public Int32 upsOutputFrequency2;                // UNITS : 0.1 Hertz  
        public Int32 upsOutputVoltage3;                // UNITS : RMS Voltage
        public Int32 upsOutputCurrent3;                // UNITS : 0.1 RMS Amp
        public Int32 upsOutputPower3;                      // UNITS : Watts
        public Int32 upsOutputPercentLoad3;          // UNITS : percent
        public Int32 upsOutputFrequency3;
        #endregion

        #region upsBypass
        public Int32 upsBypassNumLines;
        public Int32 upsBypassVoltage1;      // UNITS : RMS Voltage
        public Int32 upsBypassCurrent1;      // UNITS : 0.1 RMS Amp
        public Int32 upsBypassPower1;            // UNITS : Watts
        public Int32 upsBypassFrequency1;    // UNITS : 0.1 Hertz
        public Int32 upsBypassVoltage2;      // UNITS : RMS Voltage
        public Int32 upsBypassCurrent2;      // UNITS : 0.1 RMS Amp
        public Int32 upsBypassPower2;            // UNITS : Watts
        public Int32 upsBypassFrequency2;    // UNITS : 0.1 Hertz
        public Int32 upsBypassVoltage3;      // UNITS : RMS Voltage
        public Int32 upsBypassCurrent3;      // UNITS : 0.1 RMS Amp
        public Int32 upsBypassPower3;            // UNITS : Watts
        public Int32 upsBypassFrequency3;
        #endregion

        #region upsInverter
        public Int32 eti_inverterOutputFrequency1;
        public Int32 eti_inverterOutputVoltage1;
        public Int32 eti_inverterOutputCurrent1;
        public Int32 eti_inverterOutputFrequency2;
        public Int32 eti_inverterOutputVoltage2;
        public Int32 eti_inverterOutputCurrent2;
        public Int32 eti_inverterOutputFrequency3;
        public Int32 eti_inverterOutputVoltage3;
        public Int32 eti_inverterOutputCurrent3;
        public Int32 nCurrent;                       //	10	//141020
        public Int32 inverterVdcP;
        public Int32 inverterVdcN;
        public Int32 eti_ConverterTemp;
        public Int32 eti_InverterTemp;
        public Int32 eti_BatteryTemp;
        public Int32 eti_DCDCTemp;
        public Int32 eti_ISSUTemp;
        public Int32 eti_InnerTemp;
        #endregion

        public Int32 outputLoadTotal;
        public Int32 alarmCount;
        public Int32 systemInfo;
    }
}