using System;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            RightTriangle[] collection = new RightTriangle[10];
            Random random = new Random();

            for (int i = 0; i < collection.Length; i++)
                collection[i] =
                    i < collection.Length / 2 ?
                        new Triangle(random.Next(1, 10), random.Next(1, 10), random.Next(1, 10)) :
                        new RightTriangle(random.Next(1, 10), random.Next(1, 10));

            foreach (RightTriangle triangle in collection)
            {
                triangle.PerimiterRequested += OnPerimiterRequested; //new TriangleEvent(OnPerimiterRequested);
                triangle.Display();
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static void OnPerimiterRequested(object sender) =>
            Console.WriteLine(sender.ToString() + " has requested perimiter");

    }
}
