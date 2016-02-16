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

        public override string ToString()
        {
            return x + "," + y;
        }

        static public Point Parse(string text)
        {
            var coords = text.Split(',');

            return new Point(coords[0], coords[1]);
        }
    }
}
