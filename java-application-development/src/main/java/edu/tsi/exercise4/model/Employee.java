package edu.tsi.exercise4.model;

public class Employee {
	private static final int MAXIMUM_BOOKED_WORKPLACES_AMOUNT = 3;
	//fields
	private String name; //name of the employee
	private int maximumWorkplaces; // most books the person can check out
	
	//constructors

	public Employee(){
		this.name = "unknown name";
		maximumWorkplaces = MAXIMUM_BOOKED_WORKPLACES_AMOUNT;
	}

	public Employee(String name){
		this.name = name;
		maximumWorkplaces = MAXIMUM_BOOKED_WORKPLACES_AMOUNT;
	}

	//methods
	public String getName(){
		return name;	
	}
	
	public void setName(String anyName){
		name = anyName;
	}

	public int getMaximumWorkplaces() {
		return maximumWorkplaces;
	}

	public void setMaximumWorkplaces(int maximumWorkplaces) {
		this.maximumWorkplaces = maximumWorkplaces;
	}	
	
	public String toString(){
		return this.getName() + ", max: " + this.getMaximumWorkplaces();
	}
}
