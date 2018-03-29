package edu.tsi.exercise4.dao;

import edu.tsi.exercise4.model.Employee;
import edu.tsi.exercise4.model.Workplace;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Created by roman.kulikov on 6/13/2017.
 * All rights reserved =D
 */
public class WorkplaceStack implements WorkplaceDao {

    private List<Workplace> workplaces = new ArrayList<>();

    @Override
    public List<Workplace> getWorkplaces() {
        return workplaces;
    }
    public void destroyWorkplace(Workplace workplace) {
        workplaces.remove(workplace);
    }

    public void addWorkplace(Workplace b1) {
        this.workplaces.add(b1);
    }

    public boolean book(Workplace workplace, Employee employee) {
        if (workplace == null) {return false;}
        int workplacesBookedCount = this.getWorkplacesOfEmployee(employee).size();
        boolean canBook = (workplace.getEmployee() == null) && (workplacesBookedCount < employee.getMaximumWorkplaces());
        if (canBook) {
            workplace.setEmployee(employee);
        }
        return canBook;

    }

    public boolean cancelBooking(Workplace workplace) {
        if (workplace == null) {return false;}
        boolean canCancel = workplace.getEmployee() != null;
        if (canCancel) {
            workplace.setEmployee(null);
        }
        return canCancel;
    }

    public List<Workplace> getWorkplacesOfEmployee(Employee p1) {

        return this.getWorkplaces().stream().filter(aBook->{
            boolean bookCheckedOut = aBook.getEmployee() != null;
            return bookCheckedOut && aBook.getEmployee().getName().equals(p1.getName());
        }).collect(Collectors.toList());
    }

    public List<Workplace> getAvailableWorkplaces() {
        return this.getWorkplaces().stream()
                .filter(wp-> wp.getEmployee() == null)
                .collect(Collectors.toList());
    }

    public List<Workplace> getBookedWorkplaces() {
        return this.getWorkplaces().stream()
                .filter(wp-> wp.getEmployee() != null)
                .collect(Collectors.toList());
    }

}
