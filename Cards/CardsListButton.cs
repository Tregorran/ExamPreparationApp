using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardsListButton : MonoBehaviour
{
    [SerializeField]
    private Text CardFirstSideText;

    public string SceneToLoad;

    public int cardIndex;
    public int groupIndex;

    public GameObject Image;
    public GameObject sliderFillCorrect;
    private SerializableObject so;

    public void setCardIndex(int index) {
        cardIndex = index;
    }
    
    //retrieves information of card
    public void setGroupIndex(int index) {
        groupIndex = index;
        so = SaveManager.Load();

        SetText(so.cardGroups[groupIndex].cards[cardIndex].FirstContent);
        SetImagePath(so.cardGroups[groupIndex].cards[cardIndex].FirstImagePath);
        SetScoreCard(so.cardGroups[groupIndex].cards[cardIndex].cardCorrect);
    }

    //displays first side of card
    public void SetText(string textString)
    {
        if (!(textString == "" || textString == null)) {
            CardFirstSideText.text = textString;
        }
    }

    //displays first side image of card
    public void SetImagePath(string imagePathString) {
        if (!(imagePathString == null || imagePathString == ""))
        {
            Image.SetActive(true);

            Texture2D texture = NativeGallery.LoadImageAtPath(imagePathString);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + imagePathString);
                return;
            }
            Image.GetComponent<RawImage>().texture = texture;
        }
    }

    //displays the practice test score of the card
    public void SetScoreCard(string score) {
        if (score == "" || score == null) {
            sliderFillCorrect.GetComponent<Image>().color = Color.gray;
        } else if (score == "Incorrect") {
            sliderFillCorrect.GetComponent<Image>().color = Color.red;
        } else if (score == "Correct") {
            sliderFillCorrect.GetComponent<Image>().color = Color.green;
        }
    }

    //when card is clicked save indexes and transition to individual
    //card scene
    public void OnClickCard()
    {
        PlayerPrefs.SetInt("CardIndex", cardIndex);
        PlayerPrefs.SetInt("GroupIndex", groupIndex);
        Debug.Log("CurrentCard: " + CardFirstSideText.text);
        SceneManager.LoadScene(SceneToLoad);
    }
}
