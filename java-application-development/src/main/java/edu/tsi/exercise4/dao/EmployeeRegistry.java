package edu.tsi.exercise4.dao;

import edu.tsi.exercise4.model.Employee;

import java.util.ArrayList;

/**
 * Created by roman.kulikov on 6/12/2017.
 * All rights reserved =D
 */
public class EmployeeRegistry implements EmployeeDao {

    private ArrayList<Employee> employees;

    public EmployeeRegistry(){
        employees = new ArrayList<>();
    }

    public ArrayList<Employee> getEmployees() {
        return employees;
    }

    public void hireEmployee(Employee employee){
        this.employees.add(employee);
    }

    public void fireEmployee(Employee employee){
        this.employees.remove(employee);
    }
}
