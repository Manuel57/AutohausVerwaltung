package com.htlvil.controller.database;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.htlvil.controller.DatabaseInformation;
import com.htlvil.model.BillsRequest;
import com.htlvil.model.CustomerUserData;

public class Database {

	private final static Database instance = new Database();
	private Connection connection = null;
	private String getUserStmtString = "select username, password from kunde where username = ? and password = ?";
	private String qryGetAngebote = "select bezeichnung from reparaturart r join reparaturangebot ra on r.reparaturartid=ra.reparaturartid where standort = ?";
	private String qryChangePassword = "update kunde set password=? where username =?";
	private String qryGetBills = "select text from rechnungdocs where ( to_date(trim(substr(title,INSTR(title,'_')+1))) between to_date(?) and to_date(?)) and title like (select kundenid from kunde where username = ?)||'%'";
	private String qryFindTermin = "select to_char(datum,'dd.mm.yy') from (select datum, count(*) anz from kundrechhilfe group by datum ) where anz < 5 and to_char(datum,'dd.mm.yy') = ?";
	private String qryInsertRechnung = "insert into rechnung (rechnungsnummer, rdatum, kundenid, ausgestellt) values (?, to_date(?,'dd.mm.yyyy'), (select kundenid from kunde where username = ?),0)";
	private String qryInsertRechHilfe = "insert into kundrechhilfe (kundenid, rechnungsnummer, custommessage, datum, standort) values((select kundenid from kunde where username = ?), ?,?,to_date(?,'dd.mm.yyyy'),?)";
	private PreparedStatement getUserStmt = null;
	private PreparedStatement getAngeboteStmt = null;
	private PreparedStatement stmpChangePassword = null;
	private PreparedStatement stmtGetDocs = null;
	private PreparedStatement stmtFindTermin = null;
	private PreparedStatement stmtInsertRechnung = null;
	private PreparedStatement stmtInsertRechHilfe = null;
	private static Logger logger = Logger.getLogger("Database");

