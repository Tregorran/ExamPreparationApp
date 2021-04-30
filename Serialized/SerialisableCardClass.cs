using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerialisableCardClass
{
    //first side of card information
    public string FirstContent;
    public string FirstImagePath;

    //second side card information
    public string SecondContent;
    public string SecondImagePath;

    //if card was correct or not in test
    public string cardCorrect;

    public string dateCreated;
}
