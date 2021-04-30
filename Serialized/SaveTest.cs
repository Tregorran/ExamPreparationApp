using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class SaveTest : MonoBehaviour
{
    public SerializableObject so;
    //public GameObject groupNameField;

    private void Start()
    {
        //Permission.RequestUserPermission(Permission.Microphone);
        //SaveManager.Save(so);
        //groupNameField.GetComponent<InputField>().text = "HEEEELLLLOOO";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SaveManager.Save(so);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            so = SaveManager.Load();
        }
    }

    /*public void saveName() {
        //so = SaveManager.Load();
        string here = groupNameField.GetComponent<InputField>().text;
        so.userNameTest = here;
        SaveManager.Save(so);
    }

    public void Loadthing() {
        Debug.Log("Load6");
        so = SaveManager.Load();
        string name = so.userNameTest;
        groupNameField.GetComponent<InputField>().text = name;
        Debug.Log("Load Done7");
    }*/

    public void ResetList() {
        so = SaveManager.Load();
        so.cardGroups.Clear();
        SaveManager.Save(so);
    }
}
