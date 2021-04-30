using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefsSavePractice : MonoBehaviour
{
    private string SceneToLoad = "SelectGroupsPlay";
    public GameObject ErrorText;
    SerializableObject so;

    public int resetGameGroups = 0;

    void Start()
    {
        if (resetGameGroups == 0)
        {
            so = SaveManager.Load();
            so.groupsForTest.Clear();
            so.groupsForTest = new List<int>();
            SaveManager.Save(so);
        }
    }

    public void onClickMatching() {
        PlayerPrefs.SetString("Game", "Matching");
        loadScene();
    }

    public void onClickType() {
        PlayerPrefs.SetString("Game", "Type Answer");
        loadScene();
    }

    public void onClickSimple() {
        PlayerPrefs.SetString("Game", "Simple Show");
        loadScene();
    }

    private void loadScene() {
        SceneManager.LoadScene(SceneToLoad);
    }



    public void onClickPlayGame()
    {
        SerializableObject so;
        so = SaveManager.Load();

        int numberOfCards = 0;
        //check if the number of cards exceed 4 if not error message
        foreach (int groupNum in so.groupsForTest) {
            numberOfCards += so.cardGroups[groupNum].cards.Count;
        }
        Debug.Log("Num Of Cards: " + numberOfCards);

        if (numberOfCards < 4) {
            ErrorText.SetActive(true);
            return;
        }

        string gameToPlay = PlayerPrefs.GetString("Game");

        if (gameToPlay == "Matching")
        {
            SceneManager.LoadScene("MatchingGame");
        }
        else if (gameToPlay == "Type Answer")
        {
            SceneManager.LoadScene("TypeGame");
        }
        else if (gameToPlay == "Simple Show")
        {
            SceneManager.LoadScene("SimpleGame");
        }
    }
}
