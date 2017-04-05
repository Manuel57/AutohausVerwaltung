package com.htlvil.lam.kundenanwendung.model;

/**
 * Created by Manuel on 27.03.2017.
 */

public class User {
    private String username;
    private String password;

    public User(String password, String username) {
        this.setUsername(username);
        this.setPassword(password);
    }

    public String getUsername() {
        return this.username;
    }
    public void setUsername(String username ) {
        this.username = username;
    }

    public String getPassword() {
        return this.password;
    }
    public void setPassword(String password) {
        this.password = password;
    }

}
