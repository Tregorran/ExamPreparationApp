using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerListButton : MonoBehaviour
{
    //display objects
    public Text hourText;
    public Text minuteText;
    public Text dateText;
    public int currentActive;

    public GameObject OnButton;
    public GameObject OffButton;

    public GameObject listGameObject;

    //edit trigger panel
    public GameObject panel;
    public Dropdown hourTimeDrop;
    public Dropdown minuteTimeDrop;
    public Dropdown dayTimeDrop;

    public GameObject editTriggerButton;

    public TriggerClassSort triggerObject;

    //retrieves trigger from TriggerListControl for script to display
    public void SetTriggerClass(TriggerClassSort newTriggerObject) {
        triggerObject = newTriggerObject;
        SetHour(newTriggerObject.hour);
        SetMinute(newTriggerObject.minute);
        SetDate(newTriggerObject.day);
        SetActive(newTriggerObject.active);
    }

    public void SetHour(string textString)
    {
        hourText.text = textString;
    }

    public void SetMinute(string textString)
    {
        minuteText.text = textString;
    }

    public void SetDate(string textString)
    {
        dateText.text = textString;
    }

    public void SetActive(int active) {
        currentActive = active;
        if (active == 0)
        {
            OnButton.SetActive(true);
            OffButton.SetActive(false);
        }
        else
        {
            OnButton.SetActive(false);
            OffButton.SetActive(true);
        }
    }

    //toggle for changing the active of the trigger
    public void changeActive() {
        if (OnButton.activeSelf == true)
        {
            currentActive = 1;
            SetActive(currentActive);

            listGameObject.GetComponent<TriggerListControl>().setActiveButton(triggerObject.index);
        }
        else {
            currentActive = 0;
            SetActive(currentActive);

            listGameObject.GetComponent<TriggerListControl>().setActiveButton(triggerObject.index);
        }
    }

    //delete button is pressed, deletes the trigger
    public void DeleteNotificationTrigger() {
        listGameObject.GetComponent<TriggerListControl>().DeleteTrigger(triggerObject);
    }

    //opens the edit trigger panel to change the triggers script holds
    public void openEditTriggerPanel()
    {
        panel.SetActive(true);

        string hourString = triggerObject.hour;
        string minuteString = triggerObject.minute;
        string dayString = triggerObject.day;

        //assigns the hour in the hour dropdown in the edit panel
        for (int i = 0; i < hourTimeDrop.options.Count; i++)
        {
            if (hourTimeDrop.options[i].text == hourString)
            {
                hourTimeDrop.value = i;
            }
        }

        //assigns the minute in the minute dropdown in the edit panel
        for (int i = 0; i < minuteTimeDrop.options.Count; i++)
        {
            if (minuteTimeDrop.options[i].text == minuteString)
            {
                minuteTimeDrop.value = i;
            }
        }

        // assigns the day in the day dropdown in the edit panel
        for (int i = 0; i < dayTimeDrop.options.Count; i++)
        {
            if (dayTimeDrop.options[i].text == dayString)
            {
                dayTimeDrop.value = i;
            }
        }
        editTriggerButton.GetComponent<CreateTrigger>().CurListNum = triggerObject.index;
    }
}
