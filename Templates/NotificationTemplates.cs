using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotificationTemplates : MonoBehaviour
{
    public GameObject templatePanel;
    public string nextScene;
    public GameObject toggleUI;
    private SerializableObject so;
    public GameObject ListControl;

    public void InitialiseCheckTemplates() {
        PlayerPrefs.SetInt("CreateNotifTemps", 1);
        PlayerPrefs.SetInt("CreateCardTemps", 1);
    }

    //if the user choose not to see template panel
    public void checkToggle()
    {
        if (toggleUI.GetComponent<Toggle>().isOn == true)
        {
            PlayerPrefs.SetInt("CreateNotifTemps", 0);
        }
    }

    public void closePanel()
    {
        checkToggle();
        templatePanel.SetActive(false);
        SceneManager.LoadScene(nextScene);
    }

    //checks if not already create templates
    public void addButtonCheckNotifTemplates()
    {
        int createTemps = PlayerPrefs.GetInt("CreateNotifTemps");

        if (createTemps == 1)
        {
            templatePanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    //creates the template notifications
    public void createNotifTemps() {
        PlayerPrefs.SetInt("CreateNotifTemps", 0);
        templatePanel.SetActive(false);
        so = SaveManager.Load();

        //creates new ID for notifications
        int ID;
        if (so.notifications.Count == 0)
        {
            ID = 0;
        }
        else {
            ID = so.notifications[so.notifications.Count].notifID + 1;
        }
        createNotification("Create activities", "Create a list of activites that you need to accomplish today, " +
            "in the app.", "#01ADFF", "10", "30", ID);
        so.notifications.Add(newNotif);

        createNotification("Use flash cards", "Create a flash card, or revise them.", "#FFFFFF", "12", "00", ID + 1);
        so.notifications.Add(newNotif);

        createNotification("Practice test", "Use your flash you've created, and engage in one of the " +
            "practie tests in the app.", "#00F02D", "13", "00", ID + 2);
        so.notifications.Add(newNotif);

        createNotification("Breathing exercise", "Engage in the breathing exercise in the support.", "#00FFF9", 
        "15", "00", ID +3);
        so.notifications.Add(newNotif);

        SaveManager.Save(so);

        //update scrollable list to show new group of flashcards
        ListControl.GetComponent<NotificationListControl>().UpdateList();
    }

    SerializableNotificationClass newNotif;
    //creates each template notification
    private void createNotification(string title, string content, string colour, string hour, string minute, int ID) {
        newNotif = new SerializableNotificationClass();
        newNotif.notifPrompt = new List<SerialisableTriggerClass>();

        //assigns details to new notification
        newNotif.notifID = ID;
        newNotif.active = 0;
        newNotif.notifTitle = title;
        newNotif.notifText = content;
        newNotif.colour = colour;

        //creates each of the triggers and adds each to the notification
        newNotif.notifPrompt.Add(createTrigger("Monday", hour, minute,ID,1));
        newNotif.notifPrompt.Add(createTrigger("Tuesday", hour, minute,ID,2));
        newNotif.notifPrompt.Add(createTrigger("Wednesday", hour, minute,ID,3));
        newNotif.notifPrompt.Add(createTrigger("Thursday", hour, minute,ID,4));
        newNotif.notifPrompt.Add(createTrigger("Friday", hour, minute,ID,5));
        newNotif.notifPrompt.Add(createTrigger("Saturday", hour, minute,ID,6));
        newNotif.notifPrompt.Add(createTrigger("Sunday", hour, minute,ID,7));
    }

    //creates the triggers for the notification
    private SerialisableTriggerClass createTrigger(string day, string hour, string minute, int ID, int triggerID) {
        SerialisableTriggerClass newNotifTrigger = new SerialisableTriggerClass();
        newNotifTrigger.active = 0;
        newNotifTrigger.timeHour = hour;
        newNotifTrigger.timeMinute = minute;
        newNotifTrigger.day = day;
        newNotifTrigger.active = 0;
        newNotifTrigger.TriggerID = triggerID;
        newNotifTrigger.NotifID = ID;

        SortingTime(newNotifTrigger);
        return newNotifTrigger;
    }

    //sorts the triggers into the day of the week buckets
    public void SortingTime(SerialisableTriggerClass newSortEntry)
    {
        string day = newSortEntry.day;

        if (day == "Monday") {
            insertNewSort(ref so.monday, newSortEntry);
        } else if (day == "Tuesday") {
            insertNewSort(ref so.tuesday, newSortEntry);
        } else if (day == "Wednesday") {
            insertNewSort(ref so.wednesday, newSortEntry);
        } else if (day == "Thursday") {
            insertNewSort(ref so.thursday, newSortEntry);
        } else if (day == "Friday") {
            insertNewSort(ref so.friday, newSortEntry);
        } else if (day == "Saturday") {
            insertNewSort(ref so.saturday, newSortEntry);
        } else if (day == "Sunday") {
            insertNewSort(ref so.sunday, newSortEntry);
        }
    }

    //calculates where the trigger will be inserted
    private void insertNewSort(ref List<SerialisableTriggerClass> day, SerialisableTriggerClass newSortEntry)
    {
        // loop through each trigger in day bucket
        for (int k = 0; k < day.Count; k++)
        {
            string temp = day[k].timeHour + day[k].timeMinute;
            string temp2 = newSortEntry.timeHour + newSortEntry.timeMinute;

            //compare times
            if (int.Parse(temp) > int.Parse(temp2))
            {
                //insert the new trigger in correct place
                day.Insert(k, newSortEntry);
                return;
            }
        }
        day.Add(newSortEntry);
    }
}
