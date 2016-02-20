using System;
using System.Collections;
using System.Collections.Generic;

namespace IMS.Client.Core.Data {
    public class IntList : IList<int> {
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

        public int Count => list.Count;
        public bool IsReadOnly => false;

        public int this[int index]
        {
            get
            {
                return list[index];
            }

            set
            {
                list[index] = value;
            }
        }

        public IntList()
        {

        }

        public IntList(IntList rhs)
        {
            list = new List<int>(rhs.list);
        }

        public IntList(IEnumerable<int> collection)
        {
            list.AddRange(collection);
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

        public int IndexOf(int item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, int item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Add(int item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(int item)
        {
            return list.Contains(item);
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        bool ICollection<int>.Remove(int item)
        {
            return list.Remove(item);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
