CREATE TABLE Kunde(
	KundenID     	INTEGER,
	Vorname     	VARCHAR2(30),
	Nachname     	VARCHAR2(30),
	GebDate     	DATE,
	Password     	VARCHAR2(200),
	Username     	VARCHAR2(20),
	PRIMARY KEY (KundenID)
);

CREATE TABLE Marke(
	MarkenID     	VARCHAR2(20),
	Markenname		VARCHAR2(30),
	PRIMARY KEY (MarkenID)
);

CREATE TABLE Type(
	TypenID      		VARCHAR2(20),
	MarkenID     		VARCHAR2(20),
	TypenBezeichnung	VARCHAR2(30),
	KW           		INTEGER,
	Preis        		INTEGER,
	PRIMARY KEY (TypenID),
	FOREIGN KEY (MarkenID) REFERENCES Marke(MarkenID)
);

CREATE TABLE Auto(
	FahrgestellNummer	INTEGER,
	KundenID			INTEGER,
	TypenID				VARCHAR2(20),
	PRIMARY KEY (FahrgestellNummer),
	FOREIGN KEY (KundenID) REFERENCES Kunde(KundenID),
	fOREIGN KEY (TypenID)  REFERENCES TYPE(TypenID)
);

CREATE TABLE Autohaus(
	STANDort     VARCHAR2(100),
	Name    	 VARCHAR2(50),
	Koordinaten	 SDO_GEOMETRY,
	PRIMARY KEY (Standort)
);
INSERT INTO user_sdo_geom_metadata
( TABLE_NAME,
COLUMN_NAME,
DIMINFO,
SRID
)
VALUES
( 'Autohaus',
'coordinates',
 SDO_DIM_ARRAY( 
      SDO_DIM_ELEMENT('x',-10000000000000,10000000000000,0.00005),
      SDO_DIM_ELEMENT('y',-10000000000000,10000000000000,0.00005))
NULL -- SRID
);

CREATE INDEX idx_autohaus ON Autohaus(coordinates) INDEXTYPE IS MDSYS.SPATIAL_INDEX;

CREATE TABLE AutoLager(
	TypenID			VARCHAR2(20),
	Standort		VARCHAR2(100),
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
	Bezeichnung     VARCHAR2(50),
	Preis    	    INTEGER,
	PRIMARY KEY (Bezeichnung)
);

CREATE TABLE DurchgeführteReparatur(
	RepID     		INTEGER,
	Bezeichnung     VARCHAR2(50),
	PRIMARY KEY (RepID,Bezeichnung),
	FOREIGN KEY(REPID) REFERENCES REPARATUR(REPID),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES REPARATURART(BEZEICHNUNG)
);

CREATE TABLE ReparaturAutohaus(
	STANDort     	VARCHAR2(100),
	Bezeichnung     VARCHAR2(50),
	PRIMARY KEY (Standort, Bezeichnung),
	FOREIGN KEY (STANDORT) REFERENCES  AUTOHAUS(STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES REPARATURART(BEZEICHNUNG)
);

CREATE TABLE Autoteile(
	Preis     		INTEGER,
	Bezeichnung     VARCHAR2(50),
	PRIMARY KEY (Bezeichnung)
);

CREATE TABLE Zentrallager(
	Standort     	VARCHAR2(100),
	Koordinaten     SDO_GEOMETRY,
	Name     		VARCHAR2(50),
	PRIMARY KEY (Standort)
);

INSERT INTO user_sdo_geom_metadata
( TABLE_NAME,
COLUMN_NAME,
DIMINFO,
SRID
)
VALUES
( 'Zentrallager',
'koordinaten',
 SDO_DIM_ARRAY( 
      SDO_DIM_ELEMENT('x',-10000000000000,10000000000000,0.0005),
      SDO_DIM_ELEMENT('y',-10000000000000,10000000000000,0.0005))
NULL -- SRID
);

CREATE INDEX idx_zentrallager ON Zentrallager(koordinaten) INDEXTYPE IS MDSYS.SPATIAL_INDEX;

CREATE TABLE AutohausLagerBestand(
	STANDORT     	VARCHAR2(100),
	Bezeichnung     VARCHAR2(50),
	Menge     		INTEGER,
	PRIMARY KEY (Standort, Bezeichnung),
	FOREIGN KEY (STANDORT) REFERENCES AUTOHAUS(STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES AUTOTEILE(BEZEICHNUNG)
);

CREATE TABLE ZentrallagerBestand(
	STANDORT     	VARCHAR2(100),
	Bezeichnung     VARCHAR2(50),
	PRIMARY KEY (Bezeichnung, STANDORT),
	FOREIGN KEY (STANDORT) REFERENCES Zentrallager (STANDORT),
	FOREIGN KEY (BEZEICHNUNG) REFERENCES AUTOTEILE (BEZEICHNUNG)
);

