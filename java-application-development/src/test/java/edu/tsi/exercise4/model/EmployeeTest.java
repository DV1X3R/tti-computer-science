package edu.tsi.exercise4.model;

import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class EmployeeTest {

    Employee employee;

    @Before
    public void setUp() {
        employee = new Employee();
    }

    @Test
    public void setName_TestName() {
        employee.setName("Test Name");
        assertEquals("Test Name", employee.getName());
    }

    @Test
    public void setMaximumWorkplaces_28() {
        employee.setMaximumWorkplaces(28);
        assertEquals(28, employee.getMaximumWorkplaces());
    }

    @Test
    public void toString_TestName_28() {
        employee.setName("Test Name");
        employee.setMaximumWorkplaces(28);
        assertEquals("Test Name, max: 28", employee.toString());
    }

}
