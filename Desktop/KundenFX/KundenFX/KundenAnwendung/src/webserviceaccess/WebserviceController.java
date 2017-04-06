package webserviceaccess;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.lang.reflect.Type;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Base64;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import javax.net.ssl.HttpsURLConnection;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import model.BillsRequest;
import model.GlobalVars;
import model.ReparaturAnfrage;
import model.User;

public class WebserviceController {

	private User currentUser;
	private URL url;
	private HttpsURLConnection con;

	private WebserviceController() {

		currentUser = new User();
	}

	private static WebserviceController instance = new WebserviceController();

	public static WebserviceController getInstance() {
		return instance;
	}

	public User getCurrentUser() {
		return this.currentUser;
	}

	public boolean checkUsername(User check) throws Exception {
		boolean b = false;
		this.currentUser.setPasswort(check.getPassword());
		this.currentUser.setUsername(check.getUsername());
		String userCredentials = check.getUsername() + ":" + check.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);
		String input = new Gson().toJson(check);

		url = new URL(getBaseURI() + "/user/check");
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("POST");
		con.setRequestProperty("Authorization", basicAuth);
		con.setRequestProperty("Content-Type", "application/json");
		con.setDoOutput(true);
		OutputStreamWriter wr = new OutputStreamWriter(con.getOutputStream());
		wr.write(input);
		wr.close();
		System.out.println(input);
		System.out.println(con.getResponseCode());

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String response;
		StringBuffer buffer = new StringBuffer();
		while ((response = in.readLine()) != null) {
			buffer.append(response);
		}
		in.close();

