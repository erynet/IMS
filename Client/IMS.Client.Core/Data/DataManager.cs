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
            var ups1 = new Ups();
            var ups2 = new Ups();

            ups1.Data = new Ups.Info {
                isUsing = false,
                upsID = ups1.ID,
                groupID = 1,
                upsName = "화장실-1",
                partnerList = IntList.Parse("2"),
                cduID = 0,
                batteryDescription = "듀라셀",
                batteryCapacity = "1kW",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            ups2.Data = new Ups.Info {
                isUsing = false,
                upsID = ups2.ID,
                groupID = 1,
                upsName = "화장실-2",
                partnerList = IntList.Parse("1"),
                cduID = 0,
                batteryDescription = "듀라셀",
                batteryCapacity = "1kW",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            upsList.Add(ups1.Data.upsID, ups1);
            upsList.Add(ups2.Data.upsID, ups2);

            // Cdu
            var cdu1 = new Cdu();
            cdu1.Data = new Cdu.Info {
                isUsing = true,
                cduID = cdu1.ID,
                cduName = "방",
                isExtended = false,
                upsList = new IntList { 0, 1 },
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            cduList.Add(cdu1.Data.cduID, cdu1);

            // Group
            var group1 = new Group();
            group1.Data = new Group.Info {
                isUsing = true,
                groupID = group1.ID,
                isGroupVisible = true,
                groupName = "방1",
                coordinate = new Point(300, 400),
                upsList = new IntList()
            };

            var group2 = new Group();
            group2.Data = new Group.Info {
                isUsing = true,
                groupID = group2.ID,
                isGroupVisible = true,
                groupName = "방2",
                coordinate = new Point(400, 500),
                upsList = new IntList { 0, 1 }
            };

            groupList.Add(group1.Data.groupID, group1);
            groupList.Add(group2.Data.groupID, group2);
        }

        public List<Ups.Info> GetUpsData()
        {
            var ret = new List<Ups.Info>();

            foreach (var pair in upsList) {
                var copyData = new Ups.Info();
                copyData.Copy(pair.Value.Data);
                ret.Add(copyData);
            }

            return ret;
        }

        public List<Cdu.Info> GetCduData()
        {
            var ret = new List<Cdu.Info>();

            foreach (var pair in cduList) {
                var copyData = new Cdu.Info();
                copyData.Copy(pair.Value.Data);
                ret.Add(copyData);
            }

            return ret;
        }

        public List<Group.Info> GetGroupData()
        {
            var ret = new List<Group.Info>();

            foreach (var pair in groupList) {
                var copyData = new Group.Info();
                copyData.Copy(pair.Value.Data);

                ret.Add(copyData);
            }

            return ret;
        }

        public void AddUps(Ups.Info newInfo)
        {
            var ups = GetUps(newInfo.upsID);
            if (ups == null) {
                ups = new Ups();
                ups.Data = newInfo;
                newInfo.upsID = ups.ID;

                upsList.Add(ups.ID, ups);
            }
        }

        public void AddCdu(Cdu.Info newInfo)
        {
            var cdu = GetCdu(newInfo.cduID);
            if (cdu == null) {
                cdu = new Cdu();
                cdu.Data = newInfo;
                newInfo.cduID = cdu.ID;

                cduList.Add(cdu.ID, cdu);
            }
        }

        public void AddGroup(Group.Info newInfo)
        {
            var group = new Group();
            group.Data = newInfo;
            newInfo.groupID = group.ID;

            groupList.Add(group.ID, group);
        }

        public void EditUps(Ups.Info info)
        {
            var ups = GetUps(info.upsID);
            ups?.Data.Copy(info);
        }

        public void EditCdu(Cdu.Info info)
        {
            var cdu = GetCdu(info.cduID);
            cdu?.Data.Copy(info);
        }

        public void EditGroup(Group.Info newInfo)
        {
            var group = GetGroup(newInfo.groupID);
            group?.Data.Copy(newInfo);
        }

        public Ups GetUps(int id)
        {
            Ups ret = null;
            upsList.TryGetValue(id, out ret);

            return ret;
        }

        public Cdu GetCdu(int id)
        {
            Cdu ret = null;
            cduList.TryGetValue(id, out ret);

            return ret;
        }

        public Group GetGroup(int id)
        {
            Group ret = null;
            groupList.TryGetValue(id, out ret);

            return ret;
        }

        public void DeleteUps(int id)
        {
            var ups = GetUps(id);
            if (ups == null) {
                return;
            }

            // Partner
            var partnerList = ups.Data.partnerList;
            foreach (var partnerID in partnerList) {
                if (partnerID == id) {
                    continue;
                }

                var partnerUps = GetUps(partnerID);
                partnerUps?.Data.partnerList.Remove(id);
            }

            // Cdu
            var cdu = GetCdu(ups.Data.cduID);
            cdu?.Data.upsList.Remove(id);

            // Group
            var group = GetGroup(ups.Data.groupID);
            group?.Data.upsList.Remove(id);

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
                if (ups.Data.cduID == id) {
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

            foreach (var upsId in group.Data.upsList) {
                var ups = GetUps(upsId);
                if (ups == null) {
                    continue;
                }

                ups.Data.groupID = -1;
            }

            groupList.Remove(id);
        }
    }
}
