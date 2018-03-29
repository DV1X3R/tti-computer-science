package edu.tsi.exercise4;

import edu.tsi.exercise4.model.Employee;
import edu.tsi.exercise4.model.Workplace;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class OfficeTest {

    Office office;

    @Before
    public void setUp() {
        office = new Office("Test Office");
    }

    @Test
    public void workplacesProcessing() {
        Workplace wp1 = new Workplace("1");
        Workplace wp2 = new Workplace("2");

        office.addWorkplace(wp1);
        office.addWorkplace(wp2);
        assertEquals(2, office.getWorkplaces().size());

        office.destroyWorkplace(wp1);
        assertEquals(1, office.getWorkplaces().size());

        office.destroyWorkplace(wp2);
        assertEquals(0, office.getWorkplaces().size());
    }

    @Test
    public void employeesBooking() {
        Workplace wp1 = new Workplace("1");
        Workplace wp2 = new Workplace("2");
        Employee emp1 = new Employee("Employee1");
        Employee emp2 = new Employee("Employee2");

        office.addWorkplace(wp1);
        office.addWorkplace(wp2);
        office.hireEmployee(emp1);
        office.hireEmployee(emp2);

        assertTrue(office.book(wp1, emp1));
        assertEquals("Employee1", wp1.getEmployee().getName());

        assertFalse(office.book(wp1, emp2));

        assertTrue(office.cancelBooking(wp1));
        assertEquals(0, office.getBookedWorkplaces().size());

        assertFalse(office.cancelBooking(wp1));
        assertFalse(office.cancelBooking(wp2));
    }

    @Test
    public void maxEmployeeWorkspaces() {
        Workplace wp1 = new Workplace("1");
        Workplace wp2 = new Workplace("2");
        Employee emp1 = new Employee("Employee1");

        office.addWorkplace(wp1);
        office.addWorkplace(wp2);
        office.hireEmployee(emp1);

        emp1.setMaximumWorkplaces(1);

        assertTrue(office.book(wp1, emp1));
        assertFalse(office.book(wp2, emp1));
    }

    @Test
    public void toString_1Books_2People() {
        office.addWorkplace(new Workplace("1"));
        office.hireEmployee(new Employee());
        office.hireEmployee(new Employee());
        assertEquals("Test Office: 1 books; 2 people.", office.toString());
    }

}
