package edu.tsi.exercise4.model;

import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class WorkplaceTest {

    Workplace workplace;

    @Before
    public void setUp() {
        workplace = new Workplace("28");
    }

    @Test
    public void toString_New_Available() {
        assertEquals("Workplace # 28 (available)", workplace.toString());
    }

    @Test
    public void toString_Employee_Booked() {
        Employee employee = new Employee();
        employee.setName("Test Employee");

        workplace.setEmployee(employee);
        assertEquals("Workplace # 28 (booked by Test Employee)", workplace.toString());
    }

}
