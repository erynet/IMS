using System.Collections.Generic;

namespace IMS.Client.Core {
    public class PartnerList {
        static public PartnerList Parse(string text)
        {
            var ret = new PartnerList();

            var ids = text.Split(',');
            foreach (var strID in ids) {
                int id;
                if (int.TryParse(strID, out id) == true) {
                    ret.idList.Add(id);
                }
            }

            return ret;
        }

        private List<int> idList = new List<int>();

        public IReadOnlyList<int> IDList => idList;

        public PartnerList()
        {

        }

        public PartnerList(PartnerList rhs)
        {
            idList.AddRange(rhs.idList);
        }

        public override string ToString()
        {
            string ret = "";

            foreach (var id in idList) {
                ret += id + ",";
            }

            if (idList.Count != 0) {
                ret = ret.Remove(ret.Length - 1, 1);
            }

            return ret;
        }

        public void Remove(int id)
        {
            idList.Remove(id);
        }
    }
}
