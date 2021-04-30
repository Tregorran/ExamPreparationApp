using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardsBackButtonPrefs : MonoBehaviour
{
    //saves current scene to mova back to
    public void onClickMoveBack()
    {
        string SceneToLoad = PlayerPrefs.GetString("NextScene");
        SceneManager.LoadScene(SceneToLoad);
    }

    public void onClickForwardButtonGroups() {
        if (PlayerPrefs.GetInt("GroupsAddToNotif") == 1)
        {
            gameObject.GetComponent<GroupListButton>().GroupSelectedForTest();
        } else {
            int groupIndex = gameObject.GetComponent<GroupListButton>().groupIndex;
            PlayerPrefs.SetInt("CurrentGroupIndex", groupIndex);

            PlayerPrefs.SetString("NextScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("CardsInGroup");
        }
    }
}
