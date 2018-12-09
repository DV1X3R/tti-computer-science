using System;
using ExceptionsLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Solve()
        {
            int radius = 0;
            do { Console.Write("Please enter a cylinders radius> "); }
            while (!int.TryParse(Console.ReadLine(), out radius));
            if (radius < 0) { throw new NegativeRadiusException($"Incorrect radius value: {radius}"); }

            int height = 0;
            do { Console.Write("Please enter a cylinders height> "); }
            while (!int.TryParse(Console.ReadLine(), out height));
            if (height < 0) { throw new NegativeHeightException($"Incorrect height value: {height}"); }

            Console.WriteLine($"S = 2PI * r * (h + r) = {2 * Math.PI * radius * (height + radius)}");
        }

        static void Main(string[] args)
        {
            try { Solve(); }
            catch(NegativeRadiusException e)
            {
                Console.WriteLine("Negative Radius Exception");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");
            }
            catch(NegativeHeightException e)
            {
                Console.WriteLine("Negative Height Exception");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");
            }

            Console.ReadLine();
        }
    }
}
