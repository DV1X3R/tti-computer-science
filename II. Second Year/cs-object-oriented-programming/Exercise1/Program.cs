using System;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            CDate date = new CDate(2004, 8, 31); // Инициализация числами

            Console.WriteLine(String.Format("Day: {0} / Month: {1} / Year: {2}", date.Day, date.Month, date.Year));
            Console.WriteLine("Is current year a leap year: " + date.IsCurrentYearLeap);
            Console.WriteLine("Subtract 2003/08/31: " + date.SubtractDates(new DateTime(2003, 8, 31)));
            Console.WriteLine("Subtract 5 days: " + date.SubtractDays(5).ToShortDateString());
            Console.WriteLine(date.CompareDates((new DateTime(2005, 8, 31))));

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
