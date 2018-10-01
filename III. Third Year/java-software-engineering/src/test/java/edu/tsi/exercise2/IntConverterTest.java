package edu.tsi.exercise2;

import org.junit.Test;

import static org.junit.Assert.*;

public class IntConverterTest {

    @Test
    public void intToHex_26_0x1A() {
        assertEquals("0x1A", IntConverter.intToHex(26));
    }

    @Test
    public void intToHex_0_0x0() {
        assertEquals("0x0", IntConverter.intToHex(0));
    }

    @Test(expected = IllegalArgumentException.class)
    public void intToHex_Negative_Exception() {
        IntConverter.intToHex(-1);
    }

}
