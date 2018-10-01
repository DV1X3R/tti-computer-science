package edu.tsi.exercise1;

import java.util.Scanner;

public class Homework1 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Enter a year: ");
        CDate date = GetProgrammersDate(scanner.nextInt());
        System.out.println("Programmers day will be on: " + date.toString());
    }

    private static CDate GetProgrammersDate(int Year) {
        CDate date = new CDate(Year, 1, 1);
        date.addDays(255);
        return date;
    }

}
