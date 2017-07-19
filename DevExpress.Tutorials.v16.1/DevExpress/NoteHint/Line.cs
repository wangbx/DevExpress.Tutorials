namespace DevExpress.NoteHint
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class Line
    {
        public Line(double x1, double y1, double x2, double y2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }

        public bool ContainsPoint(Point point)
        {
            double num = GetLength(point.X, point.Y, this.X1, this.Y1);
            double num2 = GetLength(point.X, point.Y, this.X2, this.Y2);
            return (((int) (num + num2)) == ((int) this.GetLength()));
        }

        public double GetDistance(Point point, out Point intersectionPoint)
        {
            double d = (((this.X1 * Math.Pow(this.Y2 - this.Y1, 2.0)) + (point.X * Math.Pow(this.X2 - this.X1, 2.0))) + (((this.X2 - this.X1) * (this.Y2 - this.Y1)) * (point.Y - this.X1))) / (Math.Pow(this.Y2 - this.Y1, 2.0) + Math.Pow(this.X2 - this.X1, 2.0));
            if (double.IsNaN(d))
            {
                d = this.X1;
            }
            double num2 = (((this.X2 - this.X1) * (point.X - d)) / (this.Y2 - this.Y1)) + point.Y;
            if (double.IsNaN(num2))
            {
                num2 = this.Y1;
            }
            intersectionPoint = new Point(d, num2);
            return this.Len(new Point(intersectionPoint.X - point.X, intersectionPoint.Y - point.Y));
        }

        public double GetDistanceToCenterPoint(Point point, out Point intersectPoint)
        {
            double num = GetLength(point.X, point.Y, this.X1, this.Y1);
            double num2 = GetLength(point.X, point.Y, this.X2, this.Y2);
            double num3 = 0.5;
            if (this.GetLength() > 40.0)
            {
                num3 = (num > num2) ? 0.6 : 0.3;
            }
            intersectPoint = new Point(this.X1 + ((this.X2 - this.X1) * num3), this.Y1 + ((this.Y2 - this.Y1) * num3));
            return GetLength(intersectPoint.X, intersectPoint.Y, point.X, point.Y);
        }

        public double GetLength()
        {
            return GetLength(this.X1, this.Y1, this.X2, this.Y2);
        }

        public static double GetLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2.0) + Math.Pow(y2 - y1, 2.0));
        }

        private double Len(Point vec)
        {
            return Math.Sqrt(Math.Pow(vec.X, 2.0) + Math.Pow(vec.Y, 2.0));
        }

        public double X1 { get; private set; }

        public double X2 { get; private set; }

        public double Y1 { get; private set; }

        public double Y2 { get; private set; }
    }
}

