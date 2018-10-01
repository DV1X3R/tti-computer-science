package edu.tsi.exercise3;

public class CArray {

    private int[] array;

    public CArray(int[] array) {
        this.array = array;
    }

    public boolean isNull() {
        return array == null;
    }

    public boolean isEmpty() {
        return (isNull() || array.length == 0);
    }

    public int getFirstIndex(int value) {
        for (int i = 0; i < array.length; i++) {
            if (array[i] == value)
                return i;
        }
        return -1;
    }

    public int getPosition(int value) {
        if (array.length > 0) {
            if (array[0] == value) return 0;  // first
            else if (array[array.length - 1] == value) return 2;  // last
            else if (getFirstIndex(value) != -1) return 1;  // in the middle
        }
        return -1;  // not found
    }

    public boolean isEven(int index) {
        return array[index] % 2 == 0;
    }

}
