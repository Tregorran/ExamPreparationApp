using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDisturbScript : MonoBehaviour
{
    int notDisturb;

    public GameObject onButton;
    public GameObject offButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("NotDisturb") == false) {
            PlayerPrefs.SetInt("NotDisturb", 0);
        }
        notDisturb = PlayerPrefs.GetInt("NotDisturb");

        if (notDisturb == 0)
        {
            offButton.SetActive(true);
            onButton.SetActive(false);
        }
        else {
            onButton.SetActive(true);
            offButton.SetActive(false);
        }
    }

    public void switchDisturb() {
        if (notDisturb == 0)
        {
            notDisturb = 1;

            onButton.SetActive(true);
            offButton.SetActive(false);
        }
        else
        {
            notDisturb = 0;

            offButton.SetActive(true);
            onButton.SetActive(false);
        }

        PlayerPrefs.SetInt("NotDisturb", notDisturb);
    }
}
