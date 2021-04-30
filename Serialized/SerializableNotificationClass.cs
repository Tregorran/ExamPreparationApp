using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableNotificationClass
{
    public int notifID;

    public int active;
    public string notifTitle;
    public string notifText;
    public string colour;

    //each trigger for the notification
    public List<SerialisableTriggerClass> notifPrompt;
}
