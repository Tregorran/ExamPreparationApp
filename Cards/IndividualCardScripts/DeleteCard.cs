using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteCard : MonoBehaviour
{
    //reduce size of group.count
    //reduce number of correct cards in group

    private SerializableObject so;
    private int cardIndex;
    private int groupIndex;
    public string SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        cardIndex = PlayerPrefs.GetInt("CardIndex");
        groupIndex = PlayerPrefs.GetInt("GroupIndex");
    }

    public void DeleteCardFunc() {
        so = SaveManager.Load();

        if (so.cardGroups[groupIndex].cards[cardIndex].cardCorrect == "Correct") {
            so.cardGroups[groupIndex].lastTestScore -= 1;
        }
        so.cardGroups[groupIndex].cards.RemoveAt(cardIndex);
        SaveManager.Save(so);
        SceneManager.LoadScene(SceneToLoad);
    }

    public void editCardFunc() {
        int saveGroupIndex = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<CardsListButton>().groupIndex;
        int saveCardIndex = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<CardsListButton>().cardIndex;

        PlayerPrefs.SetInt("CardIndex", saveCardIndex);
        PlayerPrefs.SetInt("GroupIndex", saveGroupIndex);
        SceneManager.LoadScene(SceneToLoad);
    }
}
