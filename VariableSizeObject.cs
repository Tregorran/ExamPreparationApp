using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSizeObject : MonoBehaviour
{


    void Update()
    {
        transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    public void testDebug() {
        Debug.Log("CLICKED");
    }
}
