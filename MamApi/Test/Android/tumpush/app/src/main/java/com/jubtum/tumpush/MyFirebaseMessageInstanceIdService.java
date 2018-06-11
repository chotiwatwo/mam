package com.jubtum.tumpush;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.FirebaseInstanceIdService;

import android.content.Context;
import android.content.SharedPreferences;
import android.nfc.Tag;
import android.util.Log;

public class MyFirebaseMessageInstanceIdService extends FirebaseInstanceIdService {
    private static  final String TAG = "MyFirebaseInstanceIdServ";

    @Override
    public void onTokenRefresh() {
        String recent_token = FirebaseInstanceId.getInstance().getToken();

        SharedPreferences sharedPreferences = getApplicationContext()
                .getSharedPreferences(getString(R.string.FCM_PREF), Context.MODE_PRIVATE);

        android.content.SharedPreferences.Editor editor = sharedPreferences.edit();

        editor.putString(getString(R.string.FCM_TOKEN), recent_token);

        editor.commit();



    }
}

