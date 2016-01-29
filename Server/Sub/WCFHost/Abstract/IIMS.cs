using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    [ServiceContract(Namespace = "http://aspt.com/IMS/", SessionMode = SessionMode.Required)]
    public interface IIMS
    {
        [OperationContract]
        string Athenticate(string id, string passwd, string macAddress);

        [OperationContract]
        bool Leave();

        [OperationContract]
        List<IMSEvent> GetEvents();

        [OperationContract]
        List<IMSWarning> GetWarnings();

        [OperationContract]
        IMSDeviceStatus GetDeviceStatus(int deviceId);

        [OperationContract]
        List<IMSDeviceStatus> GetAllDeviceStatus();

        [OperationContract]
        IMSSetting GetSetting(string key);

        [OperationContract]
        List<IMSSetting> GetAllSettings();

        [OperationContract]
        bool SetSetting(IMSSetting value);

        /*
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        //[TransactionFlow(TransactionFlowOption.Mandatory)]
        string SessionOpen();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool Athenticate(string uuid);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        string Registration(Identity identity);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        //[TransactionFlow(TransactionFlowOption.Mandatory)]
        void ReportSessionSummary(DataContracts sessionSummary);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        //[TransactionFlow(TransactionFlowOption.Mandatory)]
        SiteInfo RequestOpenFtpSite();

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        //[TransactionFlow(TransactionFlowOption.Mandatory)]
        bool AckImageUploadComplete(int imageId);

        // 수동 트랜잭션 닫기
        [OperationContract(IsInitiating = false, IsTerminating = true)]
        //[TransactionFlow(TransactionFlowOption.Mandatory)]
        void SessionClose(bool isSuccess);
        */
    }
}