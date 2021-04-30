using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNotification : MonoBehaviour
{
    private SerializableObject so;

    public void deleteNotification() {
        so = SaveManager.Load();
        removeNotification();
        SaveManager.Save(so);
    }

    private void removeNotification()
    {
        //delete the notification in notifications for editing
        int curNotifNum = PlayerPrefs.GetInt("CurNotifNum");
        int curTimeDateNum = PlayerPrefs.GetInt("CurTimeDateNum");

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

    private List<SerialisableTriggerClass> removeDays(List<SerialisableTriggerClass> CurDays, int IDFind)
    {
        for (int i = CurDays.Count - 1; i > -1; i--)
        {
            if (CurDays[i].NotifID == IDFind)
            {
                Debug.Log("delete trigger: " + IDFind);
                CurDays.RemoveAt(i);
            }
        }
        return CurDays;
    }
}
