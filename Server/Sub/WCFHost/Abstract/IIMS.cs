using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    /*
    //Usage Scenario
    string sessionId = Proxy.Athenticate("12-34-56-78-90-qw-er-ty");
    while(ContinueLoop)
    {
        Proxy.GetEvents(guid);
        Proxy.GetWarnings(guid);
        Proxy.GetAllDeviceStatus();
    }
    Proxy.Leave(guid);
    */

    [ServiceContract(Namespace = "http://aspt.com/IMS/", SessionMode = SessionMode.Required)]
    public interface IIMS
    {
        // 최초 프로그램을 킬때, 사용자의 로그인을 받는다.
        // MacAddress 를 기록하는게 필요가 있나 고민중.
        [OperationContract]
        string Athenticate(string macAddress);

        // 프로그램을 종료할때, 해당 클라이언트가 종료한다는것을 알려준다.
        // 안해도 치명적인 문제는 없지만 있는편이 좋다.
        [OperationContract]
        bool Leave();

        // 클라이언트가 주기적으로 호출하여, 자신이 확인한것 이후로 새로운 이벤트가 존재하는지 확인한다.
        // 없으면 빈 이벤트가 갈 것이고, 아니면 이벤트의 목록이 가게 되어 해당 이벤트에 맞는 동작을 실행하게 된다.
        [OperationContract]
        List<IMSEvent> GetEvents();

        // 클라이언트가 주기적, 혹은 필요에 의해 호출하여 경고메시지를 긁어간다.
        // 없으면 빈 경고가 갈 것이다.
        [OperationContract]
        List<IMSWarning> GetWarnings();

        // 그룹 번호들과 해당 그룹의 상태를 리턴한다.
        // 그룹 전체의 목록을 받아갈떄 필요하다.
        [OperationContract]
        List<IMSGroup> GetGroups();

        // 특정 그룹 아래에 속한 모든 장치들의 정보를 받아갈때 사용한다.
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