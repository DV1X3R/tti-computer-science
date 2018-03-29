package edu.tsi.exercise4;

import edu.tsi.exercise4.model.Employee;
import edu.tsi.exercise4.model.Workplace;

/**
 * Created by roman.kulikov on 6/22/2017.
 * All rights reserved =D
 */
public class Application {

    public static void main(String[] args) {
        //create a new Office
        Office office = new Office("Accenture");
        Workplace b1 = initWorkplace(office, "bld-fl-001");
        Workplace b2 = initWorkplace(office, "bld-fl-002");

        Employee jim = initEmployee(office, "Jim");
        Employee sue = initEmployee(office, "Sue");

        System.out.println("Just created new Office");
        office.printStatus();

        System.out.println("Book bld-fl-001 to Sue");
        office.book(b1, sue);
        office.printStatus();

        System.out.println("Do some more stuff");
        office.cancelBooking(b1);
        office.book(b2, jim);
        office.printStatus();

    }

    private static Employee initEmployee(Office office, String employeeName) {
        Employee employee = new Employee("Fred");
        employee.setName(employeeName);
        office.hireEmployee(employee);
        return employee;
    }

    private static Workplace initWorkplace(Office office, String workplaceId) {
        Workplace workplace = new Workplace(workplaceId);
        office.addWorkplace(workplace);
        return workplace;
    }
}
