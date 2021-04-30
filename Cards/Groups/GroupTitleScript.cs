using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupTitleScript : MonoBehaviour
{
    private SerializableObject so;
    public GameObject groupTitle;
    public int choice = 0;

    // Start is called before the first frame update

    public void showGroupNameInfo() {
        if (choice == 0)
        {
            so = SaveManager.Load();
            string currentGroup = so.cardGroups[PlayerPrefs.GetInt("CurrentGroupIndex")].GroupName;
            groupTitle.GetComponent<Text>().text = currentGroup;
        }
        else if (choice == 1)
        {
            so = SaveManager.Load();
            string currentGroup = so.cardGroups[PlayerPrefs.GetInt("CurrentGroupIndex")].GroupName;
            Debug.Log(currentGroup);
            groupTitle.GetComponent<InputField>().text = currentGroup;
        }
    }
    void Start()
    {
        showGroupNameInfo();
    }
}
