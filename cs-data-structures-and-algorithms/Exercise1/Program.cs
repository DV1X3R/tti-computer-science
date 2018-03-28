using System;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            uint size;
            do Console.Write(">Enter the size of array> ");
            while (!UInt32.TryParse(Console.ReadLine(), out size));

            IntHashArray array = new IntHashArray(size, 0, 1001);
            Console.WriteLine("\tGenerated array: " + array.Array);
            Console.WriteLine("\tHashed array: " + array.HashedArray);

            int element;
            do Console.Write(">Enter the integer number to search for> ");
            while (!Int32.TryParse(Console.ReadLine(), out element));

            Console.WriteLine("\tSearch performed in the generated array:");
            ArraySearch(array, element, false);
            Console.WriteLine("\n>Search performed in the hashed array: ");
            ArraySearch(array, element, true);

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static void ArraySearch(IntHashArray Array, int Element, bool Hashed)
        {
            Array.ResetCompOpCounter();
            int index;

            if (Hashed)
                index = Array.FindInHashedArray(Element);
            else
                index = Array.FindInArray(Element);

            if (index != -1)
                Console.WriteLine("\tElement: " + Element + "\tFirst occurrence index: " + index);
            else
                Console.WriteLine("\tElement not found!");

            Console.WriteLine("\tComparison operations: " + Array.CompOpCounter);
        }

    }
}
