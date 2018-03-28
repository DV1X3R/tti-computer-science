using System;

namespace Exercise1
{
    public class CDate
    {
        // Параметры
        private uint year; private uint month; private uint day;

        // Cвойства
        public uint Year { get { return year; } set { year = value; } }
        public uint Month { get { return month; } set { month = value; } }
        public uint Day { get { return day; } set { day = value; } }

        // Конструкторы
        public CDate(uint SourceYear, uint SourceMonth, uint SourceDay)
        { Year = SourceYear; Month = SourceMonth; Day = SourceDay; }

        public CDate(DateTime SourceDate) : this((uint)SourceDate.Year, (uint)SourceDate.Month, (uint)SourceDate.Day) { }
        public CDate(String SourceDate) : this(Convert.ToDateTime(SourceDate)) { }

        // Определение високосности года
        public bool IsCurrentYearLeap { get { return DateTime.IsLeapYear((int)Year); } }

        // Вычитаение заданного количества дней из даты и вычисленеие даты через заданное количество дней
        public DateTime SubtractDays(int daysAmount)
        {
            DateTime temporaryDate = new DateTime((int)Year, (int)Month, (int)Day);
            return temporaryDate.AddDays(daysAmount * -1);
        }

        // Сравнение дат
        public string CompareDates(DateTime userDate)
        {
            DateTime temporaryDate = new DateTime((int)Year, (int)Month, (int)Day);

            if (temporaryDate > userDate)
                return temporaryDate.ToShortDateString() + " > " + userDate.ToShortDateString();
            else if (temporaryDate < userDate)
                return temporaryDate.ToShortDateString() + " < " + userDate.ToShortDateString();
            else return "Dates are equal";
        }

        // Вычисление количества дней между датами
        public int SubtractDates(DateTime UserDate)
        {
            DateTime temporaryDate = new DateTime((int)Year, (int)Month, (int)Day);
            return (int)temporaryDate.Subtract(UserDate).TotalDays;
        }

    }
}
