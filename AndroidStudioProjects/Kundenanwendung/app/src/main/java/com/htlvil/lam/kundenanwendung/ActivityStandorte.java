package com.htlvil.lam.kundenanwendung;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.htlvil.lam.kundenanwendung.controller.WebserviceConnectionController;
import com.htlvil.lam.kundenanwendung.model.Angebot;
import com.htlvil.lam.kundenanwendung.model.ReparaurAnfrage;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;

public class ActivityStandorte extends AppCompatActivity {

    public static Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_standorte);
        context = this;
        //
        String stand = getIntent().getStringExtra("standort");
        new UserTask(stand).execute((Void) null);
    }

    public class UserTask extends AsyncTask<Void, Void, Boolean> {
        ArrayList<Angebot> angebote;
        String standort;


        UserTask(String s) {
            standort = s;
        }

        @Override
        protected Boolean doInBackground(Void... params) {
            // TODO: attempt authentication against a network service.

            try {
                // Simulate network access.
                angebote = WebserviceConnectionController.getInstance().getAngebote(standort);
                Thread.sleep(2000);
            } catch (InterruptedException e) {
                return false;
            }

            return true;


        }

        @Override
        protected void onPostExecute(final Boolean success) {
            if (angebote != null) {
                ((TextView) findViewById(R.id.tvStandort)).setText(standort);
                ListView lv = (ListView) findViewById(R.id.lvAngebote);
                lv.setAdapter(new MyCustomAdapter(context
                        , R.layout.angebot_info, angebote));

                lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        Angebot a = (Angebot) parent.getItemAtPosition(position);
                        // Toast.makeText(getApplicationContext(),a.getName() + " " + a.isSelected(), Toast.LENGTH_LONG);
                    }
                });
            }
        }

        @Override
        protected void onCancelled() {

        }
    }

    private static ReparaurAnfrage anfrage;

    public static ReparaurAnfrage getAnfrage() { return anfrage;}

    public void chooseTermin(View view) {
        ListView lv = (ListView) findViewById(R.id.lvAngebote);
        MyCustomAdapter a = (MyCustomAdapter) lv.getAdapter();
        ArrayList<String> leistungen = new ArrayList<String>();
        for (int i = 0; i < a.getCount(); i++) {
            Angebot ab = a.getItem(i);
            if (ab.isSelected())
               leistungen.add(ab.getName());
        }
        anfrage = new ReparaurAnfrage(WebserviceConnectionController.getInstance().getUser(), "");
        anfrage.setLeistungen(leistungen);
        anfrage.setStandort(((TextView) findViewById(R.id.tvStandort)).getText().toString());
        Intent intent = new Intent(this, ActivityChooseTermin.class);

        startActivity(intent);

    }

    private class MyCustomAdapter extends ArrayAdapter<Angebot> {

        private ArrayList<Angebot> countryList;

        public MyCustomAdapter(Context context, int textViewResourceId,
                               ArrayList<Angebot> countryList) {
            super(context, textViewResourceId, countryList);
            this.countryList = new ArrayList<Angebot>();
            this.countryList.addAll(countryList);
        }

        private class ViewHolder {
            TextView code;
            CheckBox name;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {

            ViewHolder holder = null;
            Log.v("ConvertView", String.valueOf(position));

            if (convertView == null) {
                LayoutInflater vi = (LayoutInflater) getSystemService(
                        Context.LAYOUT_INFLATER_SERVICE);
                convertView = vi.inflate(R.layout.angebot_info, null);

                holder = new ViewHolder();
                holder.code = (TextView) convertView.findViewById(R.id.code);
                holder.name = (CheckBox) convertView.findViewById(R.id.checkBox1);
                convertView.setTag(holder);

                holder.name.setOnClickListener(new View.OnClickListener() {
                    public void onClick(View v) {
                        CheckBox cb = (CheckBox) v;
                        Angebot country = (Angebot) cb.getTag();
                        Toast.makeText(getApplicationContext(),
                                "Clicked on Checkbox: " + cb.getText() +
                                        " is " + cb.isChecked(),
                                Toast.LENGTH_LONG).show();
                        country.setSelected(cb.isChecked());
                    }
                });
            } else {
                holder = (ViewHolder) convertView.getTag();
            }

            Angebot country = countryList.get(position);
            holder.code.setText(" (" + country.getName() + ")");
            holder.name.setText(country.getName());
            holder.name.setChecked(country.isSelected());
            holder.name.setTag(country);

            return convertView;

        }

    }
}
