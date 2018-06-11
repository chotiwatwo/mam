package com.jubtum.tumpush;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;

public class MyFirebaseMessagingService extends FirebaseMessagingService {
    private static  final String TAG = "MyFirebaseMessagingServ";

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {

        String notificationBody = "";
        String notificationTitle = "";
        String notificationData = "";

        try
        {
            notificationData = remoteMessage.getData().toString();
            notificationTitle = remoteMessage.getNotification().getTitle();
            notificationBody = remoteMessage.getNotification().getBody();
        } catch (NullPointerException e )
        {
            Log.e(TAG, "onMessageReceived: NullPointerException: " + e.getMessage());
        }


        Log.e(TAG, "onMessageReceived: data: " + notificationData);
        Log.e(TAG, "onMessageReceived: notificationTitle: " + notificationTitle);
        Log.e(TAG, "onMessageReceived: notificationBody: " + notificationBody);

        /*Intent intent = new Intent(this, MainActivity.class);

        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        PendingIntent pendingIntent = PendingIntent
                .getActivity(this, 0, intent, PendingIntent.FLAG_ONE_SHOT);

        NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this);

        notificationBuilder.setContentTitle(notificationTitle);
        notificationBuilder.setContentText(notificationBody);
        notificationBuilder.setSmallIcon(R.mipmap.ic_launcher);
        //notificationBuilder.setAutoCancel(true);
        notificationBuilder.setContentIntent(pendingIntent);

        NotificationManager notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);
        notificationManager.notify(0, notificationBuilder.build());*/
    }

    @Override
    public void onDeletedMessages() {
        super.onDeletedMessages();
    }
}
