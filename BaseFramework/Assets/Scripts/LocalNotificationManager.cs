using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif
public class LocalNotificationManager : MonoBehaviour
{

#if UNITY_ANDROID
    private AndroidNotificationChannel androidNotificationChannel;
    private const string channelId = "0";

	private void SendNotification(string textToShow)
    {
        androidNotificationChannel = new AndroidNotificationChannel();
        androidNotificationChannel.Id = channelId;
        androidNotificationChannel.Name = "Default Channel";
        androidNotificationChannel.Importance = Importance.High;
        androidNotificationChannel.Description = "Generic notifications";
        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);


	    AndroidNotification notification = new AndroidNotification();
	    notification.Text = textToShow;
	    notification.Title = "Loop Land";
	    //notification.FireTime = System.DateTime.Now.AddMinutes(2);
	    notification.FireTime = System.DateTime.Now.AddHours(18);

        int id = AndroidNotificationCenter.SendNotification(notification, channelId);
    }
 
#endif

	private void Start()
	{
#if UNITY_ANDROID
		AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS
		iOSNotificationCenter.RemoveAllScheduledNotifications();
#endif

		SendNotification(GetNotificationText());

	}

	/*private void OnApplicationQuit()
	{
		SendNotification(GetNotificationText());
	}*/

    public string GetNotificationText()
	{


        //ToDo Add the text the you want to show

		string res = "JAN-KEN-PAW! Collect more Chickens now and win the race!";
		return res;
	}

    
#if UNITY_IOS
	public void SendNotification(string textToShow)
	{
		var date = System.DateTime.Now.AddSeconds(8);
		var text = textToShow;

		var timeTrigger = new iOSNotificationCalendarTrigger
		{
			Year = date.Year,
			Month = date.Month,
			Day = date.Day,
			Hour = date.Hour,
			Minute = date.Minute,
			Second = date.Second
		};

		var notification = new iOSNotification
		{
			Body = text,
			ForegroundPresentationOption = PresentationOption.Alert,
			Trigger = timeTrigger
		};

		iOSNotificationCenter.ScheduleNotification(notification);

	}
#endif
}