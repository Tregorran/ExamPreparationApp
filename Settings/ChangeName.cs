using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public GameObject errorMessage;
    public GameObject inputfield;
    private SerializableObject so;
    public GameObject panel;

    public void setName() {
        so = SaveManager.Load();

        inputfield.GetComponent<InputField>().text = so.userName;
    }

    public void ChangeNameFunc() {

        if (inputfield.GetComponent<InputField>().text == "" || inputfield.GetComponent<InputField>().text == null)
        {
            errorMessage.SetActive(true);
        }
        else {
            errorMessage.SetActive(false);
            panel.SetActive(false);
            so.userName = inputfield.GetComponent<InputField>().text;
            SaveManager.Save(so);
        }
    }
}
