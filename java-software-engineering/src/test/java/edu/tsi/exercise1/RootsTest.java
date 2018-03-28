package edu.tsi.exercise1;

import java.io.*;
import java.util.Scanner;

import org.junit.*;

import static org.junit.Assert.*;

public class RootsTest {

    Roots roots;

    @Before
    public void setUp() {
        roots = new Roots();
    }

    @Test
    public void calculateRoots() throws IOException {

        File testFile = new File(getClass().getResource("res/test-input.txt").getFile());
        String testData;

        try (Scanner s = new Scanner(testFile).useDelimiter("\\Z")) {
            testData = s.next();
        }

        String[] tests = testData.split("\n");
        double[][] parameters = new double[tests.length][6];

        for (int i = 0; i < tests.length; i++)
            for (int p = 0; p < 6; p++)
                parameters[i][p] = Double.parseDouble(tests[i].split(",")[p]);

        for (int i = 0; i < tests.length; i++) {
            double a = parameters[i][0];
            double b = parameters[i][1];
            double c = parameters[i][2];
            double hasValues = parameters[i][3];
            double x1 = parameters[i][4];
            double x2 = parameters[i][5];

            System.out.println("Testing: " + a + "x^2 + " + b + "x + " + c + " = 0   \t|   Has Values = " + hasValues + "; x1 = " + x1 + "; x2 = " + x2);

            roots.calculateRoots(a, b, c);

            if (hasValues == 1)
                assertTrue(roots.hasValues);
            else
                assertFalse(roots.hasValues);

            assertEquals(x1, roots.x1, 0.01);
            assertEquals(x2, roots.x2, 0.01);
        }

    }

}
