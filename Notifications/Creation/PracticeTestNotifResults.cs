using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeTestNotifResults : MonoBehaviour
{
    private SerializableObject so;
    public string nextScene;//NotificationCreate scene

    //indicate that results should be displayed in notification create scene
    public void saveResultsNotif() {
        PlayerPrefs.SetInt("ResultNotif", 1);
        SceneManager.LoadScene(nextScene);
    }

    public GameObject title;
    public GameObject content;
    void Start(){
        //displays the results from the test in the notification create scene if indicated by playerPref
        if (PlayerPrefs.HasKey("ResultNotif"))
        {
            if (PlayerPrefs.GetInt("ResultNotif") == 1)
            {
                so = SaveManager.Load();
                PlayerPrefs.SetInt("ResultNotif", 0);
                title.GetComponent<InputField>().text = "Practice test Flashcards";

                //assigns each group and its results to the inputfield
                foreach (int groupIndex in so.groupsForTest) {
                    string groupName = so.cardGroups[groupIndex].GroupName;
                    int lastTestScore = so.cardGroups[groupIndex].lastTestScore;
                    int cardCount = so.cardGroups[groupIndex].cards.Count;
                    content.GetComponent<InputField>().text += (groupName + " score: " + lastTestScore + "/" + cardCount + "\n");
                }
            }
        }
    }
}
