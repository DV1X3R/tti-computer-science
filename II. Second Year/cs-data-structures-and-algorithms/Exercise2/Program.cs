using System;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            uint size;
            do Console.Write(">Enter the size of array> ");
            while (!UInt32.TryParse(Console.ReadLine(), out size));

            IntSortArray array = new IntSortArray(size, 100, 1001);
            Console.WriteLine("\tGenerated array: " + array.Array);

            array.ResetCompOpCounter();
            array.SortArrayShell();
            Console.WriteLine("\tShell sorted array: " + array.SortedArray);
            uint comparisonsShell = array.CompOpCounter;

            array.ResetCompOpCounter();
            array.SortArrayRadix();
            Console.WriteLine("\tRadix sorted array: " + array.SortedArray);
            uint comparisonsRadix = array.CompOpCounter;

            Console.WriteLine("\tShell Comparison operations: " + comparisonsShell);
            Console.WriteLine("\tRadix Comparison operations: " + comparisonsRadix);

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

    }
}
