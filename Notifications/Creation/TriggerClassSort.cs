using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClassSort
{
    public string hour;
    public string minute;
    public string day;
    public int active;
    public int index;

    public bool compareTime(TriggerClassSort compareTrigger) {
        string curClassTime = hour + minute;
        string otherClassTime = compareTrigger.hour + compareTrigger.minute;

        if (int.Parse(curClassTime) > int.Parse(otherClassTime))
        {
            return true;
        }
        return false;
    }
}
