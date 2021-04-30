using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckUserNameScene : MonoBehaviour
{
    private SerializableObject so;
    public string MainMenuString;
    public string getNameString;

    void Start()
    {
        so = SaveManager.Load();

        if (so.userName == "" || so.userName == null)
        {
            SceneManager.LoadScene(getNameString);
        }
        else {
            SceneManager.LoadScene(MainMenuString);
        }
    }
}
