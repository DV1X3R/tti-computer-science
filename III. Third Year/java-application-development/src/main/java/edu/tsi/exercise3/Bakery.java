package edu.tsi.exercise3;

public class Bakery {

    private int muffins;
    private boolean isOpened;

    public int getMuffinsCount() {
        return muffins;
    }

    public boolean getIsOpened() {
        return isOpened;
    }

    public void setIsOpened(boolean value) {
        isOpened = value;
    }

    public Bakery() {
        muffins = 0;
        setIsOpened(false);
    }

    synchronized int get(int amount) {
        if (muffins < amount) {
            int available = muffins;
            muffins = 0;
            return available;
        } else {
            muffins -= amount;
            return amount;
        }
    }

    synchronized void put(int amount) {
        muffins += amount;
    }

}
