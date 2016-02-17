using System;

namespace IMS.Client.Core {
    public class Point {
        private int x;
        private int y;

        public int X => x;
        public int Y => y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(string x, string y)
        {
            this.x = int.Parse(x);
            this.y = int.Parse(y);
        }

        public Point(Point rhs)
        {
            x = rhs.x;
            y = rhs.y;
        }

        public Point(string tdxt)
        {
            ParseStr(tdxt);
        }

        public override string ToString()
        {
            return x + "," + y;
        }

        public void ParseStr(string text)
        {
            var coords = text.Split(',');
            if (coords.Length != 2) {
                return;
            }

            x = int.Parse(coords[0]);
            y = int.Parse(coords[1]);
        }

        static public Point Parse(string text)
        {
            var coords = text.Split(',');

            if (coords.Length != 2) {
                return new Point();
            }
            else {
                return new Point(coords[0], coords[1]);
            }
        }
    }
}
