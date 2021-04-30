using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsScript : MonoBehaviour
{
    public string userName;
    public string backgroundColour;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("score", 5);
        // PlayerPrefs.SetFloat("volume", 0.6f);

        backgroundColour = PlayerPrefs.GetString("BackgroundColour");
        userName = PlayerPrefs.GetString("UserName");

        if (backgroundColour == "") {
            PlayerPrefs.SetString("BackgroundColour", "blue");
        }

        if (userName == "")
        {
            PlayerPrefs.SetString("BackgroundColour", "blue");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
