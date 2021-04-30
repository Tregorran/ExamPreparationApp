using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSaveName : MonoBehaviour
{
    public SerializableObject so;
    public GameObject userNameField;
    public GameObject ErrorText;

    void Start()
    {
        so = SaveManager.Load();
        if (!(so.userName == "" || so.userName == null)) {
            nextScene();
        }
    }

    public void saveName()
    {
        SerializableObject so = new SerializableObject();
        Debug.Log("Name straight: " + userNameField.GetComponent<InputField>().text);
        string name = userNameField.GetComponent<InputField>().text;
        Debug.Log("Name: " + name);
        if (!(name == "" || name == null))
        {
            so.userName = name;
            Debug.Log("Save start");
            SaveManager.Save(so);
            Debug.Log("Save successful");
            nextScene();
        }
        else {
            ErrorText.SetActive(true);
        }
            
    }

    private void nextScene() {
        SceneManager.LoadScene("MainMenu");
    }
}
