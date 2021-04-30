using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPrefsEditNotification : MonoBehaviour
{
    private SerializableObject so;

    public InputField titleInputField;
    public InputField contentInputField;
    public GameObject triggerList;
    public GameObject colourButton;

    public int curNotifID;

    //retrieve and place details of notification in inputfields
    void Start()
    {
        so = SaveManager.Load();

        int curNotifIndex = PlayerPrefs.GetInt("CurrentNotification");

        //retrieve details of notification
        titleInputField.text = so.notifications[curNotifIndex].notifTitle;
        contentInputField.text = so.notifications[curNotifIndex].notifText;

        //loop through each trigger and display
        foreach(SerialisableTriggerClass STN in so.notifications[curNotifIndex].notifPrompt){
            triggerList.GetComponent<TriggerListControl>().addTriggerTime(STN.timeHour, STN.timeMinute, STN.day, STN.active);
        }

        colourButton.GetComponent<SelectColour>().setColour(so.notifications[curNotifIndex].colour);
    }
}
