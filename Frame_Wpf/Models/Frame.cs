using System.Collections.Generic;
using Tekla.Structures.Geometry3d;

namespace Frame_Wpf
{
    class Frame
    {
        public List<Point> Columns = new List<Point>();
        public List<Point> Beams = new List<Point>();

        public Frame(double l, double h1, double h2)
        {
            Point c1StartPoint = new Point(0, 0, 0);
            Point c1EndPoint = new Point(0, 0, h1);
            Point c2StartPoint = new Point(l, 0, 0);
            Point c2EndPoint = new Point(l, 0, h1);

            Columns.Add(c1StartPoint);
            Columns.Add(c1EndPoint);
            Columns.Add(c2StartPoint);
            Columns.Add(c2EndPoint);

            Point b1StartPoint = new Point(0, 0, h1);
            Point b1EndPoint = new Point(l/2, 0, h1 + h2);
            Point b2StartPoint = new Point(l/2, 0, h1 + h2);
            Point b2EndPoint = new Point(l, 0, h1);

            Beams.Add(b1StartPoint);
            Beams.Add(b1EndPoint);
            Beams.Add(b2StartPoint);
            Beams.Add(b2EndPoint);

        }
    }
}
