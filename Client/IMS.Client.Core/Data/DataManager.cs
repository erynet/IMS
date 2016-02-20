using System.Collections.Generic;
using IMS.Client.Core.Data.DB;

namespace IMS.Client.Core.Data {
    public class DataManager {
        private static DataManager client = new DataManager();
        public static DataManager inst => client;

        private Dictionary<int, Ups> upsList = new Dictionary<int, Ups>();
        private Dictionary<int, Cdu> cduList = new Dictionary<int, Cdu>();
        private Dictionary<int, Group> groupList = new Dictionary<int, Group>();

        public Dictionary<int, Ups> UpsList => upsList;
        public Dictionary<int, Cdu> CduList => cduList;
        public Dictionary<int, Group> GroupList => groupList;

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

        public void DeleteUps(int id)
        {
            var ups = GetUps(id);
            if (ups == null) {
                return;
            }

            // Partner
            var partnerList = ups.partnerIdxList;
            foreach (var partnerID in partnerList) {
                if (partnerID == id) {
                    continue;
                }

                var partnerUps = GetUps(partnerID);
                partnerUps?.partnerIdxList.Remove(id);
            }

            // Cdu
            var cdu = GetCdu(ups.cduIdx);
            cdu?.upsIdxList.Remove(id);

            // Group
            var group = GetGroup(ups.groupIdx);
            group?.upsIdxList.Remove(id);

            upsList.Remove(id);
        }

        public void DeleteCdu(int id)
        {
            var cdu = GetCdu(id);
            if (cdu == null) {
                return;
            }

            // Remove child ups
            var removeUpsList = new List<int>();

            foreach (var pair in upsList) {
                var ups = pair.Value;
                if (ups.Data.cduIdx == id) {
                    removeUpsList.Add(ups.ID);
                }
            }

            foreach (var upsID in removeUpsList) {
                upsList.Remove(upsID);
            }

            // Remove
            cduList.Remove(id);
        }

        public void DeleteGroup(int id)
        {
            var group = GetGroup(id);
            if (group == null) {
                return;
            }

            foreach (var upsId in group.upsIdxList) {
                var ups = GetUps(upsId);
                if (ups == null) {
                    continue;
                }

                ups.groupIdx = -1;
            }

            groupList.Remove(id);
        }
    }
}
