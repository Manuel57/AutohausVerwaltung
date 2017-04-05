package com.htlvil.lam.kundenanwendung.model;

/**
 * Created by Manuel on 28.03.2017.
 */

public class Datum {
    private String year;

    public Datum(String year, String month, String day) {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    public String getYear() {
        return year;
    }

    public void setYear(String year) {
        this.year = year;
    }

    public String getMonth() {
        return month;
    }

    public void setMonth(String month) {
        this.month = month;
    }

    public String getDay() {
        return day;
    }

    public void setDay(String day) {
        this.day = day;
    }

    private String month;
    private String day;
}
