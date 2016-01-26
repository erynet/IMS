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
        string Leave(string guid);

        [OperationContract]
        Event[] GetEvents(string guid);

        [OperationContract]
        Warning[] GetWarnings(string guid);

        [OperationContract]
        DeviceStatus GetDeviceStatus(int deviceId);

        [OperationContract]
        DeviceStatus[] GetAllDeviceStatus();

        [OperationContract]
        Setting GetSetting(string key);

        [OperationContract]
        Setting[] GetAllSettings();

        [OperationContract]
        bool SetSetting(string guid, Setting value);

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