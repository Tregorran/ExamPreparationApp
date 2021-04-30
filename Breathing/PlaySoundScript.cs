using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundScript : MonoBehaviour
{
    public GameObject seaSoundObj;
    public GameObject natureSoundObj;

    public GameObject noneButton;
    public GameObject NatureButton;
    public GameObject SeaButton;

    void Start()
    {
        NoneSound();
    }

    public void NoneSound() { 
        seaSoundObj.SetActive(false);
        natureSoundObj.SetActive(false);
        noneButton.GetComponent<Image>().color = Color.gray;
        NatureButton.GetComponent<Image>().color = Color.white;
        SeaButton.GetComponent<Image>().color = Color.white;
    }

    public void playSea() {
        seaSoundObj.SetActive(true);
        natureSoundObj.SetActive(false);
        noneButton.GetComponent<Image>().color = Color.white;
        NatureButton.GetComponent<Image>().color = Color.white;
        SeaButton.GetComponent<Image>().color = Color.cyan;
    }

    public void playNature()
    {
        seaSoundObj.SetActive(false);
        natureSoundObj.SetActive(true);
        noneButton.GetComponent<Image>().color = Color.white;
        NatureButton.GetComponent<Image>().color = Color.green;
        SeaButton.GetComponent<Image>().color = Color.white;
    }
}
