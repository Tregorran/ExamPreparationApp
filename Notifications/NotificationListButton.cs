using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotificationListButton : MonoBehaviour
{
    [SerializeField]
    private Text notfiDateTimeText;

    [SerializeField]
    private Text titleText;

    [SerializeField]
    private Text contentText;

    private int currentActive;

    public string sceneToLoad;
    public GameObject OnButton;
    public GameObject OffButton;
    public GameObject colourTag;
    public GameObject listControl;

    private SerializableObject so;

    public GameObject MobileNotificationObject;

    public int notifIndex;
    public int triggerIndex;

    public void SetTitle(string textString)
    {
        titleText.text = textString;
    }

    public void SetContent(string textString)
    {
        contentText.text = textString;
    }

    public void SetColour(string hexColour)
    {
        Color myColor;
        ColorUtility.TryParseHtmlString(hexColour, out myColor);
        colourTag.GetComponent<Image>().color = myColor;
    }

    //retrieves details to display on the button within the list
    public void SetNotifNum(int newNotifIndex, int newTriggerIndex, int active, string hour, string minute, string day)
    {
        notifIndex = newNotifIndex;
        triggerIndex = newTriggerIndex;

        so = SaveManager.Load();
        SetTitle(so.notifications[notifIndex].notifTitle);
        SetContent(so.notifications[notifIndex].notifText);
        SetActiveButton(active);
        SetColour(so.notifications[notifIndex].colour);

        notfiDateTimeText.text = day + " | " + hour + ":" + minute;
    }

    public void SetActiveButton(int active) {
        if (active == 0) {
            currentActive = 0;
            OnButton.SetActive(true);
            OffButton.SetActive(false);
        } else {
            currentActive = 1;
            OnButton.SetActive(false);
            OffButton.SetActive(true);
        }
    }

    //button toggle to change active of trigger
    public void changeActive()
    {
        if (OnButton.activeSelf == true)
        {
            currentActive = 1;
            OnButton.SetActive(false);
            OffButton.SetActive(true);

            so.notifications[notifIndex].notifPrompt[triggerIndex].active = 1;
        }
        else
        {
            currentActive = 0;
            OnButton.SetActive(true);
            OffButton.SetActive(false);

            so.notifications[notifIndex].notifPrompt[triggerIndex].active = 0;
        }

        SaveManager.Save(so);
        listControl.GetComponent<NotificationListControl>().UpdateList();//update list 
        MobileNotificationObject.GetComponent<mobileNotifs>().recompileNotifications();//compute changed notification
    }

    //when button is clicked save the notification number for edit
    public void OnClickNotification()
    {
        PlayerPrefs.SetInt("CurrentNotification", notifIndex);
        SceneManager.LoadScene(sceneToLoad);
    }
}
