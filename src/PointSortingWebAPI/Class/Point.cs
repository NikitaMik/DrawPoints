using System;
using System.Runtime.Serialization;

namespace PointSortingWebAPI
{
    [DataContract]
    public class Point : IComparable<Point>
    {
        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }

        public double Radius { get; set; }

        public Point()
        {
        }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
            RadiusCount();
        }

        private void RadiusCount()
        {
            Radius = Math.Round(Math.Sqrt(Math.Pow((X), 2) + Math.Pow((Y), 2)), 2);
        }

        public void RadiusList(Point center)
        {
            Radius = Math.Round(Math.Sqrt(Math.Pow((X - center.X), 2) + Math.Pow((Y - center.Y), 2)), 2);
        }

        int IComparable<Point>.CompareTo(Point other)
        {
            return Convert.ToInt32(Radius - other.Radius);
        }
    }

    public class SpiralPoint 
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Point BezE { get; set; }

        static double angle = 270;
        static double radian = (angle * Math.PI / 180);

        public SpiralPoint(Point start, Point end) 
        {
            this.Start = start;
            this.End = end;
            this.BezE = new Point();
            function();
        }

        public void function()
        {
            double centerX = (Start.X + End.X) / 2;
            double centerY = (Start.Y + End.Y) / 2;

            BezE.X = Math.Round(centerX + (Start.X - centerX) * Math.Cos(radian) - (Start.Y - centerY) * Math.Sin(radian),2);
            BezE.Y = Math.Round(centerY + (Start.X - centerX) * Math.Sin(radian) + (Start.Y - centerY) * Math.Cos(radian),2);
        }

    }
}