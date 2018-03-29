package edu.tsi.exercise4.dao;

import edu.tsi.exercise4.model.Employee;
import edu.tsi.exercise4.model.Workplace;

import java.util.List;

/**
 * Created by roman.kulikov on 6/13/2017.
 * All rights reserved =D
 */
public interface WorkplaceDao {
    void destroyWorkplace(Workplace workplace);

    void addWorkplace(Workplace b1);

    boolean book(Workplace workplace, Employee employee);
    boolean cancelBooking(Workplace workplace);

    List<Workplace> getWorkplacesOfEmployee(Employee p1);

    List<Workplace> getWorkplaces();
    List<Workplace> getAvailableWorkplaces();
    List<Workplace> getBookedWorkplaces();
}
