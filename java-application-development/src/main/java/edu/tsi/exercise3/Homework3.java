package edu.tsi.exercise3;

public class Homework3 {

    public static void main(String[] args) {
        Bakery bakery = new Bakery();
        new Baker(bakery, 100, 10, 2000).start();
        new Customer(bakery, 10, 2000).start();
    }

}