	private Database() {
		try {
			Class.forName(DatabaseInformation.driver);
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public static Database getInstance() {
		return instance;
	}

	public void connect() throws SQLException {
		if (this.connection == null || this.connection.isClosed()) {
			this.connection = DriverManager.getConnection(DatabaseInformation.url, "d5a09", "d5a");
			this.getUserStmt = this.connection.prepareStatement(this.getUserStmtString,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			this.getAngeboteStmt = this.connection.prepareStatement(this.qryGetAngebote,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			this.stmpChangePassword = this.connection.prepareStatement(this.qryChangePassword,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			this.stmtGetDocs = this.connection.prepareStatement(this.qryGetBills, ResultSet.TYPE_SCROLL_INSENSITIVE,
					ResultSet.CONCUR_UPDATABLE);
			this.stmtFindTermin = this.connection.prepareStatement(this.qryFindTermin,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			this.stmtInsertRechnung = this.connection.prepareStatement(this.qryInsertRechnung,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			this.stmtInsertRechHilfe = this.connection.prepareStatement(this.qryInsertRechHilfe,
					ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		
		}
	}

	public ArrayList<CustomerUserData> getUsers(String un, String pw) throws SQLException {
		ArrayList<CustomerUserData> users = new ArrayList<>();
		this.getUserStmt.setString(1, un);
		this.getUserStmt.setString(2, pw);

		ResultSet rs = this.getUserStmt.executeQuery();
		this.getUserStmt.clearParameters();

		while (rs.next()) {
			users.add(new CustomerUserData(rs.getString(0), rs.getString(1)) {
			});
		}
		this.connection.close();
		return users;
	}

	public ArrayList<String> getStandorte() throws SQLException {
		logger.log(Level.INFO, "entered getstandorte");
		connect();
		Statement stmt = connection.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);
		ResultSet rs = stmt.executeQuery("select standort from werkstatt");
		ArrayList<String> standorte = new ArrayList<String>();
		logger.info("executed select standorte, size: " + rs.getMetaData().getColumnCount());

		while (rs.next()) {
			standorte.add(rs.getString(1));
		}
		logger.info("Number of rows: " + standorte.size());
		this.connection.close();

		return standorte;
	}

	public ArrayList<String> getAngebote(String standort) throws SQLException {
		connect();
		ArrayList<String> angebote = new ArrayList<String>();
		this.getAngeboteStmt.setString(1, standort);
		ResultSet rs = this.getAngeboteStmt.executeQuery();
		while (rs.next()) {
			angebote.add(rs.getString(1));
		}
		this.getAngeboteStmt.clearParameters();
		return angebote;
	}

	public void updatePassword(String username, String password) throws SQLException {
		connect();
		this.stmpChangePassword.setString(1, password);
		this.stmpChangePassword.setString(2, username);
		this.stmpChangePassword.execute();
		this.stmpChangePassword.clearParameters();
		this.connection.close();
	}

	public ArrayList<byte[]> getBills(BillsRequest br) throws SQLException, IOException {
		connect();
		DateTimeFormatter dtf = DateTimeFormatter.ofPattern("d.MM.uu");
		String dF = br.getDateFrom().format(dtf);
		String dT = br.getDateTo().format(dtf);

		this.stmtGetDocs.setString(1, dF);
		this.stmtGetDocs.setString(2, dT);
		this.stmtGetDocs.setString(3, br.getUsername());
		ArrayList<byte[]> bytes = new ArrayList<byte[]>();

		ResultSet rs = this.stmtGetDocs.executeQuery();
		while (rs.next()) {
			byte[] buffer = new byte[1];
			InputStream is = rs.getBinaryStream(1);
			ByteArrayOutputStream bos = new ByteArrayOutputStream();
			while (is.read(buffer) > 0) {
				bos.write(buffer);
			}
			bos.flush();
			bytes.add(bos.toByteArray());
			bos.close();
		}
		this.stmtGetDocs.clearParameters();
		this.connection.close();
		return bytes;
	}

	public String findTermin(List<LocalDate> list, String standort) throws SQLException {
		boolean found = false;
		connect();
		String termin = "";

		for (int i = 0; i < list.size() && found == false; i++) {
			DateTimeFormatter dtf = DateTimeFormatter.ofPattern("dd.MM.uuuu");
			String dF = list.get(i).format(dtf);

			this.stmtFindTermin.setString(1, dF);
			//ResultSet rs = this.stmtFindTermin.executeQuery();
			//while (rs.next()) {
				//found = true;
			//	termin = rs.getString(1);
			//}
			PreparedStatement st = this.connection.prepareStatement("select datum, count(*) anz from kundrechhilfe where to_char(datum,'dd.mm.yyyy')=? and standort = ? group by datum");
			st.setString(1, dF);
			st.setString(2,standort);
			ResultSet rs1 = st.executeQuery();
			boolean empts = true;
			int anz = -1;
			while(rs1.next()) {
				empts = false;
				anz = rs1.getInt(2);
			}
			if(anz < 5) {
				found = true;
			}
			st.clearParameters();
			
			if(found == true)
				termin = dF;
			this.stmtFindTermin.clearParameters();
		}
		this.connection.close();
		return termin;

	}

	public BigDecimal insertRechnung(String username, String date) throws SQLException {
		connect();
		BigDecimal rnr = new BigDecimal(-1);
		Statement stmt = this.connection.createStatement();
		ResultSet rs = stmt.executeQuery("select max(rechnungsnummer)+1 from rechnung");
		while (rs.next()) {
			rnr = rs.getBigDecimal(1);
		}
		
		this.stmtInsertRechnung.setBigDecimal(1, rnr);
		this.stmtInsertRechnung.setString(2, date);
		this.stmtInsertRechnung.setString(3, username);
		
		this.stmtInsertRechnung.execute();
		this.stmtInsertRechnung.clearParameters();
		this.connection.close();
		return rnr;
	}

	public void insertKundRechHilfe(BigDecimal rnr, String termin, String standort, String message, String username) throws SQLException {
		connect();
		this.stmtInsertRechHilfe.setString(1, username);
		this.stmtInsertRechHilfe.setBigDecimal(2, rnr);
		this.stmtInsertRechHilfe.setString(3, message);
		this.stmtInsertRechHilfe.setString(4, termin);
		this.stmtInsertRechHilfe.setString(5, standort);
		this.stmtInsertRechHilfe.execute();
		this.stmtInsertRechHilfe.clearParameters();
		this.connection.close();
	}

}
