using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefsGetGameName : MonoBehaviour
{
    public Text GameTitleText;
    void Start()
    {
        string gamePlaying = PlayerPrefs.GetString("Game");
        if (!(gamePlaying == "" || gamePlaying == null))
        {
            GameTitleText.text = PlayerPrefs.GetString("Game");
        }
        else {
            Debug.Log("Error GameTitle");
        }
    }
}
