package edu.tsi.exercise3;

import org.junit.Test;

import static org.junit.Assert.*;

public class CArrayTest {

    @Test // 11
    public void array_isNull() {
        CArray array = new CArray(null);
        assertTrue(array.isNull());
        assertTrue(array.isEmpty());
    }

    @Test // 10
    public void array_NotFound_Multi() {
        CArray array = new CArray(new int[]{11, 12, 13});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(-1, array.getFirstIndex(14));
    }

    @Test // 9
    public void array_NotFound_Single() {
        CArray array = new CArray(new int[]{42});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(-1, array.getFirstIndex(14));
    }

    @Test // 8
    public void array_Last_Odd_Multi() {
        CArray array = new CArray(new int[]{11, 12, 13});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(2, array.getFirstIndex(13));
        assertEquals(2, array.getPosition(13));
        assertFalse(array.isEven(2));
    }

    @Test // 7
    public void array_Last_Even_Multi() {
        CArray array = new CArray(new int[]{11, 12, 14});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(2, array.getFirstIndex(14));
        assertEquals(2, array.getPosition(14));
        assertTrue(array.isEven(2));
    }

    @Test // 6
    public void array_Middle_Odd_Multi() {
        CArray array = new CArray(new int[]{11, 13, 14});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(1, array.getFirstIndex(13));
        assertEquals(1, array.getPosition(13));
        assertFalse(array.isEven(1));
    }

    @Test // 5
    public void array_Middle_Even_Multi() {
        CArray array = new CArray(new int[]{11, 12, 13});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(1, array.getFirstIndex(12));
        assertEquals(1, array.getPosition(12));
        assertTrue(array.isEven(1));
    }

    @Test // 4
    public void array_First_Odd_Multi() {
        CArray array = new CArray(new int[]{11, 12, 13});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(0, array.getFirstIndex(11));
        assertEquals(0, array.getPosition(11));
        assertFalse(array.isEven(0));
    }

    @Test // 3
    public void array_First_Even_Multi() {
        CArray array = new CArray(new int[]{10, 12, 13});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(0, array.getFirstIndex(10));
        assertEquals(0, array.getPosition(10));
        assertTrue(array.isEven(0));
    }

    @Test // 2
    public void array_Odd_Single() {
        CArray array = new CArray(new int[]{43});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(0, array.getFirstIndex(43));
        assertFalse(array.isEven(0));
    }

    @Test // 1
    public void array_Even_Single() {
        CArray array = new CArray(new int[]{42});
        assertFalse(array.isNull());
        assertFalse(array.isEmpty());
        assertEquals(0, array.getFirstIndex(42));
        assertTrue(array.isEven(0));
    }

}
