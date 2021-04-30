using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C:\Program Files\Unity\Hub\Editor\2019.4.18f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\tools

//All the class information that is stored on the user's mobile phone
[System.Serializable]
public class SerializableObject
{
    
    public string userName;

    //flashcards, each group of flashcards
    public List<SerializableGroupClass> cardGroups = new List<SerializableGroupClass>();

    //practicetest, 
    public List<int> groupsForTest = new List<int>();
    public List<string> resultNotifGroups = new List<string>();

    //notifications, list of each notification
    public List<SerializableNotificationClass> notifications = new List<SerializableNotificationClass>();

    //for sorting the notification triggers
    public List<SerialisableTriggerClass> monday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> tuesday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> wednesday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> thursday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> friday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> saturday = new List<SerialisableTriggerClass>();
    public List<SerialisableTriggerClass> sunday = new List<SerialisableTriggerClass>();

    //Own activity
    public List<string> ownActivityContent = new List<string>();
    public List<string> dayActivityWritten = new List<string>();
}
