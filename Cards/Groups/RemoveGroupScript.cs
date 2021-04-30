using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RemoveGroupScript : MonoBehaviour
{
    public GameObject editGroupNamePanel;
    public GameObject curEditPanel;
    public GameObject ListControl;
    private SerializableObject so;

    public GameObject EditButton;
    public GameObject groupNotifButtons;

    public void RemoveGroupFunc() {
        int groupIndex = saveGroupIndex();
        so = SaveManager.Load();
        so.cardGroups.RemoveAt(groupIndex);
        SaveManager.Save(so);
        curEditPanel.SetActive(false);
        ListControl.GetComponent<ButtonListControl>().UpdateList();
    }

    void Update()
    {
        if (!(EditButton == null))
        {
            if (!(groupNotifButtons == null))
            {
                if (PlayerPrefs.GetInt("GroupsAddToNotif") == 1)
                {
                    EditButton.SetActive(false);
                    groupNotifButtons.SetActive(true);
                }
                else
                {
                    EditButton.SetActive(true);
                    groupNotifButtons.SetActive(false);
                }
            }
        }

        if (!(EditButton == null))
        {
            if (groupNotifButtons == null)
            {
                if (PlayerPrefs.GetInt("GroupsAddToNotif") == 1)
                {
                    EditButton.GetComponent<Button>().enabled = false;
                    EditButton.GetComponent<Image>().enabled = false;
                    //EditButton.SetActive(false);
                }
                else
                {
                    EditButton.GetComponent<Button>().enabled = true;
                    EditButton.GetComponent<Image>().enabled = true;
                    //EditButton.SetActive(true);
                }
            }
        }
    }

    public void addToNotificationFunc() {
        //reset game
        so = SaveManager.Load();
        so.groupsForTest.Clear();
        SaveManager.Save(so);

        curEditPanel.SetActive(false);

        PlayerPrefs.SetInt("GroupsAddToNotif", 1);
    }

    public void crossStopNotificationFunc() {
        PlayerPrefs.SetInt("GroupsAddToNotif", 0);
    }

    public void editGroupFunc() {
        int groupIndex = saveGroupIndex();
        editGroupNamePanel.SetActive(true);
        curEditPanel.SetActive(false);
    }

    private int saveGroupIndex() {
        int saveGroupIndex = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GroupListButton>().groupIndex;
        PlayerPrefs.SetInt("CurrentGroupIndex", saveGroupIndex);
        return saveGroupIndex;
    }

    public string nextScene;//NotificationCreate scene
    public void moveToNotification() {
        crossStopNotificationFunc();
        PlayerPrefs.SetInt("ResultNotif", 1);
        SceneManager.LoadScene(nextScene);
    }
}
