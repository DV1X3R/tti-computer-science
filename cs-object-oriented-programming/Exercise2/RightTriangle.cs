using System;

namespace Exercise2
{
    public delegate void TriangleEvent(object sender);

    public class RightTriangle
    {
        public event TriangleEvent PerimiterRequested;

        public RightTriangle(double x, double y)
        { X = x; Y = y; }

        public double X { get; protected set; }
        public double Y { get; protected set; }
        public virtual double Z
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
            protected set { }
        }

        public double Perimiter
        {
            get
            {
                // if (PerimiterRequested != null)
                    PerimiterRequested(this);

                return X + Y + Z;
            }
        }

        public void Display() =>
            Console.WriteLine("{0}: X={1}; Y={2}; Z={3}; P={4}", GetType(), X, Y, Z, Perimiter);

    }
}
