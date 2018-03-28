package edu.tsi.exercise1;

public class Roots {
    public double x1, x2;
    public boolean hasValues;

    public void calculateRoots(double a, double b, double c) {   // 1

        double d = Math.pow(b, 2) - (4 * a * c);        // 1

        if (d < 0) {                                    // 2
            hasValues = false;                          // 3
            x1 = x2 = 0;                                // 3
        } else if (d == 0) {                            // 4
            hasValues = true;                           // 5
            x1 = x2 = (b * -1) / (2 * a);               // 5
        } else {                                        // 6
            hasValues = true;                           // 7
            x1 = (b * -1 + Math.sqrt(d)) / (2 * a);     // 7
            x2 = (b * -1 - Math.sqrt(d)) / (2 * a);     // 7
        }                                               // 8
    }                                                   // 8

}
