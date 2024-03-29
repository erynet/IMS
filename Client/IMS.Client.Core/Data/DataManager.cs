﻿using System;
using System.Collections.Generic;
using IMS.Client.Core.Data.DB;
using IMS.Database.LocalDB.Model;

namespace IMS.Client.Core.Data {
    public class DataManager {
        private static DataManager client = new DataManager();
        public static DataManager inst => client;

        public void Init()
        {
            // Ups
            //var ups1 = new Ups();
            //var ups2 = new Ups();

            //ups1.Data = new Ups.Info {
            //    isUsing = false,
            //    upsIdx = ups1.ID,
            //    groupIdx = 1,
            //    upsName = "화장실-1",
            //    partnerIdxList = IntList.Parse("2"),
            //    cduIdx = 0,
            //    batteryDescription = "듀라셀",
            //    batteryCapacity = "1kW",
            //    ip = "192.168.0.1",
            //    installDate = "2016.01.01",
            //};

            //ups2.Data = new Ups.Info {
            //    isUsing = false,
            //    upsIdx = ups2.ID,
            //    groupIdx = 1,
            //    upsName = "화장실-2",
            //    partnerIdxList = IntList.Parse("1"),
            //    cduIdx = 0,
            //    batteryDescription = "듀라셀",
            //    batteryCapacity = "1kW",
            //    ip = "192.168.0.1",
            //    installDate = "2016.01.01",
            //};

            //upsList.Add(ups1.Data.upsIdx, ups1);
            //upsList.Add(ups2.Data.upsIdx, ups2);

            //// Cdu
            //var cdu1 = new Cdu();
            //cdu1.Data = new Cdu.Info {
            //    isUsing = true,
            //    cduIdx = cdu1.ID,
            //    cduName = "방",
            //    isExtended = false,
            //    upsIdxList = new IntList { 0, 1 },
            //    ip = "192.168.0.1",
            //    installDate = "2016.01.01",
            //};

            //cduList.Add(cdu1.Data.cduIdx, cdu1);

            //// Group
            //var group1 = new Group();
            //group1.Data = new Group.Info {
            //    isUsing = true,
            //    groupIdx = group1.ID,
            //    isGroupVisible = true,
            //    groupName = "방1",
            //    coordinate = new Point(300, 400),
            //    upsIdxList = new IntList()
            //};

            //var group2 = new Group();
            //group2.Data = new Group.Info {
            //    isUsing = true,
            //    groupIdx = group2.ID,
            //    isGroupVisible = true,
            //    groupName = "방2",
            //    coordinate = new Point(400, 500),
            //    upsIdxList = new IntList { 0, 1 }
            //};

            //groupList.Add(group1.Data.groupIdx, group1);
            //groupList.Add(group2.Data.groupIdx, group2);
        }

        public List<Ups.Info> GetUpsData()
        {
            return LocalDBDriver.GetUpss();
            //var ret = new List<Ups.Info>();

            //foreach (var pair in upsList) {
            //    var copyData = new Ups.Info();
            //    copyData.Copy(pair.Value.Data);
            //    ret.Add(copyData);
            //}

            //return ret;
        }

        public List<Cdu.Info> GetCduData()
        {
            return LocalDBDriver.GetCdus();
            //var ret = new List<Cdu.Info>();

            //foreach (var pair in cduList) {
            //    var copyData = new Cdu.Info();
            //    copyData.Copy(pair.Value.Data);
            //    ret.Add(copyData);
            //}

            //return ret;
        }

        public List<Group.Info> GetGroupData()
        {
            return LocalDBDriver.GetGroups();
            //var ret = new List<Group.Info>();

            //foreach (var pair in groupList) {
            //    var copyData = new Group.Info();
            //    copyData.Copy(pair.Value.Data);

            //    ret.Add(copyData);
            //}

            //return ret;
        }

        public List<CduSocket.Info> GetSocketData(int cduIdx)
        {
            return LocalDBDriver.GetCduSocketsByIdx(cduIdx);
            //return null;
        }

        public List<UpsEvent.Info> GetRecentUpsEvent(int upsNo, int maxRow = 100)
        {
            return LocalDBDriver.GetRecentUpsEvent(upsNo, maxRow);
        }

