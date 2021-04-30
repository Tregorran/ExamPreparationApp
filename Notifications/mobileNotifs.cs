using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class mobileNotifs : MonoBehaviour
{
    private SerializableObject so;

    public System.DateTime CurrentTimeDate;

    private List<AndroidNotification> androidNotifs;
    private List<int> notifIds;

    void Start()
    {
        so = SaveManager.Load();

        //Remove notifications that have already been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        androidNotifs = new List<AndroidNotification>();
        notifIds = new List<int>();

        CreateNotifChannel();
        SendNotifications();//calculates the notifications in "so"
    }

    //Create the Android Notification Channel to send messages through
    void CreateNotifChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    //recompile notifications if notifications get updated or 
    public void recompileNotifications() {
        AndroidNotificationCenter.CancelAllNotifications();
        SendNotifications();
    }

    //calculates the notifications times and schedules them
    void SendNotifications() {
        if (PlayerPrefs.HasKey("NotDisturb") == false)
        {
            PlayerPrefs.SetInt("NotDisturb", 0);
        }

        if (PlayerPrefs.GetInt("NotDisturb") == 1) {
            return;
        }


        androidNotifs.Clear();
        notifIds.Clear();

        //loop through every trigger and calculate fire times
        if (!(so.notifications.Count == 0))
        {
            //loop through each notification
            for (int i = 0; i < so.notifications.Count; i++)
            {
                if (so.notifications[i].active == 0)//check if notification is on
                {
                    //loop through each trigger in each notification
                    foreach (SerialisableTriggerClass eachTimeDate in so.notifications[i].notifPrompt)
                    {
                        if (eachTimeDate.active == 0)//check if trigger is on
                        {
                            //get current times
                            int curYear = System.DateTime.Now.Year;
                            int curMonth = System.DateTime.Now.Month;
                            int curDay = System.DateTime.Now.Day;
                            int curHour = System.DateTime.Now.Hour;
                            int curMinute = System.DateTime.Now.Minute;
                            Debug.Log(curYear + "/" + curMonth + "/" + curDay + ". " + curHour + ":" + curMinute);
                            DateTime dateValue = new DateTime(curYear, curMonth, curDay);
                            string curDayOfWeek = dateValue.DayOfWeek.ToString();

                            //retrieve trigger times
                            string nextDay = eachTimeDate.day;
                            string nextHour = eachTimeDate.timeHour;
                            string nextMinute = eachTimeDate.timeMinute;

                            //calculate fire time
                            int totalNumOfMin = DifferenceTime(curDayOfWeek, curHour, curMinute, nextDay,
                                int.Parse(nextHour), int.Parse(nextMinute));

                            //add a notification to be fired
                            AndroidNotification notification = new AndroidNotification
                            {
                                Title = so.userName + "! " + eachTimeDate.day + " " + nextHour + ":" + nextMinute + ": " 
                                + so.notifications[i].notifTitle,
                                Text = so.notifications[i].notifText,
                                FireTime = System.DateTime.Now.AddMinutes(totalNumOfMin),
                                RepeatInterval = new System.TimeSpan(0, (10080 - totalNumOfMin), 0),
                            };

                            var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");//schedules notification
                            androidNotifs.Add(notification);//for resending the same notifications
                            notifIds.Add(id);//for checking if notifications have been schedules multiple times
                        }
                    }
                }
            }
        }
        else {
            Debug.Log("No notifications to send");
        }

        //resends the notifications for no duplicates on multiple reloads of the script,
        //possibly due to navigating to scene multiple times
        if (!(notifIds.Count == 0))
        {
            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notifIds[0]) == NotificationStatus.Scheduled)
            {
                AndroidNotificationCenter.CancelAllNotifications();
                foreach(AndroidNotification resentNotif in androidNotifs)
                {
                    AndroidNotificationCenter.SendNotification(resentNotif, "channel_id");
                }
            }
        }
    }

    //calculates when the notifation should fire
    private int DifferenceTime(string currentDay, int curHour, int curMinute, string nextDay, int nextHour, int nextMinute) {
        int currentDayNum = calcDayNum(currentDay);
        int nextDayNum = calcDayNum(nextDay);
        int numOfDays = 0;
        int numOfHours = 0;
        int numOfMinutes = 0;
        numOfDays = nextDayNum - currentDayNum;

        if (currentDayNum > nextDayNum) {
            numOfDays += 7;
        }
        numOfHours = nextHour - curHour;
        if (curHour > nextHour) {
            numOfHours += 24;
            numOfDays -= 1;

            if (currentDayNum == nextDayNum) {
                numOfDays += 7;
            }
        }
        numOfMinutes = nextMinute - curMinute;
        if (curMinute > nextMinute) {
            numOfMinutes += 60;
            numOfHours -= 1;

            if (curHour == nextHour) {
                numOfHours += 24;
                if (currentDayNum == nextDayNum)
                {
                    numOfDays += 6;
                }
            }
        }
        return ((numOfDays * 24 * 60) + (numOfHours * 60) + numOfMinutes);//convert all to minutes
    }

    //return the string day
    private int calcDayNum(string day) {
        if (day == "Monday") {
            return 1;
        }
        else if (day == "Tuesday")
        {
            return 2;
        }
        else if (day == "Wednesday")
        {
            return 3;
        }
        else if (day == "Thursday")
        {
            return 4;
        }
        else if (day == "Friday")
        {
            return 5;
        }
        else if (day == "Saturday")
        {
            return 6;
        }
        else if (day == "Sunday")
        {
            return 7;
        }

        Debug.Log("day problem");
        return 0;
    }
}
