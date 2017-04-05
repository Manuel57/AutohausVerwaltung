package com.htlvil.model;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReparaturAnfrage {

	private CustomerUserData user = null;
	private List<String> leistungen = null;
	private String customMessage = "";
	private List<LocalDate> datesToCheck = null;
	private String standort;

	public String getStandort() {
		return standort;
	}

	public void setStandort(String standort) {
		this.standort = standort;
	}

	public CustomerUserData getUser() {
		return user;
	}

	public void setUser(CustomerUserData user) {
		if (user != null) {
			this.user = user;
		}

	}

	public String getCustomMessage() {
		return customMessage;
	}

	public void setCustomMessage(String customMessage) {
		this.customMessage = customMessage;
		
	}	

	public List<String> getLeistungen() {
		return leistungen;
	}

	public void setLeistungen(List<String> leistungen) {
		if(leistungen != null)
		this.leistungen = leistungen;
	}
	
	

	public List<LocalDate> getDatesToCheck() {
		return datesToCheck;
	}

	public void setDatesToCheck(List<LocalDate> datesToCheck) {
		if(datesToCheck != null)
		this.datesToCheck = datesToCheck;
	}

	public ReparaturAnfrage(CustomerUserData user, String customMessage) {
		this.leistungen = new ArrayList<String>();
		this.datesToCheck = new ArrayList<LocalDate>();
		setUser(user);
		setCustomMessage(customMessage);
	}

}
