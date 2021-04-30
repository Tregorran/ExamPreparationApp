using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateCardScript : MonoBehaviour
{
    //user inputs
    public GameObject firstSideInputField;
    public GameObject firstImageButton;
    public GameObject secondSideInputField;
    public GameObject secondImageButton;
    public Dropdown groupSelection;
    
    //error text
    public GameObject errorText1;
    public GameObject errorText2;
    public GameObject errorText3;

    //storing user inputs
    private string firstSideText;
    private string firstSideImagePath;
    private string secondSideText;
    private string secondSideImagePath;
    private string groupText;
    private int selectedIndex;
    private int errors;

    private SerializableObject so;
    public string SceneToLoad;

    private int edit = 0;
    int cardIndex;
    int groupIndex;

    public void CreateCardFunc() {
        //Get text from inputfields and dropdown
        getInputs();

        //Make sure there is user inputs
        checkErrors();

        if (errors > 0)
        {
            return;
        }

        so = SaveManager.Load();

        addCardToGroup();
        SceneManager.LoadScene(SceneToLoad);
    }

    //Retrieves inputs from inputfields
    private void getInputs() {
        firstSideText = firstSideInputField.GetComponent<Text>().text;
        secondSideText = secondSideInputField.GetComponent<Text>().text;

        firstSideImagePath = firstImageButton.GetComponent<GetImage>().pathSelected;
        secondSideImagePath = secondImageButton.GetComponent<GetImage>().pathSelected;

        selectedIndex = groupSelection.value;
        groupText = groupSelection.options[selectedIndex].text;
    }

    //checks that the inputs are not empty
    private void checkErrors() {
        errors = 0;

        errorText1.SetActive(false);
        errorText2.SetActive(false);
        errorText3.SetActive(false);

        if (firstSideText == "" || firstSideText == null) {
            if (firstSideImagePath == "" || firstSideImagePath == null)
            {
                errorText1.SetActive(true);
                errors += 1;
            }
        }

        if (secondSideText == "" || secondSideText == null)
        {
            if (secondSideImagePath == "" || secondSideImagePath == null)
            {
                errorText2.SetActive(true);
                errors += 1;
            }
        }

        if (selectedIndex == 0)
        {
            errorText3.SetActive(true);
            errors += 1;
        }
    }

    //for edit card button
    public void onClickEditCard() {
        edit = 1;
        so = SaveManager.Load();
        cardIndex = PlayerPrefs.GetInt("CardIndex");
        groupIndex = PlayerPrefs.GetInt("GroupIndex");

        //reduces test score of current group, that flashcard is assigned to
        if (so.cardGroups[groupIndex].cards[cardIndex].cardCorrect == "Correct") {
            so.cardGroups[groupIndex].lastTestScore -= 1;
        }

        //remove card from previous group
        so.cardGroups[groupIndex].cards.RemoveAt(cardIndex);

        //get inputs from inputfields
        getInputs();

        checkErrors();

        if (errors > 0)
        {
            return;
        }
        addCardToGroup();
        SceneManager.LoadScene(SceneToLoad);
    }

    //assigns card the attributes and assigns the card to a group
    private void addCardToGroup() {
        foreach (SerializableGroupClass eachGroup in so.cardGroups)
        {
            if (eachGroup.GroupName == groupText)
            {
                //create new card and assign attributes
                SerialisableCardClass newCard = new SerialisableCardClass();

                newCard.FirstContent = firstSideText;
                newCard.SecondContent = secondSideText;
                newCard.FirstImagePath = firstSideImagePath;
                newCard.SecondImagePath = secondSideImagePath;

                newCard.dateCreated = System.DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");
                eachGroup.cards.Add(newCard);

                SaveManager.Save(so);
                return;
            }
        }
    }
}
