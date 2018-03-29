package edu.tsi.exercise1;

public class CDate {

    private int year, month, day;

    public int getYear() {
        return year;
    }

    public int getMonth() {
        return month;
    }

    public int getDay() {
        return day;
    }

    public void setYear(int value) {
        year = value < 1800 ? 1800 : value > 2800 ? 2800 : value;
    }

    public void setMonth(int value) {
        month = value < 1 ? 1 : value > 12 ? 12 : value;
    }

    public void setDay(int value) {
        day = value < 1 ? 1 : value > 31 ? 31 : value;
    }

    public boolean isGregorian() {
        if (year > 1918 || (year == 1918 && month >= 2))
            return true;
        else
            return false;
    }

    public boolean isLeap() {
        if (isGregorian()) {
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
                return true;
            else
                return false;
        } else if (year % 4 == 0)
            return true;
        else
            return false;

    }

    public int daysInMonth() {
        if (month == 2) {
            if (isLeap())
                return 29;
            else
                return 28;
        } else if (month == 8)
            return 31;
        else return month % 2 == 0 ? 30 : 31;
    }

    CDate(int dateYear, int dateMonth, int dateDay) {
        setYear(dateYear);
        setMonth(dateMonth);
        setDay(dateDay);
    }

    @Override
    public String toString() {
        String sMonth = month > 9 ? String.valueOf(month) : "0" + month;
        String sDay = day > 9 ? String.valueOf(day) : "0" + day;
        return sDay + "." + sMonth + "." + year;
    }

    public void addDays(int daysAmount) {
        daysAmount++; // --DaysAmount is ruining one cycle iteration ¯\_(ツ)_/¯
        while (--daysAmount > 0) {

            day++;

            if (day > daysInMonth()) {

                day = 1;
                month++;

                if (month > 12) {
                    month = 1;
                    year++;
                }

            } else if (year == 1918 && month == 2 && (day - 1) == 1) {
                day = 15;
            }

            // System.out.println(">" + toString());
        }
    }

}
