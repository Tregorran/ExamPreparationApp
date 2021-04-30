using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underline : MonoBehaviour
{
    public GameObject allUnderline;
    public GameObject monUnderline;
    public GameObject tueUnderline;
    public GameObject wedUnderline;
    public GameObject thuUnderline;
    public GameObject friUnderline;
    public GameObject satUnderline;
    public GameObject sunUnderline;

    /*void Start()
    {
        int dayTab = PlayerPrefs.GetInt("DayTab");
        if (dayTab == 0) {
            underlineAll();
        } else if (dayTab == 1) {
            underlineMon();
        } else if (dayTab == 2) {
            underlineTue();
        } else if (dayTab == 3) {
            underlineWed();
        } else if (dayTab == 4) {
            underlineThu();
        } else if (dayTab == 5) {
            underlineFri();
        } else if (dayTab == 6) {
            underlineSat();
        } else if (dayTab == 7) {
            underlineSun();
        }
    }*/

    public void underlineAll() {
        removeOtherLines();
        allUnderline.SetActive(true);
    }

    public void underlineMon()
    {
        removeOtherLines();
        monUnderline.SetActive(true);
    }

    public void underlineTue()
    {
        removeOtherLines();
        tueUnderline.SetActive(true);
    }

    public void underlineWed()
    {
        removeOtherLines();
        wedUnderline.SetActive(true);
    }

    public void underlineThu()
    {
        removeOtherLines();
        thuUnderline.SetActive(true);
    }

    public void underlineFri()
    {
        removeOtherLines();
        friUnderline.SetActive(true);
    }

    public void underlineSat()
    {
        removeOtherLines();
        satUnderline.SetActive(true);
    }

    public void underlineSun()
    {
        removeOtherLines();
        sunUnderline.SetActive(true);
    }

    public void removeOtherLines() {
        allUnderline.SetActive(false);
        monUnderline.SetActive(false);
        tueUnderline.SetActive(false);
        wedUnderline.SetActive(false);
        thuUnderline.SetActive(false);
        friUnderline.SetActive(false);
        satUnderline.SetActive(false);
        sunUnderline.SetActive(false);
    }
}
