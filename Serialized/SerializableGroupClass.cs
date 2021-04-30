using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableGroupClass
{
    public string GroupName;

    //list of cards in group
    public List<SerialisableCardClass> cards = new List<SerialisableCardClass>();
    public int lastTestScore = 0;
    public string dateCreated;
}
