using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    [ServiceContract(Namespace = "http://aspt.com/IMS/", SessionMode = SessionMode.Required)]
    public interface IIMS
    {
        // 최초 프로그램을 킬때, 사용자의 로그인을 받는다.
        // MacAddress 를 기록하는게 필요가 있나 고민중.
        [OperationContract]
        string Athenticate(string id, string passwd, string macAddress);

        // 프로그램을 종료할때, 해당 클라이언트가 종료한다는것을 알려준다.
        // 안해도 치명적인 문제는 없지만 있는편이 좋다.
        [OperationContract]
        bool Leave();

        [OperationContract]
        List<IMSEvent> GetEvents();

        [OperationContract]
        List<IMSWarning> GetWarnings();

        [OperationContract]
        List<IMSGroup> GetGroups();

        [OperationContract]
        List<IMSDevice> GetDevicesOfSomeGroup(int groupIdx);

        [OperationContract]
        IMSDeviceStatus GetDeviceStatus(int deviceIdx);

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