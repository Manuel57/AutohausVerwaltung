package com.htlvil.model;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class CustomerUserData {

	private String username;
	private String password;
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public CustomerUserData(String username, String password) {
		super();
		this.username = username;
		this.password = password;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password= password;
	}
}
