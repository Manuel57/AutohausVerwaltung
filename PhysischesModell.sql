CREATE TABLE Kunde(
	KundenID     	INTEGER,
	Vorname     	VARCHAR2,
	Nachname     	VARCHAR2,
	GebDate     	DATE,
	Password     	VARCHAR2,
	Username     	VARCHAR2,
	PRIMARY KEY (KundenID)
);

CREATE TABLE Marke(
	MarkenID     	VARCHAR2,
	Markenname		VARCHAR2,
	PRIMARY KEY (MarkenID)
);

CREATE TABLE Type(
	TypenID      		VARCHAR2,
	MarkenID     		VARCHAR2,
	TypenBezeichnung	VARCHAR2,
	KW           		INTEGER,
	Preis        		INTEGER,
	PRIMARY KEY (TypenID),
	FOREIGN KEY (MarkenID) REFERENCES Marke(MarkenID)
);

CREATE TABLE Auto(
	FahrgestellNummer	INTEGER,
	KundenID			INTEGER,
	TypenID				VARCHAR2,
	PRIMARY KEY (FahrgestellNummer),
	FOREIGN KEY (KundenID) REFERENCES Kunde(KundenID),
	fOREIGN KEY (TypenID)  REFERENCES TYPE(TypenID)
);

CREATE TABLE Autohaus(
	STANDort     VARCHAR2,
	Name    	 VARCHAR2,
	Koordinaten	 SDO_GEOMETRY,
	PRIMARY KEY (Standort)
);

CREATE TABLE AutoLager(
	TypenID			VARCHAR2,
	Standort		VARCHAR2,
	Lagerbastand	INTEGER,
	PRIMARY KEY (TypenID, STANDORT),
	FOREIGN KEY (typenID) REFERENCES TYPE(TYPENID),
	FOREIGN KEY (STANDORT) REFERENCES AUTOHAUS(STANDORT)
);

CREATE TABLE Rechnung(
	Rechnungsnummer     INTEGER,
	RDatum    			DATE,
	Gesamtpreis     	INTEGER,	
	PRIMARY KEY (Rechnungsnummer)
);

CREATE TABLE Reparatur(
	RepID     			INTEGER,
	Datum    			DATE,
	RechnungsNr     	INTEGER,
	FahrgestellNummer	INTEGER,
	PRIMARY KEY (RepID),
	FOREIGN KEY (RECHNUNGSNR) REFERENCES RECHNUNG(RECHNUNGSNUMMER),
	FOREIGN KEY (FAHRGESTELLNUMMER) REFERENCES AUTO(FAHRGESTELLNUMMER)
);

CREATE TABLE Reparaturart(
	Bezeichnung     VARCHAR2,
	Preis    	    INTEGER,
	PRIMARY KEY (Bezeichnung)
);

CREATE TABLE DurchgeführteReparatur(
	RepID     		INTEGER,
	Bezeichnung     VARCHAR2,
	PRIMARY KEY (RepID,Bezeichnung),
	FOREIGN KEY(REPID) REFERENCES REPARATUR(REPID),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES REPARATURART(BEZEICHNUNG)
);

CREATE TABLE ReparaturAutohaus(
	STANDort     	VARCHAR2,
	Bezeichnung     VARCHAR2,
	PRIMARY KEY (Standort, Bezeichnung),
	FOREIGN KEY (STANDORT) REFERENCES  AUTOHAUS(STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES REPARATURART(BEZEICHNUNG)
);

CREATE TABLE Autoteile(
	Preis     		INTEGER,
	Bezeichnung     VARCHAR2,
	PRIMARY KEY (Bezeichnung)
);

CREATE TABLE Zentrallager(
	Standort     	VARCHAR2,
	Koordinaten     SDO_GEOMETRY,
	Name     		VARCHAR2,
	PRIMARY KEY (Standort)
);

CREATE TABLE AutohausLagerBestand(
	STANDORT     	VARCHAR2,
	Bezeichnung     VARCHAR2,
	Menge     		INTEGER,
	PRIMARY KEY (Standort, Bezeichnung),
	FOREIGN KEY (STANDORT) REFERENCES AUTOHAUS(STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES AUTOTEILE(BEZEICHNUNG)
);

CREATE TABLE ZentrallagerBestand(
	STANDORT     	VARCHAR2,
	Bezeichnung     VARCHAR2,
	PRIMARY KEY (Bezeichnung, STANDORT),
	FOREIGN KEY (STANDORT) REFERENCES Zentrallager (STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES AUTOTEILE (BEZEICHNUNG)
);

