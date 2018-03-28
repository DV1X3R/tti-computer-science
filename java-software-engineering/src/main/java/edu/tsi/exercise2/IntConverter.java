package edu.tsi.exercise2;

public class IntConverter {

    public static String intToHex(int i) {              // 1

        if (i < 0)                                      // 2
            throw new IllegalArgumentException();       // 3
        else if (i == 0)                                // 4
            return "0x0";                               // 5

        StringBuilder result = new StringBuilder();

        while (i >= 1) {                                // 6
            char c;

            if (i % 16 >= 10)                           // 7
                c = (char) ('A' + ((i % 16) % 10));     // 8
            else
                c = (char) ('0' + (i % 16));            // 9

            result.append(c);                           // 10
            i /= 16;                                    // 11
        }

        result.reverse();                               // 12

        return "0x" + result.toString();                // 13
    }

}
