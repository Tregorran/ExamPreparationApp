using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private List<GameObject> buttons;

    private SerializableObject so;

    public int dayTab = 0;

    void Start()
    {
        buttons = new List<GameObject>();//holds each of the buttons instantiated
        UpdateList();
    }

    //removes buttons from list to display new buttons in list
    public void RemoveButtonsFromList()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

    //Updates the list with the, display day selected
    public void UpdateList()
    {
        so = SaveManager.Load();

        RemoveButtonsFromList();
        if (dayTab == 0) {
            displayAllDays();
        }
        else if (dayTab == 1) {
            displayMonday();
        }
        else if (dayTab == 2) {
            displayTuesday();
        }
        else if (dayTab == 3) {
            displayWednesday();
        }
        else if (dayTab == 4) {
            displayThursday();
        }
        else if (dayTab == 5) {
            displayFriday();
        }
        else if (dayTab == 6) {
            displaySaturday();
        }
        else if (dayTab == 7)  {
            displaySunday();
        }
    }

    //displays all of the days
    public void displayAllDays() {
        displayMonday();
        displayTuesday();
        displayWednesday();
        displayThursday();
        displayFriday();
        displaySaturday();
        displaySunday();
        dayTab = 0;
    }

    public void displayMonday() {
        dayTab = 1;
        displayDay(so.monday);
    }

    public void displayTuesday() {
        dayTab = 2;
        displayDay(so.tuesday);
    }

    public void displayWednesday() {
        dayTab = 3;
        displayDay(so.wednesday);
    }

    public void displayThursday() {
        dayTab = 4;
        displayDay(so.thursday);
    }

    public void displayFriday() {
        dayTab = 5;
        displayDay(so.friday);
    }

    public void displaySaturday() {
        dayTab = 6;
        displayDay(so.saturday);
    }

    public void displaySunday() {
        dayTab = 7;
        displayDay(so.sunday);
    }

    //Displays the triggers for each of the notifications of that day
    private void displayDay(List<SerialisableTriggerClass> day)
    {
        if (!(day.Count == 0))
        {
            foreach (SerialisableTriggerClass CS in day)
            {
                createButtons(CS.NotifID, CS.TriggerID, CS.active, CS.timeHour, CS.timeMinute, CS.day);
            }
        }
    }

    //Instantiates the trigger buttons into the dynamic list
    public void createButtons(int notifID, int triggerID, int active, string hour, string minute, string day) {
        so = SaveManager.Load();

        //loop through each notification until find notifID
        for (int i = 0; i < so.notifications.Count; i++)
        {
            if (so.notifications[i].notifID == notifID)
            {
                //loop through each trigger until find triggerID
                for (int j = 0; j < so.notifications[i].notifPrompt.Count; j++) 
                {
                    if (so.notifications[i].notifPrompt[j].TriggerID == triggerID) 
                    {
                        //create model template button in list
                        active = so.notifications[i].notifPrompt[j].active;
                        
                        GameObject button = Instantiate(buttonTemplate) as GameObject;
                        button.SetActive(true);
                        button.GetComponent<NotificationListButton>().SetNotifNum(i, j, active, hour, minute, day);
                        button.transform.SetParent(buttonTemplate.transform.parent, false);
                        buttons.Add(button);
                    }
                }
            }
        }
    }
}