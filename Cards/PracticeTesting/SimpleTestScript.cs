using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleTestScript : MonoBehaviour
{
    SerializableObject so;
    public int testIndex = 0;//current index of which card in the practiceTest

    private int sideShow = 0;

    //will contain the randomised cards for the practice test of all groups selected
    public List<ClassPracticeTest> practiceTestList = new List<ClassPracticeTest>();

    public GameObject sideShowingText;
    public Text progressText;
    public Slider progress;

    public GameObject outputText;
    public GameObject outputImage;

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
        foreach (int groupIndex in so.groupsForTest) {
            for (int i = 0; i < so.cardGroups[groupIndex].cards.Count; i++) {
                randomPos = Random.Range(0, practiceTestList.Count);

                ClassPracticeTest testCard = new ClassPracticeTest();
                testCard.groupNum = groupIndex;
                testCard.cardNum = i;
                practiceTestList.Insert(randomPos, testCard);
            }
        }

        //display first card
        displayCard(practiceTestList[0].groupNum, practiceTestList[0].cardNum, 0);
    }
    
    //display the card, and which side
    private void displayCard(int curGroupIndex, int curCardIndex, int side)
    {
        updateOrderSlider();
        if (side == 0)
        {
            sideShowingText.GetComponent<Text>().text = "First side";
            string firstContent = so.cardGroups[curGroupIndex].cards[curCardIndex].FirstContent;
            string firstPath = so.cardGroups[curGroupIndex].cards[curCardIndex].FirstImagePath;
            displayContent(firstContent, firstPath);
        }
        else if (side == 1)
        {
            sideShowingText.GetComponent<Text>().text = "Second side";
            string secondContent = so.cardGroups[curGroupIndex].cards[curCardIndex].SecondContent;
            string secondPath = so.cardGroups[curGroupIndex].cards[curCardIndex].SecondImagePath;
            displayContent(secondContent, secondPath);
        }
    }

    //Displays the text or the image of the card
    private void displayContent(string content, string path) {
        if (path == "" || path == null)
        {
            outputImage.SetActive(false);
            outputText.GetComponent<InputField>().text = content;
        }
        else {
            outputImage.SetActive(true);
            outputText.GetComponent<InputField>().text = "";

            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + path);
                return;
            }
            outputImage.GetComponent<RawImage>().texture = texture;
        }
    }

    //If the user got the card correct
    public void correctFunc()
    {
        outputText.GetComponent<Animator>().SetTrigger("right");
        StartCoroutine(SwipeRightAnimWait());
        correctScore += 1;
    }

    //increments testIndex for next card in list and set card to correct
    IEnumerator SwipeRightAnimWait()
    {
        yield return new WaitForSeconds(0.34f);
        sideShow = 0;
        practiceTestList[testIndex].cardCorrect = "Correct";
        testIndex += 1;

        checkFinished();
    }

    //If the user got the card incorrect
    public void incorrectFunc()
    {
        outputText.GetComponent<Animator>().SetTrigger("left");
        StartCoroutine(SwipeLeftAnimWait());
        incorrectScore += 1;
    }

    //increments testIndex for next card in list and set card to incorrect
    IEnumerator SwipeLeftAnimWait()
    {
        yield return new WaitForSeconds(0.34f);
        sideShow = 0;
        practiceTestList[testIndex].cardCorrect = "Incorrect";
        testIndex += 1;

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
            //display next card
            displayCard(practiceTestList[testIndex].groupNum, practiceTestList[testIndex].cardNum, sideShow);
        }
    }

    //toggle flips the current card
    public void FlipCardFunc()
    {
        if (sideShow == 0)
        {
            sideShow = 1;
            outputText.GetComponent<Animator>().SetTrigger("flip");

            outputText.GetComponent<InputField>().text = "";
            outputImage.GetComponent<RawImage>().texture = null;
            outputImage.SetActive(false);

            StartCoroutine(WaitForFlip1());
        }
        else {
            sideShow = 0;
            outputText.GetComponent<Animator>().SetTrigger("flip");

            outputText.GetComponent<InputField>().text = "";
            outputImage.GetComponent<RawImage>().texture = null;
            outputImage.SetActive(false);

            StartCoroutine(WaitForFlip1());
        }
    }

    IEnumerator WaitForFlip1()
    {
        yield return new WaitForSeconds(0.32f);
        outputImage.SetActive(true);
        displayCard(practiceTestList[testIndex].groupNum, practiceTestList[testIndex].cardNum, sideShow);
    }

    //displays position of user in test
    private void updateOrderSlider() {
        float curOrder = testIndex + 1;

        float sliderValue = curOrder / practiceTestList.Count;
        progress.value = sliderValue;

        progressText.text = "Cards: " + curOrder + "/" + practiceTestList.Count;
    }

    SwipeControls sc = new SwipeControls();

    //set correct and incorrect scores and retrieve swipe inputs
    private void Update()
    {
        CorrectScoreText.GetComponent<Text>().text = "Correct: " + correctScore;
        IncorrectScoreText.GetComponent<Text>().text = "Incorrect: " + incorrectScore;

        string swipeStr = sc.Swipe();
        if (swipeStr == "Left")
        {
            incorrectFunc();
        }
        else if (swipeStr == "Right")
        {
            correctFunc();
        }
        else if (swipeStr == "Tap")
        {
            FlipCardFunc();
        }
        swipeStr = "";
    }
}
