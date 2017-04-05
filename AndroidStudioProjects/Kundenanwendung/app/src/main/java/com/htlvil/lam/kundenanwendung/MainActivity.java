package com.htlvil.lam.kundenanwendung;

import android.app.TabActivity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TabHost;
import android.widget.Toast;

import com.htlvil.lam.kundenanwendung.controller.WebserviceConnectionController;
import com.htlvil.lam.kundenanwendung.model.User;

import java.util.ArrayList;
import java.util.Arrays;

public class MainActivity extends AppCompatActivity {

    private static Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this;
        setContentView(R.layout.main);
        // setSupportActionBar(toolbar);

        // Intent intent = new Intent(this,LoginActivity.class);
        //  //startActivity(intent);

        //WebserviceConnectionController.getInstance().setUser(new User("asdf12345", "ThomasHuber201669"));

        new UserTask().execute((Void)null);



    }

    public static void showMessage(String message) {
        Toast.makeText(context, message, Toast.LENGTH_LONG).show();
    }

    public class UserTask extends AsyncTask<Void, Void, Boolean> {
    ArrayList<String> s;
        @Override
        protected Boolean doInBackground(Void... params) {
            // TODO: attempt authentication against a network service.

            try {
                // Simulate network access.
                s = WebserviceConnectionController.getInstance().getStandorte();
                Thread.sleep(2000);
            } catch (InterruptedException e) {
                return false;
            }

            return true;


        }

        @Override
        protected void onPostExecute(final Boolean success) {
            if(s!=null) {
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(context,
                        android.R.layout.simple_list_item_1, s);
                ListView lv = (ListView) findViewById(R.id.lvStandorte);
                lv.setAdapter(adapter);
                lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        Intent i = new Intent(context, ActivityStandorte.class);
                        i.putExtra("standort", parent.getItemAtPosition(position).toString());
                        startActivity(i);
                    }
                });
            }
        }

        @Override
        protected void onCancelled() {

        }
    }
}
