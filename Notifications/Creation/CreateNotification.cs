using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNotification : MonoBehaviour
{
    //decide if to edit notification or create
    public int edit = 0;

    //navigate back to list scene
    public string SceneToLoad;

    //inputfields
    public InputField notifTitleText;
    public InputField notifContentText;
    public GameObject colourObject;


    public GameObject triggerListObjects;//the scrollable list for the triggers
    private List<TriggerClassSort> triggerList;//the list of new triggers created

    private SerializableObject so;

    //the new notifications
    private SerializableNotificationClass newNotification;
    private SerialisableTriggerClass individualTrigger;

    //error messages
    public GameObject errorText1;
    public GameObject errorText2;
    public GameObject errorText3;

    private int errors;

    public void CreateNotificationFunc()
    {

        //retrieve input list of triggers
        triggerList = triggerListObjects.GetComponent<TriggerListControl>().AllClassTriggers;

        checkErrors();
        if (errors > 0) {
            return;
        }

        so = SaveManager.Load();
        //check if when button clicked will edit or create new notification
        if (edit == 1)
        {
            DeleteCurrentEdit();
        }

        newNotification = new SerializableNotificationClass();// create new notification object
        newNotification.notifPrompt = new List<SerialisableTriggerClass>();// Create new trigger list

        //creates ID of notification
        if (so.notifications.Count == 0)
        {
            newNotification.notifID = 0;
        }
        else {
            newNotification.notifID = so.notifications[so.notifications.Count - 1].notifID + 1;
        }

        //set content for notification
        newNotification.active = 0;
        newNotification.notifTitle = notifTitleText.text;
        newNotification.notifText = notifContentText.text;
        newNotification.colour = colourObject.GetComponent<SelectColour>().ColourSelectedHex;

        //loop through list of triggers adding to the new notification
        for (int i = 0; i < triggerList.Count; i++)
        {
            individualTrigger = new SerialisableTriggerClass();
            individualTrigger.timeHour = triggerList[i].hour;
            individualTrigger.timeMinute = triggerList[i].minute;
            individualTrigger.day = triggerList[i].day;
            individualTrigger.active = triggerList[i].active;

            //assigns new TriggerIDs
            int NewTriggerID;
            if (newNotification.notifPrompt.Count == 0)
            {
                NewTriggerID = 0;
            }
            else
            {
                NewTriggerID = newNotification.notifPrompt[newNotification.notifPrompt.Count - 1].TriggerID + 1;
            }

            individualTrigger.TriggerID = NewTriggerID;
            individualTrigger.NotifID = newNotification.notifID;
            newNotification.notifPrompt.Add(individualTrigger);

            //sorts the triggers
            SortingTime(individualTrigger);
        }

        so.notifications.Add(newNotification);
        SaveManager.Save(so);
        SceneManager.LoadScene(SceneToLoad);
    }

    //error checking
    private void checkErrors() {
        errors = 0;
        errorText1.SetActive(false);
        errorText2.SetActive(false);
        errorText3.SetActive(false);

        if (notifTitleText.text == "" || notifTitleText.text == null)
        {
            errorText1.SetActive(true);
            errors += 1;
        }

        if (notifContentText.text == "" || notifContentText.text == null)
        {
            errorText2.SetActive(true);
            errors += 1;
        }

        if (triggerList.Count < 1)
        {
            errorText3.SetActive(true);
            errors += 1;
        }
    }

    //removes the current notification for new edited notification to be added
    private void DeleteCurrentEdit()
    {
        //delete the notification in notifications for editing
        int curNotifNum = PlayerPrefs.GetInt("CurrentNotification");

        int numberToFind = so.notifications[curNotifNum].notifPrompt.Count;
        int IDFind = so.notifications[curNotifNum].notifID;
        so.notifications.RemoveAt(curNotifNum);

        so.monday = removeDays(so.monday, IDFind);
        so.tuesday = removeDays(so.tuesday, IDFind);
        so.wednesday = removeDays(so.wednesday, IDFind);
        so.thursday = removeDays(so.thursday, IDFind);
        so.friday = removeDays(so.friday, IDFind);
        so.saturday = removeDays(so.saturday, IDFind);
        so.sunday = removeDays(so.sunday, IDFind);
    }
    
    //removes the triggers for the sorted lists
    private List<SerialisableTriggerClass> removeDays(List<SerialisableTriggerClass> CurDays, int IDFind)
    {
        //loop backwards through list to avoid indexing errors when size of list changes.
        for (int i = CurDays.Count - 1; i > -1; i--)
        {
            if (CurDays[i].NotifID == IDFind) {
                Debug.Log("delete trigger: " + IDFind);
                CurDays.RemoveAt(i);
            }
        }
        return CurDays;
    }

    //sorts the triggers into each day
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
    private void insertNewSort(ref List<SerialisableTriggerClass> day, SerialisableTriggerClass newSortEntry) {
        //loop through each trigger in day bucket
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