package edu.tsi.exercise4;

import edu.tsi.exercise4.dao.WorkplaceDao;
import edu.tsi.exercise4.dao.WorkplaceStack;
import edu.tsi.exercise4.dao.EmployeeDao;
import edu.tsi.exercise4.model.Workplace;
import edu.tsi.exercise4.dao.EmployeeRegistry;
import edu.tsi.exercise4.model.Employee;

import java.util.List;

@SuppressWarnings("WeakerAccess")
public class Office implements WorkplaceDao, EmployeeDao {

	private String name;

	private WorkplaceStack workplaceStack;
	private EmployeeRegistry employeeRegistry;

	public Office(String name) {
		this.setName(name);
		this.workplaceStack = new WorkplaceStack();
		this.employeeRegistry = new EmployeeRegistry();
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public List<Workplace> getWorkplaces() {
		return workplaceStack.getWorkplaces();
	}

	public List<Employee> getEmployees() {
		return employeeRegistry.getEmployees();
	}

	public void hireEmployee(Employee p1) {
		this.employeeRegistry.hireEmployee(p1);
	}

	public void fireEmployee(Employee p1) {
		this.employeeRegistry.fireEmployee(p1);
	}

	public void destroyWorkplace(Workplace workplace) {
		this.workplaceStack.destroyWorkplace(workplace);
	}

	public void addWorkplace(Workplace workplace) {
		this.workplaceStack.addWorkplace(workplace);
	}


	public boolean book(Workplace workplace, Employee employee) {
		return workplaceStack.book(workplace, employee);
	}

	public boolean cancelBooking(Workplace workplace) {
		return workplaceStack.cancelBooking(workplace);
	}

	public List<Workplace> getWorkplacesOfEmployee(Employee employee) {
		return this.workplaceStack.getWorkplacesOfEmployee(employee);
	}

	public List<Workplace> getAvailableWorkplaces() {
		return this.workplaceStack.getAvailableWorkplaces();
	}

	public List<Workplace> getBookedWorkplaces() {
		return this.workplaceStack.getBookedWorkplaces();
	}

	public String toString() {
		return this.getName() + ": " + this.getWorkplaces().size() + " books; " + this.getEmployees().size() + " people.";
	}


	public void printStatus(){
		System.out.println("Status Report of Office \n" + this.toString());
		for (Workplace thisWorkplace : this.getWorkplaces()){
			System.out.println(thisWorkplace);
		}
		
		for(Employee p : this.getEmployees()){
			int count = this.getWorkplacesOfEmployee(p).size();
			System.out.println(p + " (has booked" + count + " workplaces)");
		}
		System.out.println("Workplaces available: " + this.getAvailableWorkplaces().size());
		System.out.println("--- End of Status Report ---");
	}
		
}
