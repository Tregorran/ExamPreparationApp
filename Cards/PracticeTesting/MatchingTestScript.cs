using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchingTestScript : MonoBehaviour
{
    public GameObject FirstSideText;
    public GameObject FirstSideImage;
    public GameObject TopLeftText;
    public GameObject TopLeftImage;
    public GameObject TopRightText;
    public GameObject TopRightImage;
    public GameObject BottomLeftText;
    public GameObject BottomLeftImage;
    public GameObject BottomRightText;
    public GameObject BottomRightImage;

    private SerializableObject so;

    public Text progressText;
    public Slider progress;
    private int testIndex = 0;//current index of which card in the practiceTest

    //will contain the randomised cards for the practice test of all groups selected
    private List<ClassPracticeTest> practiceTestList = new List<ClassPracticeTest>();

    private int TopLeftIndex;
    private int TopRightIndex;
    private int BottomLeftIndex;
    private int BottomRightIndex;

    //For displaying correct and incorrect scores
    private int correctScore = 0;
    private int incorrectScore = 0;
    public GameObject CorrectScoreText;
    public GameObject IncorrectScoreText;

    void Start()
    {
        so = SaveManager.Load();
        int randomPos;

        //randomly arrange cards of each group selected into a list
        foreach (int groupIndex in so.groupsForTest)
        {
            for (int i = 0; i < so.cardGroups[groupIndex].cards.Count; i++)
            {
                randomPos = Random.Range(0, practiceTestList.Count);

                ClassPracticeTest testCard = new ClassPracticeTest();
                testCard.groupNum = groupIndex;
                testCard.cardNum = i;
                practiceTestList.Insert(randomPos, testCard);
            }
        }

        updateOrderSlider();
        displayCard();
    }

    //display the user correct and incorrect scores
    void Update()
    {
        CorrectScoreText.GetComponent<Text>().text = "Correct: " + correctScore;
        IncorrectScoreText.GetComponent<Text>().text = "Incorrect: " + incorrectScore;
    }

    //The 4 options the user can select
    List<int> CardsAssignment = new List<int>();

    //displays the 5 cards
    //1 being the first side of a card
    private void displayCard() {
        //store current test index
        CardsAssignment.Clear();
        CardsAssignment.Add(testIndex);

        //displays the first side of card
        DisplayGetInfo(testIndex, FirstSideText, FirstSideImage, 0);

        //assign 4 random cards to the options in cardsAssignment
        while (CardsAssignment.Count < 4)
        {
            int randomNum = Random.Range(0, practiceTestList.Count);
            if (!(CardsAssignment.Contains(randomNum)) && (testIndex != randomNum))
            {
                CardsAssignment.Add(randomNum);
            }
        }
        
        //Assigning the UI buttons cards
        TopLeftIndex = Random.Range(0, CardsAssignment.Count);
        DisplayGetInfo(CardsAssignment[TopLeftIndex], TopLeftText, TopLeftImage, 1);
        CardsAssignment.RemoveAt(TopLeftIndex);

        TopRightIndex = Random.Range(0, CardsAssignment.Count);
        DisplayGetInfo(CardsAssignment[TopRightIndex], TopRightText, TopRightImage, 1);
        CardsAssignment.RemoveAt(TopRightIndex);

        BottomLeftIndex = Random.Range(0, CardsAssignment.Count);
        DisplayGetInfo(CardsAssignment[BottomLeftIndex], BottomLeftText, BottomLeftImage, 1);
        CardsAssignment.RemoveAt(BottomLeftIndex);

        BottomRightIndex = Random.Range(0, CardsAssignment.Count);
        DisplayGetInfo(CardsAssignment[BottomRightIndex], BottomRightText, BottomRightImage, 1);
        CardsAssignment.RemoveAt(BottomRightIndex);
    }

    //user got card correct, increments testIndex and sets card to correct
    private void CorrectCard() {
        practiceTestList[testIndex].cardCorrect = "Correct";
        testIndex += 1;
        correctScore += 1;
        updateOrderSlider();
        checkFinished();
    }

    //user got card incorrect, increments testIndex and sets card to incorrect
    private void IncorrectCard() {
        practiceTestList[testIndex].cardCorrect = "Incorrect";
        testIndex += 1;
        incorrectScore += 1;
        updateOrderSlider();
        checkFinished();
    }

    //For checking whether the user has reached the end of the test
    private void checkFinished()
    {
        //if user has answered all cards in the practiceTestList
        if (testIndex >= practiceTestList.Count)
        {
            //puts in all the scores for the groups from test into "so"
            foreach (ClassPracticeTest eachTestCard in practiceTestList)
            {
                so.cardGroups[eachTestCard.groupNum].cards[eachTestCard.cardNum].cardCorrect = eachTestCard.cardCorrect;
            }

            //sets the scores for all the cards in test into "so"
            foreach (int groupNum in so.groupsForTest)
            {
                int numCorrect = 0;
                for (int i = 0; i < so.cardGroups[groupNum].cards.Count; i++)
                {
                    if (so.cardGroups[groupNum].cards[i].cardCorrect == "Correct")
                    {
                        numCorrect += 1;
                    }
                }
                so.cardGroups[groupNum].lastTestScore = numCorrect;
            }
            SaveManager.Save(so);
            SceneManager.LoadScene("ResultScene");
        }
        else
        {
            displayCard();
        }
    }

    //UI Button in top left
    public void onClickTopLeft() {
        if (TopLeftIndex == 0)
        {
            CorrectCard();
        }
        else {
            IncorrectCard();
        }
    }

    //UI Button in top right
    public void onClickTopRight() {
        if (TopRightIndex == 0)
        {
            CorrectCard();
        }
        else
        {
            IncorrectCard();
        }
    }

    //UI Button in bottom left
    public void onClickBottomLeft() {
        if (BottomLeftIndex == 0)
        {
            CorrectCard();
        }
        else
        {
            IncorrectCard();
        }
    }

    //UI Button in bottom right
    public void onClickBottomRight() {
        if (BottomRightIndex == 0)
        {
            CorrectCard();
        }
        else
        {
            IncorrectCard();
        }
    }

    //retrieves details of the card from "so"
    private void DisplayGetInfo(int index, GameObject contentObject, GameObject imageObject, int side)
    {
        int curGroupIndex = practiceTestList[index].groupNum;
        int curCardIndex = practiceTestList[index].cardNum;
        string content;
        string path;
        if (side == 0)
        {
            content = so.cardGroups[curGroupIndex].cards[curCardIndex].FirstContent;
            path = so.cardGroups[curGroupIndex].cards[curCardIndex].FirstImagePath;
        }
        else {
            content = so.cardGroups[curGroupIndex].cards[curCardIndex].SecondContent;
            path = so.cardGroups[curGroupIndex].cards[curCardIndex].SecondImagePath;
        }

        DisplayFunc(contentObject, imageObject, content, path);
    }

    //displays given, of either text or image of the card in the gameobject button
    private void DisplayFunc(GameObject contentObject, GameObject imageObject, string content, string path)
    {
        if (path == "" || path == null)
        {
            imageObject.SetActive(false);
            contentObject.GetComponent<InputField>().text = content;
        }
        else
        {
            imageObject.SetActive(true);
            contentObject.GetComponent<InputField>().text = "";

            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + path);
                return;
            }
            imageObject.GetComponent<RawImage>().texture = texture;
        }
    }

    //displays position of user in test
    private void updateOrderSlider()
    {
        float curOrder = testIndex + 1;

        float sliderValue = curOrder / practiceTestList.Count;
        progress.value = sliderValue;
        
        progressText.text = "Cards: " + curOrder + "/" + practiceTestList.Count;
    }
}
