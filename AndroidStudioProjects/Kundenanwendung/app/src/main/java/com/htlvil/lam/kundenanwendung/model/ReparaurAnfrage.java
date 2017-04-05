package com.htlvil.lam.kundenanwendung.model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by Manuel on 27.03.2017.
 */

public class ReparaurAnfrage {


    private User user = null;
    private List<String> leistungen = null;
    private String customMessage = "";
    private List<Datum> datesToCheck = null;
    private String standort = "";

    public ReparaurAnfrage(User user, String customMessage) {
        this.leistungen = new ArrayList<String>();
        this.datesToCheck = new ArrayList<Datum>();
        setUser(user);
        setCustomMessage(customMessage);
    }

    public void setStandort(String standort) {
        this.standort=standort;
    }
    public String getStandort() {
        return this.standort;
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
            customMessage = "";
        }
    }

    private List<String> getLeistungen() {
        return leistungen;
    }

    public void setLeistungen(List<String> leistungen) {
        if (leistungen != null)
            this.leistungen = leistungen;
    }


    private List<Datum> getDatesToCheck() {
        return datesToCheck;
    }

    public void setDatesToCheck(List<Datum> datesToCheck) {
        if (datesToCheck != null)
            this.datesToCheck = datesToCheck;
    }



}


