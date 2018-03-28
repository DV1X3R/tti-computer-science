namespace Exercise2
{
    public class Triangle : RightTriangle
    {
        public Triangle(double x, double y, double z)
            : base(x, y) { Z = z; }
        public override double Z { get; protected set; }
    }
}
