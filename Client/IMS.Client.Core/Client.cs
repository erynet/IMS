using System.Collections.Generic;

namespace IMS.Client.Core {
    public class Client {
        private static Client client = new Client();
        public static Client inst => client;

        private Dictionary<int, Ups> upsList = new Dictionary<int, Ups>();
        private Dictionary<int, Panel> panelList = new Dictionary<int, Panel>();
        private Dictionary<int, Group> groupList = new Dictionary<int, Group>();

        public Dictionary<int, Ups> UpsList => upsList;
        public Dictionary<int, Panel> PanelList => panelList;
        public Dictionary<int, Group> GroupList => groupList;

        public void Init()
        {
            // Ups
            var ups1 = new Ups();
            var ups2 = new Ups();

            ups1.Data = new Ups.Info {
                isUsing = false,
                upsID = ups1.ID,
                groupNumber = 1,
                upsName = "화장실-1",
                partnerList = PartnerList.Parse("2"),
                panelID = 1,
                batteryDescription = "듀라셀",
                batteryCapacity = "1kW",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            ups2.Data = new Ups.Info {
                isUsing = false,
                upsID = ups2.ID,
                groupNumber = 1,
                upsName = "화장실-2",
                partnerList = PartnerList.Parse("1"),
                panelID = 1,
                batteryDescription = "듀라셀",
                batteryCapacity = "1kW",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            upsList.Add(ups1.Data.upsID, ups1);
            upsList.Add(ups2.Data.upsID, ups2);

            // Panel
            var panel1 = new Panel();
            panel1.Data = new Panel.Info {
                isUsing = true,
                panelID = 3,
                panelName = "방",
                isExtended = false,
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            };

            panelList.Add(panel1.Data.panelID, panel1);

            // Group
            var group1 = new Group();
            group1.Data = new Group.Info {
                isUsing = true,
                groupNumber = group1.ID,
                isGroupVisible = true,
                groupName = "방1",
                isSeperatelyUsing = false,
                coordinate = new Point(300, 400)
            };

            var group2 = new Group();
            group2.Data = new Group.Info {
                isUsing = true,
                groupNumber = group2.ID,
                isGroupVisible = true,
                groupName = "방2",
                isSeperatelyUsing = false,
                coordinate = new Point(400, 500)
            };

            group2.Data.UpsList.Add(0);
            group2.Data.UpsList.Add(1);

            groupList.Add(group1.Data.groupNumber, group1);
            groupList.Add(group2.Data.groupNumber, group2);
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

        public List<Panel.Info> GetPanelData()
        {
            var ret = new List<Panel.Info>();

            foreach (var pair in panelList) {
                var copyData = new Panel.Info();
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

        public void AddGroup(Group.Info newInfo)
        {
            var group = GetGroup(newInfo.groupNumber);
            if (group == null) {
                group = new Group();
                group.Data = newInfo;
                newInfo.groupNumber = group.ID;

                groupList.Add(group.ID, group);
            }
        }

        public void AddGroup(string groupName, string upsList, string coord)
        {
            var group = new Group();
            group.Data = new Group.Info {
                isUsing = true,
                groupNumber = group.ID,
                isGroupVisible = true,
                groupName = groupName,
                isSeperatelyUsing = false,
                coordinate = Point.Parse(coord)
            };

            var ups = upsList.Split(',');
            foreach (var id in ups) {
                group.Data.UpsList.Add(int.Parse(id));
            }
        }

        public void EditUps(Ups.Info info)
        {
            var ups = GetUps(info.upsID);
            ups?.Data.Copy(info);
        }

        public void EditGroup(Group.Info newInfo)
        {
            var group = GetGroup(newInfo.groupNumber);
            group?.Data.Copy(newInfo);
        }

        public void EditGroup(int groupID, string groupName, string upsList, string coord)
        {
            var group = GetGroup(groupID);
            if (group == null) {
                return;
            }

            group.Data.groupName = groupName;
            group.Data.coordinate = Point.Parse(coord);

            var oldUps = new List<int>(group.Data.UpsList);

            group.Data.UpsList.Clear();
            var newUps = upsList.Split(',');
            foreach (var strID in newUps) {
                int id;
                if (int.TryParse(strID, out id) == false) {
                    continue;
                }

                group.Data.UpsList.Add(id);
            }

            foreach (var id in oldUps) {
                var ups = GetUps(id);
                if (ups == null) {
                    continue;
                }

                ups.Data.groupNumber = -1;
            }

            foreach (var strID in newUps) {
                int id;
                if (int.TryParse(strID, out id) == false) {
                    continue;
                }

                var ups = GetUps(id);
                if (ups == null) {
                    continue;
                }

                ups.Data.groupNumber = groupID;
            }
        }

        public Ups GetUps(int id)
        {
            Ups ret = null;
            upsList.TryGetValue(id, out ret);

            return ret;
        }

        public Panel GetPanel(int id)
        {
            Panel ret = null;
            panelList.TryGetValue(id, out ret);

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

            var partnerList = ups.Data.partnerList;
            foreach (var partnerID in partnerList.IDList) {
                if (partnerID == id) {
                    continue;
                }

                var partnerUps = GetUps(partnerID);
                partnerUps?.Data.partnerList.Remove(id);
            }

            var group = GetGroup(ups.Data.groupNumber);
            group?.Data.UpsList.Remove(id);

            upsList.Remove(id);
        }

        public void DeleteGroup(int id)
        {
            var group = GetGroup(id);
            if (group == null) {
                return;
            }

            foreach (var upsId in group.Data.UpsList) {
                var ups = GetUps(upsId);
                if (ups == null) {
                    continue;
                }

                ups.Data.groupNumber = -1;
            }

            groupList.Remove(id);
        }
    }
}
