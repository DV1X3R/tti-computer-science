using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            IntList list = new IntList(10, 10, 101);
            Console.WriteLine("\tGenerated list:");
            list.RunRight();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.WriteLine("\tInserting \"500, 404, 403\" into this list after the third element:");
            list.Insert(3, 500);
            list.Insert(3, 404);
            list.Insert(3, 403);
            list.RunRight();
            Console.WriteLine(String.Format("\tAverage value: {0}", list.Average));

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.WriteLine("\tRemoving the fifth element:");
            list.Remove(4);
            list.RunRight();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

    }
}
