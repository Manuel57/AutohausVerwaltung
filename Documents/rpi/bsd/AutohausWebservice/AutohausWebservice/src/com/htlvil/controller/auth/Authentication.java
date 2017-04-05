package com.htlvil.controller.auth;

import java.io.IOException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Base64;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.StringTokenizer;

import com.htlvil.controller.database.Database;
import com.htlvil.model.CustomerUserData;

public class Authentication {

	private static HashMap<String, Date> authorized = new HashMap<String, Date>();

	public static String generateSha256(String pw) throws NoSuchAlgorithmException {
		MessageDigest md = MessageDigest.getInstance("SHA-256");
		md.update(pw.getBytes());

		byte byteData[] = md.digest();

		StringBuffer sb = new StringBuffer();
		for (int i = 0; i < byteData.length; i++) {
			sb.append(Integer.toString((byteData[i] & 0xff) + 0x100, 16).substring(1));
		}
		return sb.toString();
	}

	public static boolean isAuthorized(String username, String password) throws SQLException, NoSuchAlgorithmException {
		Database.getInstance().connect();
		ArrayList<CustomerUserData> users = Database.getInstance().getUsers(username, generateSha256(password));
		boolean isValid = false;
		if (users.size() > 0)
			isValid = true;
		return isValid;
	}

	public static boolean isAuthorized(String authCred) throws SQLException, NoSuchAlgorithmException {
		if (authCred == null || authCred.isEmpty() )
			return false;
		String auth = decodeAuth(authCred);
		if (auth.isEmpty())
			return false;

		final StringTokenizer tokenizer = new StringTokenizer(auth, ":");
		final String username = tokenizer.nextToken();
		final String password = tokenizer.nextToken();
		return isAuthorized(username, password);

	}

	private static String decodeAuth(String authCredentials) {
		if (null == authCredentials)
			return "";
		final String encodedUserPassword = authCredentials.replaceFirst("Basic" + " ", "");
		String usernameAndPassword = null;
		try {
			byte[] decodedBytes = Base64.getDecoder().decode(encodedUserPassword);
			
			
			usernameAndPassword = new String(decodedBytes, "UTF-8");
		} catch (IOException e) {
			e.printStackTrace();
		}

		return usernameAndPassword;
	}
}