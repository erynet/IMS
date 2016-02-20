using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;

using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    /*
    //Usage Scenario
    string sessionId = Proxy.Athenticate("12-34-56-78-90-qw-er-ty");
    while(ContinueLoop)
    {
        List<IMSEvent> Events = Proxy.GetEvents();
        var grouped = from e in Events g e by e.Code select e;
        foreach (IMSEvent ie in grouped)
        {
            switch()
            {
                case 0:
                    // do nothing
                    break;
                case 10:
                    // have warning
                    var warn = Proxy.GetWarnings();
                    ...
                    break;
                case 100:
                    // ups status modificated
                    var uos = Proxy.GetAllUpsStatus();
                    ...
                    break;
                case 1000:
                    // global setting modified
                    var settings = Proxy.GetAllSettings();
                    ...
                    break;
            }
        }
    }
    Proxy.Leave(guid);
    */

    [ServiceContract(Namespace = "http://aspt.com/IMS/", SessionMode = SessionMode.Required)]
    public interface IIMS
    {
        [OperationContract]
        string Ping();

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
        List<IMSEvent> GetEvents(int maxCount = 100, int? priority = null, DateTime? from = null, DateTime? to = null);

        // 클라이언트가 주기적, 혹은 필요에 의해 호출하여 경고메시지를 긁어간다.
        // 없으면 빈 경고가 갈 것이다.
        [OperationContract]
        List<IMSWarning> GetWarnings();

        #region Group

        // 그룹 번호들과 해당 그룹의 상태를 리턴한다.
        // 그룹 전체의 목록을 받아갈떄 필요하다.
        [OperationContract]
        IMSGroups GetGroups();

        [OperationContract]
        IMSGroup GetGroup(int groupIdx);

        [OperationContract]
        bool SetGroup(IMSGroup g);

        [OperationContract]
        int AddGroup(IMSGroup g);

        [OperationContract]
        bool DelGroup(int groupIdx);

        #endregion

        #region Ups

        // 특정 그룹 아래에 속한 모든 UPS 들의 정보를 받아갈때 사용한다.
        [OperationContract]
        List<IMSUps> GetAllUps(int? groupIdx);

        [OperationContract]
        IMSUps GetUps(int upsIdx);

        [OperationContract]
        bool SetUps(IMSUps ups);

        [OperationContract]
        int AddUps(IMSUps ups);

        [OperationContract]
        bool DelUps(int upsIdx);

        [OperationContract]
        IMSUpsStatus GetUpsStatus(int upsIdx);

        [OperationContract]
        List<IMSUpsEvent> GetUpsEvents(int upsIdx, int maxCount = 100, DateTime? from = null, DateTime? to = null);

        #endregion

        #region Cdu

        [OperationContract]
        List<IMSCdu> GetAllCdu(int? groupIdx);

        [OperationContract]
        IMSCdu GetCdu(int cduIdx);

        [OperationContract]
        bool SetCdu(IMSCdu cdu);

        [OperationContract]
        int AddCdu(IMSCdu cdu);

        [OperationContract]
        bool DelCdu(int cduIdx);

        [OperationContract]
        IMSCduStatus GetCduStatus(int cduIdx);

        [OperationContract]
        List<IMSCduEvent> GetCduEvents(int cduIdx, int maxCount = 100, DateTime? from = null, DateTime? to = null);

        [OperationContract]
        List<IMSCduSocket> GetCduSocket(int cduIdx);

        [OperationContract]
        bool SetCduSocket(IMSCduSocket cduSocket);

        #endregion

        #region Setting

        [OperationContract]
        IMSSetting GetSetting(string key);

        [OperationContract]
        List<IMSSetting> GetAllSettings();

        [OperationContract]
        bool SetSetting(IMSSetting value);

        #endregion
    }
}