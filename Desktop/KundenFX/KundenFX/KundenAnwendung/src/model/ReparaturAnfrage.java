package model;

import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReparaturAnfrage {

	private User user = null;
	private List<String> leistungen = null;
	private String standort = "";
	private String customMessage = "";
	private List<LocalDate> datesToCheck = null;

	
	public String getStandort() {
		return standort;
	}

	public void setStandort(String standort) {
		this.standort = standort;
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		if (user != null) {
			this.user = user;
		}

	}

	public String getCustomMessage() {
		return customMessage;
	}

	public void setCustomMessage(String customMessage) {
		if (customMessage != null && !customMessage.isEmpty()) {
			this.customMessage = customMessage;
		} else {
			customMessage = GlobalVars.DEFAULTCUSTOMMESSAGE;
		}
	}	

	private List<String> getLeistungen() {
		return leistungen;
	}

	public void setLeistungen(List<String> leistungen) {
		if(leistungen != null)
		this.leistungen = leistungen;
	}
	
	

	private List<LocalDate> getDatesToCheck() {
		return datesToCheck;
	}

	public void setDatesToCheck(List<Date> datesToCheck) {
	
		if(datesToCheck != null) {
			for(Date d : datesToCheck) {
				this.datesToCheck.add(d.toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
			}
		}
		
			
	}

	public ReparaturAnfrage(User user, String customMessage) {
		this.leistungen = new ArrayList<String>();
		this.datesToCheck = new ArrayList<LocalDate>();
		setUser(user);
		setCustomMessage(customMessage);
	}

}
