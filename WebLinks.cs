using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLinks : MonoBehaviour
{
    public string URL;

    public void LinkOpenURL()
    {
        Application.OpenURL(URL);
    }
}
