package com.htlvil.lam.kundenanwendung.controller;

import android.accounts.AuthenticatorException;
import android.os.AsyncTask;
import android.util.Base64;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.htlvil.lam.kundenanwendung.R;
import com.htlvil.lam.kundenanwendung.model.Angebot;
import com.htlvil.lam.kundenanwendung.model.ReparaurAnfrage;
import com.htlvil.lam.kundenanwendung.model.User;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.StringTokenizer;

public class WebserviceConnectionController {
    private static final String checkUserPostFix = "/user/check";
    public static final String checkTerminPostFix = "/main/termin";
    public static final String getStandorte = "/main/standorte";
    public static final String getAngebote = "/main/angebote";
    private User user = null;

    private static final String BASE_URL = "https://manuel57.ddns.net/AutohausWebservice/rest";
    private static WebserviceConnectionController instance = new WebserviceConnectionController();

    private WebserviceConnectionController() {

    }
    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public static WebserviceConnectionController getInstance() {
        return instance;
    }

    public ArrayList<String> getStandorte() {
        Gson gson = new Gson();

        String jsonBack = load(BASE_URL + getStandorte, "", ConnectionType.GET_STANDORTE);

        // MainActivity.showMessage(jsonBack);
        Log.d("-----", jsonBack);
        return gson.fromJson(jsonBack,new TypeToken<ArrayList<String>>(){}.getType());
    }




    public boolean isValidUser(User user) {
        Gson gson = new Gson();
        String jsonSend = gson.toJson(user);
        Log.d("-----", jsonSend);
        String jsonBack = load(BASE_URL + checkUserPostFix, jsonSend, ConnectionType.CHECK_USER);

        // MainActivity.showMessage(jsonBack);
        Log.d("-----", jsonBack);
        return gson.fromJson(jsonBack, boolean.class);
    }

    private InputStream OpenHttpConnection(String urlStr, String value, ConnectionType type) throws IOException, AuthenticatorException {
        InputStream ins = null;
        int response = -1;
        URL url = new URL(urlStr);
        URLConnection con = url.openConnection();

        if (!(con instanceof HttpURLConnection)) throw new IOException("Not an HTTP connection");
        try {
            HttpURLConnection http = (HttpURLConnection) con;
            //http.setAllowUserInteraction(false);
            // http.setInstanceFollowRedirects(true);

            Log.d("---------",(this.user==null)+"");

            if (this.user != null) {
                Log.d("---------",this.user.getUsername());
                String userCredentials = this.user.getUsername() + ":" + this.user.getPassword();
                String basicAuth = "Basic " + new String(Base64.encode(userCredentials.getBytes(), Base64.DEFAULT));
                Log.d("---------","Before setting auth");
                http.setRequestProperty("Authorization", basicAuth);
            }
            Log.d("---------","after setting auth");
            Log.d("---------",urlStr);
            Log.d("---------",type.toString());
            switch (type) {
                case CHECK_USER:
                    http.setRequestProperty("Content-Type", "application/json");
                    http.setRequestMethod("POST");
                    Log.d("********++++  ", value + " - " + urlStr);
                    OutputStreamWriter oos = new OutputStreamWriter(http.getOutputStream());
                    oos.write(value);
                    oos.close();
                    break;
                case CHECK_TERMIN:
                    http.setRequestProperty("Content-Type", "application/json");
                    http.setRequestMethod("POST");
                    Log.d("********++++  ", value + " - " + urlStr);
                    OutputStreamWriter oo = new OutputStreamWriter(http.getOutputStream());
                    oo.write(value);
                    oo.close();
                    break;
                case GET_STANDORTE:
                    http.setRequestMethod("GET");
                    http.connect();
                    break;
                case GET_ANGEBOT:
                    http.setRequestMethod("GET");
                    http.connect();
                    break;
                default:
                    break;

            }

            //  http.connect();
            Log.d("---------",urlStr);
            int statusCode = http.getResponseCode();
            if (statusCode != 200) {
                ins = http.getErrorStream();
            } else {

                // if (response == HttpURLConnection.HTTP_OK)
                ins = http.getInputStream();
            }
            //if (response == HttpURLConnection.HTTP_UNAUTHORIZED) throw new AuthenticatorException();
        } catch (Exception e) {
            Log.d("Networking", e.getMessage());
            Log.d("Networking", Arrays.toString(e.getStackTrace()));
            if (e instanceof AuthenticatorException)
                throw new AuthenticatorException("401 Not authorized");

            throw new IOException("Error connecting " + urlStr);
        }
        return ins;
    }


    private String load(String url, String value, ConnectionType type) {
        InputStream in = null;
        String response = "";
        try {
            in = OpenHttpConnection(url, value, type);
            Log.e("****************  ", (in == null) + "");
            if (in != null) {
                InputStreamReader isr = new InputStreamReader(in);
                int read;
                char[] buffer = new char[1024];
                while ((read = isr.read(buffer)) > 0)
                    response += String.copyValueOf(buffer, 0, read);
                in.close();
            }
        } catch (IOException e) {
            Log.d("NetworkingActivity", e.getLocalizedMessage());
            Log.d("NetworkingActivity", e.getMessage());
            Log.d("NetworkingActivity", Arrays.toString(e.getStackTrace()));
        } catch (AuthenticatorException e) {
            Log.e("---------", e.getMessage());
        }
        return response;
    }

    public ArrayList<Angebot> getAngebote(String standort) {
        Gson gson = new Gson();


        String jsonBack = load(BASE_URL + getAngebote + "/" + standort, "", ConnectionType.GET_ANGEBOT);

        // MainActivity.showMessage(jsonBack);
        Log.d("-----", jsonBack);
        ArrayList<String> a = gson.fromJson(jsonBack,new TypeToken<ArrayList<String>>(){}.getType());
        ArrayList<Angebot> an = new ArrayList<Angebot>();
        for (String s: a) {
            an.add(new Angebot(s,false));
        }
        return an;
    }

    public String checkTermin(ReparaurAnfrage a) {
        Gson gson = new Gson();
        String jsonSend = gson.toJson(a);
        Log.d("-----", jsonSend);
        String jsonBack = load(BASE_URL + checkTerminPostFix, jsonSend, ConnectionType.CHECK_TERMIN);

        // MainActivity.showMessage(jsonBack);
        Log.d("-----", jsonBack);
        return gson.fromJson(jsonBack, String.class);
    }
}
