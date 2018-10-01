package edu.tsi.exercise3;

import java.util.Random;

public class Baker extends Thread {

    private Bakery bakery;
    private static Random random = new Random();

    private int muffinsToBake, maxMuffinsPerBake, maxInterval;

    Baker(Bakery bakery, int muffinsToBake, int maxMuffinsPerBake, int maxInterval) {
        this.bakery = bakery;
        this.muffinsToBake = muffinsToBake;
        this.maxMuffinsPerBake = maxMuffinsPerBake;
        this.maxInterval = maxInterval;

        bakery.setIsOpened(true);
        System.out.println("Baker opened the bakery");
    }

    @Override
    public void run() {

        try {
            while (muffinsToBake > 0) {
                if (bakery.getMuffinsCount() == 0) {

                    int baked = random.nextInt(maxMuffinsPerBake) + 1;
                    bakery.put(baked);
                    muffinsToBake -= baked;

                    System.out.println("Baker baked: " + baked);
                }

                sleep(random.nextInt(maxInterval));
            }
        } catch (InterruptedException e) {
            System.out.println("Baker thread was interrupted!");
        } finally {
            bakery.setIsOpened(false);
            System.out.println("Baker closed the bakery");
        }

    }

}
