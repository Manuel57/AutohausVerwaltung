package com.htlvil.lam.kundenanwendung;

import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.support.v4.app.NotificationCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Display;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CalendarView;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.appindexing.Thing;
import com.google.android.gms.common.api.GoogleApiClient;
import com.htlvil.lam.kundenanwendung.controller.WebserviceConnectionController;
import com.htlvil.lam.kundenanwendung.model.Datum;
import com.htlvil.lam.kundenanwendung.model.ReparaurAnfrage;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class ActivityChooseTermin extends AppCompatActivity {

    private static ReparaurAnfrage anfrage = null;
    private ArrayList<Datum> dates;
    public static Context context;
    private boolean first = false;
    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    private GoogleApiClient client;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_choose_termin);
        context = this;
        anfrage = ActivityStandorte.getAnfrage();
        dates = new ArrayList<Datum>();
        first = false;
        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client = new GoogleApiClient.Builder(this).addApi(AppIndex.API).build();
    }


    public void btnClickSecond(View v) {
        DatePicker dp = (DatePicker) findViewById(R.id.dpFrom);
        dates.add(new Datum(dp.getYear() + "", dp.getMonth() + 1 + "", dp.getDayOfMonth() + ""));
        if (!first) {
            first = true;
            ((Button) findViewById(R.id.btnSecondDate)).setText("Check Termin");
        } else {
            Log.d("...............++", anfrage.getStandort());
            anfrage.setDatesToCheck(dates);
            anfrage.setCustomMessage(((EditText) findViewById(R.id.txtMessage)).getText().toString());
            Log.d("ActBtnLast --", "alles okay");
            new UserTask(anfrage).execute((Void) null);
            try {
                //We need to get the instance of the LayoutInflater, use the context of this activity
                //  LayoutInflater inflater = (LayoutInflater) ProfileView.this
                //          .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                //Inflate the view from a predefined XML layout
                View layout = getLayoutInflater().inflate(R.layout.popup,
                        (ViewGroup) findViewById(R.id.popup_element));
                // create a 300px width and 470px height PopupWindow
                PopupWindow pw = new PopupWindow(layout, 900, 400, true);
                // display the popup in the center
                pw.showAtLocation(findViewById(R.id.btnSecondDate), Gravity.CENTER, 0, 0);

            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    public Action getIndexApiAction() {
        Thing object = new Thing.Builder()
                .setName("ActivityChooseTermin Page") // TODO: Define a title for the content shown.
                // TODO: Make sure this auto-generated URL is correct.
                .setUrl(Uri.parse("http://[ENTER-YOUR-URL-HERE]"))
                .build();
        return new Action.Builder(Action.TYPE_VIEW)
                .setObject(object)
                .setActionStatus(Action.STATUS_TYPE_COMPLETED)
                .build();
    }

    @Override
    public void onStart() {
        super.onStart();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client.connect();
        AppIndex.AppIndexApi.start(client, getIndexApiAction());
    }

    @Override
    public void onStop() {
        super.onStop();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        AppIndex.AppIndexApi.end(client, getIndexApiAction());
        client.disconnect();
    }

    public class UserTask extends AsyncTask<Void, Void, Boolean> {
        ReparaurAnfrage a;
        String termin;

        UserTask(ReparaurAnfrage an) {
            a = an;
        }

        @Override
        protected Boolean doInBackground(Void... params) {
            // TODO: attempt authentication against a network service.

            try {
                // Simulate network access.
                termin = WebserviceConnectionController.getInstance().checkTermin(a);
                Thread.sleep(2000);
            } catch (InterruptedException e) {
                return false;
            }

            return true;


        }

        @Override
        protected void onPostExecute(final Boolean success) {
            if (termin != null) {
                NotificationCompat.Builder mBuilder =
                        new NotificationCompat.Builder(getApplicationContext())
                                .setSmallIcon(R.mipmap.ic_launcher)
                                .setContentTitle("Termin bestätigt")
                                .setContentText("Sie haben am " + termin + " einen Termin bei uns");
                ((Button) findViewById(R.id.btnSecondDate)).setText("Check Termin");





                NotificationManager mNotifyMgr =
                        (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
                mNotifyMgr.notify(001, mBuilder.build());
            }
        }

        @Override
        protected void onCancelled() {

        }
    }
}
