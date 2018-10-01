using System;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] incidentList = new int[12, 4] {
                { 1, 4, -1, -1 }, { 0, 4, 6, -1 },   { 3, 7, -1, -1 },      //0-1-2
                { 2, 7, -1, -1 }, { 0, 1, 5, 9 },    { 4, -1, -1, -1 },     //3-4-5
                { 1, 9, 11, -1 }, { 2, 3, 10, 11 },  { 9, -1, -1, -1 },     //6-7-8
                { 4, 6, 8, -1 },  { 7, -1, -1, -1 }, { 6, 7, -1, -1 } };    //9-10-11

            char[] name = new char[12] { 'd', 'e', 'n', 'n', 'y', 'd', 'v', '1', 'x', '3', 'r', '.' };

            Graph graph = new Graph(incidentList, name);

            Console.WriteLine("\tIncidence List:");
            graph.ShowIncidenceList();

            Console.Write("\nCalculated Path (width): ");
            graph.FillPathWidth();
            graph.ShowPath();

            Console.Write("Source name: ");
            graph.ShowSourceName();

            Console.Write("Sorted name: ");
            graph.SortName();
            graph.ShowSortedName();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

    }
}
