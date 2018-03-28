package edu.tsi.exercise3;

import java.util.Random;

public class Customer extends Thread {

    private Bakery bakery;
    private static Random random = new Random();

    private int maxMuffinsPerGet, maxInterval;

    Customer(Bakery bakery, int maxMuffinsPerGet, int maxInterval) {
        this.bakery = bakery;
        this.maxMuffinsPerGet = maxMuffinsPerGet;
        this.maxInterval = maxInterval;

        System.out.println("Customer entered the bakery");
    }

    @Override
    public void run() {

        try {
            while (bakery.getIsOpened()) {
                if (bakery.getMuffins() > 0) {

                    int requested = random.nextInt(maxMuffinsPerGet) + 1;
                    int received = bakery.get(requested);

                    if (requested == received) {
                        System.out.println(String.format("Customer bought: %s muffins", received));
                    } else {
                        System.out.println(String.format("Customer wants to buy: %s muffins, but actually bought %s", requested, received));
                    }

                }

                sleep(random.nextInt(maxInterval));
            }
        } catch (InterruptedException e) {
            System.out.println("Customer thread was interrupted!");
        } finally {
            bakery.setIsOpened(false);
            System.out.println("Customer left the bakery");
        }

    }

}
