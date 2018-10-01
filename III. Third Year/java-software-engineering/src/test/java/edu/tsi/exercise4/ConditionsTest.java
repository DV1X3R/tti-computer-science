package edu.tsi.exercise4;

import org.junit.Test;

import static org.junit.Assert.*;

public class ConditionsTest {

    @Test
    public void charp() {
        assertEquals("csharp", Conditions.languageSelector(false, false).toString());
    }

    @Test
    public void python() {
        assertEquals("python", Conditions.languageSelector(false, true).toString());
    }

    @Test
    public void c() {
        assertEquals("c", Conditions.languageSelector(true, false).toString());
    }

    @Test
    public void java() {
        assertEquals("java", Conditions.languageSelector(true, true).toString());
    }

}
