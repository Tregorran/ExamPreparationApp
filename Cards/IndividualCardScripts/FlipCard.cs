using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCard : MonoBehaviour
{
    public GameObject cardSideInputField;
    public GameObject imageField;

    public Text cardOrder;

    public GameObject sideShowingText;
    public GameObject cardOrderBar;
    public GameObject correctFillBar;

    private string firstSideText;
    private string imagePathFirstSide;
    private string secondSideText;
    private string imagePathSecondSide;

    private int sideShowing = 0;
    private SerializableObject so;
    private int index;

    private int cardIndex;
    private int groupIndex;

    //retrieve indexes of selected card
    void Start() {
        cardIndex = PlayerPrefs.GetInt("CardIndex");
        groupIndex = PlayerPrefs.GetInt("GroupIndex");

        getCardDetails();
    }

    //get previous card, if not first card
    public void prevCard() {
        if ((cardIndex) > 0)
        {
            cardSideInputField.GetComponent<Animator>().SetTrigger("right");
        }
        StartCoroutine(SwipeRightAnimWait());
    }

    //get next card, if not last card
    public void nextCard() {
        if ((cardIndex) < so.cardGroups[groupIndex].cards.Count - 1)
        {
            cardSideInputField.GetComponent<Animator>().SetTrigger("left");
        }
        StartCoroutine(SwipeLeftAnimWait());      
    }

    //decrease card index for previous card
    IEnumerator SwipeRightAnimWait()
    {
        yield return new WaitForSeconds(0.34f);
        if ((cardIndex) > 0)
        {
            cardIndex -= 1;
            PlayerPrefs.SetInt("CardIndex", cardIndex);
        }
        getCardDetails();
    }

    //increase card index for next card
    IEnumerator SwipeLeftAnimWait()
    {
        yield return new WaitForSeconds(0.34f);
        if ((cardIndex) < so.cardGroups[groupIndex].cards.Count - 1)
        {
            cardIndex += 1;
            PlayerPrefs.SetInt("CardIndex", cardIndex);
        }
        getCardDetails();
    }

    //retrieve details of the card from indexs
    //and display information of card
    public void getCardDetails()
    {
        so = SaveManager.Load();
        firstSideText = so.cardGroups[groupIndex].cards[cardIndex].FirstContent;
        secondSideText = so.cardGroups[groupIndex].cards[cardIndex].SecondContent;
        imagePathFirstSide = so.cardGroups[groupIndex].cards[cardIndex].FirstImagePath;
        imagePathSecondSide = so.cardGroups[groupIndex].cards[cardIndex].SecondImagePath;
        
        //calculates position of card in group
        float curCardOrder = cardIndex+1;
        float totalCardOrder = so.cardGroups[groupIndex].cards.Count;
        float totalScoreFloat = (curCardOrder / totalCardOrder);
        cardOrderBar.GetComponent<Slider>().value = totalScoreFloat;

        showFirstSide();

        string cardScore = so.cardGroups[groupIndex].cards[cardIndex].cardCorrect;
        SetScoreCard(cardScore);

        cardOrder.text = "Cards: " + curCardOrder + "/" + totalCardOrder;
    }

    //set colour depending on if correct or incorrect card
    public void SetScoreCard(string score)
    {
        if (score == "" || score == null)
        {
            correctFillBar.GetComponent<Image>().color = Color.gray;
        }
        else if (score == "Incorrect")
        {
            correctFillBar.GetComponent<Image>().color = Color.red;
        }
        else if (score == "Correct")
        {
            correctFillBar.GetComponent<Image>().color = Color.green;
        }
    }

    //displays the contents for the first side of the card
    //checks if text is empty then displays image,
    //if text not empty display text for card
    private void showFirstSide() {
        sideShowingText.GetComponent<Text>().text = "First side";
        if (firstSideText == "" || firstSideText == null)
        {
            imageField.SetActive(true);
            cardSideInputField.GetComponent<InputField>().text = "";


            Texture2D texture = NativeGallery.LoadImageAtPath(imagePathFirstSide);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + imagePathFirstSide);
                return;
            }

            imageField.GetComponent<RawImage>().texture = texture;
        }
        else {
            imageField.SetActive(false);
            cardSideInputField.GetComponent<InputField>().text = firstSideText;
        }
    }

    //displays the contents for the second side of the card
    //checks if text is empty then displays image,
    //if text not empty display text for card
    private void showSecondSide()
    {
        sideShowingText.GetComponent<Text>().text = "Second side";
        if (secondSideText == "" || secondSideText == null)
        {
            imageField.SetActive(true);
            cardSideInputField.GetComponent<InputField>().text = "";


            Texture2D texture = NativeGallery.LoadImageAtPath(imagePathSecondSide);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + imagePathSecondSide);
                return;
            }
            imageField.GetComponent<RawImage>().texture = texture;
        }
        else
        {
            imageField.SetActive(false);
            cardSideInputField.GetComponent<InputField>().text = secondSideText;
        }
    }

    //toggles which side of the card to show
    public void FlipCardFunc() {
        if (sideShowing == 0)
        {
            cardSideInputField.GetComponent<Animator>().SetTrigger("flip");
            cardSideInputField.GetComponent<InputField>().text = "";
            imageField.GetComponent<RawImage>().texture = null;
            imageField.SetActive(false);
            StartCoroutine(WaitForFlip1());
            
        }
        else {
            cardSideInputField.GetComponent<Animator>().SetTrigger("flip");
            cardSideInputField.GetComponent<InputField>().text = "";
            imageField.GetComponent<RawImage>().texture = null;
            imageField.SetActive(false);
            StartCoroutine(WaitForFlip2());
            
        }
    }


    IEnumerator WaitForFlip1()
    {
        yield return new WaitForSeconds(0.32f);
        imageField.SetActive(true);
        showSecondSide();
        sideShowing = 1;
    }


    IEnumerator WaitForFlip2()
    {
        yield return new WaitForSeconds(0.32f);
        imageField.SetActive(true);
        showFirstSide();
        sideShowing = 0;
    }

    SwipeControls sc = new SwipeControls();

    private void Update()
    {
        string swipeStr = sc.Swipe();

        if (swipeStr == "Left")
        {
            nextCard();
        }
        else if (swipeStr == "Right")
        {
            prevCard();
        }
        else if (swipeStr == "Tap") {
            FlipCardFunc();
        }
        swipeStr = "";
    }
}
