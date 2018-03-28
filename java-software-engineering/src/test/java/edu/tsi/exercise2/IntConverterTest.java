package edu.tsi.exercise2;

import org.junit.*;

import static org.junit.Assert.*;

public class IntConverterTest {

    // 1-2-4-6-7-8-10-11-6-12-13
    @Test
    public void intToHex_189_0xBD() {
        assertEquals("0xBD", IntConverter.intToHex(189));
    }

    // 1-2-4-6-7-9-10-11-6-12-13
    @Test
    public void intToHex_20_0x14() {
        assertEquals("0x14", IntConverter.intToHex(20));
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
