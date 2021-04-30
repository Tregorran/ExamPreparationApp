using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListControl : MonoBehaviour
{
    public GameObject buttonTemplate;

    public List<GameObject> AllTimeDateButtons;//the instantiated buttons
    public List<TriggerClassSort> AllClassTriggers;//unsorted triggers stored

    private int daySortDisplay = 0;

    void Start()
    {
        AllTimeDateButtons = new List<GameObject>();
        AllClassTriggers = new List<TriggerClassSort>();
    }

    //changes the triggers active when switch button pressed
    public void setActiveButton(int index) {

        if (AllClassTriggers[index].active == 0)
        {
            AllClassTriggers[index].active = 1;
        }
        else {
            AllClassTriggers[index].active = 0;
        }
    }

    //removes a trigger from the list displayed - deleting a trigger button
    public void DeleteTrigger(TriggerClassSort triggerToDelete)
    {
        AllClassTriggers.RemoveAt(triggerToDelete.index);

        //all following index, reduce their index by one and disp
        for (int i = triggerToDelete.index; i < AllClassTriggers.Count; i++)
        {
            AllClassTriggers[i].index -= 1;
        }

        DispTriggers();
    }

    //this function is called for each trigger from the "GetPrefsEditNotification" - if edit
    //creates a trigger to place in list
    public void addTriggerTime(string hourString, string minuteString, string dateString, int active)
    {
        TriggerClassSort newTrigger = new TriggerClassSort();
        newTrigger.hour = hourString;
        newTrigger.minute = minuteString;
        newTrigger.day = dateString;
        newTrigger.active = active;
        newTrigger.index = AllClassTriggers.Count;

        AllClassTriggers.Add(newTrigger);

        DispTriggers();
    }

    public void RemoveButtonsFromList()
    {
        if (AllTimeDateButtons.Count > 0)
        {
            foreach (GameObject button in AllTimeDateButtons)
            {
                Destroy(button.gameObject);
            }
            AllTimeDateButtons.Clear();
        }
    }

    public void DispTriggers() {
        RemoveButtonsFromList();

        if (daySortDisplay == 0) {
            displayTriggersAll();
        }
        else if (daySortDisplay == 1)
        {
            displayTriggersMonday();
        }
        else if (daySortDisplay == 2)
        {
            displayTriggersTuesday();
        }
        else if (daySortDisplay == 3)
        {
            displayTriggersWednesday();
        }
        else if (daySortDisplay == 4)
        {
            displayTriggersThursday();
        }
        else if (daySortDisplay == 5)
        {
            displayTriggersFriday();
        }
        else if (daySortDisplay == 6)
        {
            displayTriggersSaturday();
        }
        else if (daySortDisplay == 7)
        {
            displayTriggersSunday();
        }
    }

    //controls which day user wants to display triggers
    public void displayTriggersAll()
    {
        displayTriggersMonday();
        displayTriggersTuesday();
        displayTriggersWednesday();
        displayTriggersThursday();
        displayTriggersFriday();
        displayTriggersSaturday();
        displayTriggersSunday();
        daySortDisplay = 0;
    }

    public void displayTriggersMonday() {
        daySortDisplay = 1;
        string DisplayDay = "Monday";
        displayFunc(DisplayDay);
    }
    
    public void displayTriggersTuesday()
    {
        daySortDisplay = 2;
        string DisplayDay = "Tuesday";
        displayFunc(DisplayDay);
    }

    public void displayTriggersWednesday()
    {
        daySortDisplay = 3;
        string DisplayDay = "Wednesday";
        displayFunc(DisplayDay);
    }

    public void displayTriggersThursday()
    {
        daySortDisplay = 4;
        string DisplayDay = "Thursday";
        displayFunc(DisplayDay);
    }

    public void displayTriggersFriday()
    {
        daySortDisplay = 5;
        string DisplayDay = "Friday";
        displayFunc(DisplayDay);
    }

    public void displayTriggersSaturday()
    {
        daySortDisplay = 6;
        string DisplayDay = "Saturday";
        displayFunc(DisplayDay);
    }

    public void displayTriggersSunday()
    {
        daySortDisplay = 7;
        string DisplayDay = "Sunday";
        displayFunc(DisplayDay);
    }

    private List<TriggerClassSort> tempSortedList = new List<TriggerClassSort>();

    //sorts the triggers in a list then instantiates the triggers to be displayed.
    private void displayFunc(string day)
    {
        tempSortedList.Clear();
        //sorts the triggers from AllClassTrigers into tempSortedList
        foreach (TriggerClassSort Trigger in AllClassTriggers)
        {
            sortTriggers(Trigger, day);
        }

        //with sorted list of triggers create the triggers into display list
        foreach (TriggerClassSort Trigger in tempSortedList)
        {
            createTriggerButton(Trigger);
        }
    }

    //sorts all triggers by inserting the trigger into the correct place
    private void sortTriggers(TriggerClassSort insertItem, string day) {
        if (insertItem.day == day)
        {
            for (int k = 0; k < tempSortedList.Count; k++)
            {
                if (tempSortedList[k].compareTime(insertItem))
                {
                    tempSortedList.Insert(k, insertItem);
                    return;
                }
            }
            tempSortedList.Add(insertItem);
        }
    }

    //instantiates each trigger in order into the scrollable list
    private void createTriggerButton(TriggerClassSort trigger)
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<TriggerListButton>().SetTriggerClass(trigger);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
        AllTimeDateButtons.Add(button);
    }
}