		b = new Gson().fromJson(buffer.toString(), Boolean.class);
		System.out.println(b);
		return b;

	}

	public boolean changePassword(User newUser) throws Exception {
		boolean b = true;

		String userCredentials = this.currentUser.getUsername() + ":" + this.currentUser.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);
		this.currentUser.setPasswort(newUser.getPassword());

		String input = new Gson().toJson(newUser);

		url = new URL(getBaseURI() + "/user/changepw");
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("POST");
		con.setRequestProperty("Authorization", basicAuth);
		con.setRequestProperty("Content-Type", "application/json");
		con.setDoOutput(true);
		OutputStreamWriter wr = new OutputStreamWriter(con.getOutputStream());
		wr.write(input);
		wr.close();
		System.out.println(input);
		System.out.println(con.getResponseCode());
		if (con.getResponseCode() != 200) {
			b = false;
		}

		return b;

	}

	public List<byte[]> getRechnungen(String zeitr, int number) throws Exception {
		Date start = new Date();
		Date end = new Date();

		Calendar c = Calendar.getInstance();
		switch (zeitr) {
		case GlobalVars.WEEK:

			c.set(Calendar.WEEK_OF_YEAR, number);
			c.set(Calendar.DAY_OF_WEEK, Calendar.SUNDAY);
			start = c.getTime();

			c.add(Calendar.DAY_OF_WEEK, 7);
			end = c.getTime();

			break;
		case GlobalVars.MONTH:
			c.set(Calendar.MONTH, number - 1);
			c.set(Calendar.DAY_OF_MONTH, 1);
			start = c.getTime();
			c.set(Calendar.DAY_OF_MONTH, c.getActualMaximum(Calendar.DAY_OF_MONTH));
			end = c.getTime();
			break;
		case GlobalVars.HALFYEAR:
			if (c.get(Calendar.MONTH) < 6) {
				c.set(Calendar.MONTH, 0);
				c.set(Calendar.DAY_OF_MONTH, 1);
				start = c.getTime();
				c.set(Calendar.MONTH, 5);
				c.set(Calendar.DAY_OF_MONTH, c.getActualMaximum(Calendar.DAY_OF_MONTH));
				end = c.getTime();
			} else {
				c.set(Calendar.MONTH, 6);
				c.set(Calendar.DAY_OF_MONTH, 1);
				start = c.getTime();
				c.set(Calendar.MONTH, 11);
				c.set(Calendar.DAY_OF_MONTH, c.getActualMaximum(Calendar.DAY_OF_MONTH));
				end = c.getTime();
			}
			break;
		default:
			break;

		}
		BillsRequest re = new BillsRequest();
		re.setUsername(this.currentUser.getUsername());
		re.setDateFrom(start.toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
		re.setDateTo(end.toInstant().atZone(ZoneId.systemDefault()).toLocalDate());

		String userCredentials = this.currentUser.getUsername() + ":" + this.currentUser.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);
		String input = new Gson().toJson(re);
		List<byte[]> allBills = new ArrayList<byte[]>();

		url = new URL(getBaseURI() + "/main/bills");
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("POST");
		con.setRequestProperty("Authorization", basicAuth);
		con.setRequestProperty("Content-Type", "application/json");
		con.setDoOutput(true);
		OutputStreamWriter wr = new OutputStreamWriter(con.getOutputStream());
		wr.write(input);
		wr.close();
		System.out.println(input);
		System.out.println(con.getResponseCode());

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String response;
		StringBuffer buffer = new StringBuffer();
		while ((response = in.readLine()) != null) {
			buffer.append(response);
		}
		in.close();

		Type collType = new TypeToken<List<byte[]>>() {
		}.getType();
		System.out.println(buffer.toString());
		allBills = new Gson().fromJson(buffer.toString(), collType);

		System.out.println(re.getDateFrom());
		System.out.println(re.getDateTo());
		return allBills;
	}

	public List<String> getAllAvailableServices(String standort) throws Exception {
		List<String> all = new ArrayList<String>();
		String userCredentials = this.currentUser.getUsername() + ":" + this.currentUser.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);

		url = new URL(getBaseURI() + "/main/angebote/" + standort);
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("GET");
		con.setRequestProperty("Authorization", basicAuth);
		// con.setRequestProperty("Content-Type","application/json");

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String response;
		StringBuffer buffer = new StringBuffer();
		while ((response = in.readLine()) != null) {
			buffer.append(response);
		}
		in.close();
		Type collType = new TypeToken<List<String>>() {
		}.getType();
		System.out.println(buffer.toString());
		all = new Gson().fromJson(buffer.toString(), collType);

		return all;
	}

	public List<String> getAllStandorte() throws Exception {

		List<String> allStandorte = new ArrayList<String>();
		String userCredentials = this.currentUser.getUsername() + ":" + this.currentUser.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);

		url = new URL(getBaseURI() + "/main/standorte");
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("GET");
		con.setRequestProperty("Authorization", basicAuth);

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String response;
		StringBuffer buffer = new StringBuffer();
		while ((response = in.readLine()) != null) {
			buffer.append(response);
		}
		in.close();
		Type collType = new TypeToken<List<String>>() {
		}.getType();
		System.out.println(buffer.toString());
		allStandorte = new Gson().fromJson(buffer.toString(), collType);

		return allStandorte;
	}

	public String checkService(ReparaturAnfrage rA) throws Exception  {
		String userCredentials = this.currentUser.getUsername() + ":" + this.currentUser.getPassword();
		String basicAuth = "Basic " + new String(Base64.getEncoder().encode(userCredentials.getBytes()));
		System.out.println(basicAuth);
		String input = new Gson().toJson(rA);
		
		url = new URL(getBaseURI() + "/main/termin");
		con = (HttpsURLConnection) url.openConnection();

		con.setRequestMethod("POST");
		con.setRequestProperty("Authorization", basicAuth);
		con.setRequestProperty("Content-Type", "application/json");
		con.setDoOutput(true);
		OutputStreamWriter wr = new OutputStreamWriter(con.getOutputStream());
		wr.write(input);
		wr.close();
		System.out.println(input);
		System.out.println(con.getResponseCode());

		BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
		String response;
		StringBuffer buffer = new StringBuffer();
		while ((response = in.readLine()) != null) {
			buffer.append(response);
		}
		in.close();
		System.out.println(buffer.toString());
		String ret;
		if(buffer.toString().isEmpty()) {
			ret = null;
		} else {
			ret = buffer.toString();
		}
		
		return ret;
	}

//	public boolean insertTermin(ReparaturAnfrage rA) {
//		// Webservice Json schicken rA und dort eintragen
//		return true;
//	}

	private static String getBaseURI() {
		return "https://manuel57.ddns.net/AutohausWebservice/rest";

	}

}
