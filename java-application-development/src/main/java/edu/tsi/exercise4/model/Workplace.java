package edu.tsi.exercise4.model;

public class Workplace {
	private String id;
	private Employee employee;

	public Workplace(String id) {
		this.id = id;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public void setEmployee(Employee employee) {
		this.employee = employee;
	}

	public Employee getEmployee() {
		return this.employee;
	}
	
	public String toString() {
		String available;
		if (this.getEmployee() == null){
			available = "available";
		}
		else{
			available = "booked by " + this.getEmployee().getName();
		}
		return "Workplace # " + this.getId() + " (" + available + ")";
	}

}
