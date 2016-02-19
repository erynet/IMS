using System.Collections.Generic;

namespace IMS.Client.Core {
    public class IntList {
        static public IntList Parse(string text)
        {
            var ret = new IntList();

            var ids = text.Split(',');
            foreach (var strID in ids) {
                int id;
                if (int.TryParse(strID, out id) == true) {
                    ret.list.Add(id);
                }
            }

            return ret;
        }

        private List<int> list = new List<int>();

        public IReadOnlyList<int> IDList => list;

        public IntList()
        {

        }

        public IntList(IntList rhs)
        {
            list.AddRange(rhs.list);
        }

        public override string ToString()
        {
            string ret = "";

            foreach (var id in list) {
                ret += id + ",";
            }

            if (list.Count != 0) {
                ret = ret.Remove(ret.Length - 1, 1);
            }

            return ret;
        }

        public void Remove(int id)
        {
            list.Remove(id);
        }
    }
}
