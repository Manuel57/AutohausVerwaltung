package com.htlvil.lam.kundenanwendung.model;

/**
 * Created by Manuel on 27.03.2017.
 */

public class Angebot {
    String name = null;
    boolean selected = false;

    public Angebot(String name, boolean selected) {
        super();
        this.name = name;
        this.selected = selected;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isSelected() {
        return selected;
    }

    public void setSelected(boolean selected) {
        this.selected = selected;
    }
}
