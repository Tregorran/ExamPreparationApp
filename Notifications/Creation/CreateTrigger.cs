using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTrigger : MonoBehaviour
{
    //dropdown values in panel
    public Dropdown hourTime;
    public Dropdown minuteTime;
    public Dropdown dayTime;

    public GameObject triggerList;

    public int CurListNum;

    //retrieves values from panel before sending these values to "TriggerListControl" to
    //instantiate new trigger button
    public void OnclickCreateTrigger() {
        //get values from panel
        string hourTimeText = hourTime.options[hourTime.value].text;
        string minuteTimeText = minuteTime.options[minuteTime.value].text;
        string dayTimeText = dayTime.options[dayTime.value].text;

        //create new trigger
        triggerList.GetComponent<TriggerListControl>().addTriggerTime(hourTimeText, minuteTimeText, dayTimeText, 0);
    }

    //Editing the trigger updates the trigger in the scrollable list in "TriggerListControl"
    public void OnClickEditTrigger() {
        //get info from dropdowns
        string newHour = hourTime.options[hourTime.value].text;
        string newMinute = minuteTime.options[minuteTime.value].text;
        string newDay = dayTime.options[dayTime.value].text;

        //edits the trigger
        triggerList.GetComponent<TriggerListControl>().AllClassTriggers[CurListNum].hour = newHour;
        triggerList.GetComponent<TriggerListControl>().AllClassTriggers[CurListNum].minute = newMinute;
        triggerList.GetComponent<TriggerListControl>().AllClassTriggers[CurListNum].day = newDay;

        triggerList.GetComponent<TriggerListControl>().DispTriggers();
    }
}