        public List<UpsEvent.Info> GetUpsEventRange(int upsNo, DateTime from_time, DateTime to_time)
        {
            return LocalDBDriver.GetUpsEventRange(upsNo, from_time, to_time);
        }

        public List<CduEvent.Info> GetRecentCduEvent(int cduNo, int maxRow = 100)
        {
            return LocalDBDriver.GetRecentCduEvent(cduNo, maxRow);
        }

        public List<CduEvent.Info> GetCduEventRange(int cduNo, DateTime from_time, DateTime to_time)
        {
            return LocalDBDriver.GetCduEventRange(cduNo, from_time, to_time);
        }

        public void AddUps(Ups.Info newInfo)
        {
            LocalDBDriver.AddUps(newInfo);
            //var ups = GetUps(newInfo.upsIdx);
            //if (ups == null) {
            //    ups = new Ups();
            //    ups.Data = newInfo;
            //    newInfo.upsIdx = ups.ID;

            //    upsList.Add(ups.ID, ups);
            //}
        }

        public void AddCdu(Cdu.Info newInfo)
        {
            LocalDBDriver.AddCdu(newInfo);
            //var cdu = GetCdu(newInfo.cduIdx);
            //if (cdu == null) {
            //    cdu = new Cdu();
            //    cdu.Data = newInfo;
            //    newInfo.cduIdx = cdu.ID;

            //    cduList.Add(cdu.ID, cdu);
            //}
        }

        public void AddGroup(Group.Info newInfo)
        {
            LocalDBDriver.AddGroup(newInfo);
            //var group = new Group();
            //group.Data = newInfo;
            //newInfo.groupIdx = group.ID;

            //groupList.Add(group.ID, group);
        }

        public void EditUps(Ups.Info info)
        {
            LocalDBDriver.SetUps(info);
            //var ups = GetUps(info.upsIdx);
            //ups?.Data.Copy(info);
        }

        public void EditCdu(Cdu.Info info)
        {
            LocalDBDriver.SetCdu(info);
            //var cdu = GetCdu(info.cduIdx);
            //cdu?.Data.Copy(info);
        }

        public void EditGroup(Group.Info info)
        {
            LocalDBDriver.SetGroup(info);
            //var group = GetGroup(newInfo.groupIdx);
            //group?.Data.Copy(newInfo);
        }

        public void EditSocket(int cduIdx, List<CduSocket.Info> infos)
        {
            LocalDBDriver.SetCduSocket(cduIdx, infos);
        }

        public Ups.Info GetUps(int idx)
        {
            return LocalDBDriver.GetUpsByIdx(idx);
            //Ups ret = null;
            //upsList.TryGetValue(id, out ret);

            //return ret;
        }

        public Cdu.Info GetCdu(int idx)
        {
            return LocalDBDriver.GetCduByIdx(idx);
            //Cdu ret = null;
            //cduList.TryGetValue(id, out ret);

            //return ret;
        }

        public Group.Info GetGroup(int idx)
        {
            return LocalDBDriver.GetGroupByIdx(idx);
            //Group ret = null;
            //groupList.TryGetValue(id, out ret);

            //return ret;
        }

        public List<WarningLog.Info> GetWarningLog(DateTime fromTime)
        {
            return LocalDBDriver.GetWarningLog(fromTime);
        }

        public List<WarningLog.Info> GetWarningLogRange(DateTime fromTime, DateTime toTime)
        {
            return LocalDBDriver.GetWarningLogRange(fromTime, toTime);
        }

        public List<EventLog.Info> GetEventLog(DateTime fromTime)
        {
            return LocalDBDriver.GetEventLog(fromTime);
        }

        public List<EventLog.Info> GetEventLogRange(DateTime fromTime, DateTime toTime)
        {
            return LocalDBDriver.GetEventLogRange(fromTime, toTime);
        }

        public void DeleteUps(int id)
        {
            LocalDBDriver.DelUps(id);
        }

        public void DeleteCdu(int id)
        {
            LocalDBDriver.DelCdu(id);
        }

        public void DeleteGroup(int id)
        {
            LocalDBDriver.DelGroup(id);
        }
    }
}
