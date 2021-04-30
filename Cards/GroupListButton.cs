using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroupListButton : MonoBehaviour
{
    [SerializeField]
    private Text groupText;

    [SerializeField]
    private Text numText;

    public string SceneToLoad;

    public GameObject sliderScore;
    public Text scoreText;
    public string numOfCards;

    public int groupIndex;
    private SerializableObject so;

    //get details from index given
    public void SetGroupIndex(int index) {
        so = SaveManager.Load();
        groupIndex = index;
        groupText.text = so.cardGroups[groupIndex].GroupName;
        numOfCards = (so.cardGroups[groupIndex].cards.Count).ToString();
        numText.text = ("Cards: " + (so.cardGroups[groupIndex].cards.Count).ToString());
        SetLastScore(so.cardGroups[groupIndex].lastTestScore);
    }

    //displays last score of group
    public void SetLastScore(int scoreInt) {
        if (scoreInt > 0) {
            int numOfCardsInt = int.Parse(numOfCards);
            float totalScoreInt = (float)scoreInt/(float)numOfCardsInt;
            sliderScore.GetComponent<Slider>().value = totalScoreInt;
        }
        else {
            sliderScore.GetComponent<Slider>().value = 0;
        }
        scoreText.text = "Last test: " + scoreInt.ToString() + "/" + numOfCards;
    }

    //When group is clicked transition to groupofflashcards scene
    //and save group index
    public void OnClickGroup() {
        PlayerPrefs.SetInt("CurrentGroupIndex", groupIndex);
        Debug.Log("CurrentGroup: " + groupIndex);
        SceneManager.LoadScene(SceneToLoad);
    }

    private int selected = 0;

    //for practice test, toggle if groups gets selected or
    //deselect for practice test
    public void GroupSelectedForTest() {
        SerializableObject so;
        so = SaveManager.Load();
        //Add group to practice test
        if (selected == 0)
        {
            so.groupsForTest.Add(groupIndex);

            selected = 1;
            gameObject.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.6f, 1f);
        }
        else if (selected == 1) {
            //remove group from practice test
            for (int i = 0; i < so.groupsForTest.Count; i++) {
                if (so.groupsForTest[i] == groupIndex) {
                    so.groupsForTest.RemoveAt(i);
                    break;
                }
            }

            selected = 0;
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        SaveManager.Save(so);
    }

    public GameObject editPanel;
    public GameObject closeEditPanelButton;
    public GameObject editGroupTextField;

    //open the edit panel for this group
    public void editPanelOpen() {
        Debug.Log("GroupIndex: " + groupIndex);
        PlayerPrefs.SetInt("CurrentGroupIndex", groupIndex);
        editPanel.SetActive(true);
        closeEditPanelButton.SetActive(true);
        editGroupTextField.GetComponent<GroupTitleScript>().showGroupNameInfo();
    }

    //for adding group to notification, when nolonger in selection mode
    //make gameobject white again
    void Update()
    {
        if ((PlayerPrefs.GetInt("GroupsAddToNotif") == 0) && (selected == 0))
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
