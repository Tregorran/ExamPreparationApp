using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private List<GameObject> buttons;

    private SerializableObject so;
    public int Disp;

    private int sortGroups;
    private int flipGroups;
    private int sortCards;
    private int flipCards;

    void Start()
    {
        //create new list of buttons to create
        buttons = new List<GameObject>();
        setSorts();
        UpdateList();
    }

    //retrieves sort values
    private void setSorts() {
        int temp;

        temp = PlayerPrefs.GetInt("SortGroup");
        Debug.Log(temp);
        if (temp == null)
        {
            sortGroups = 0;
        }
        else {
            sortGroups = temp;
        }

        temp = PlayerPrefs.GetInt("FlipGroup");
        Debug.Log("groupCheck: " + temp);
        if (temp == null)
        {
            flipGroups = 0;
            PlayerPrefs.SetInt("FlipGroup",0);
        }
        else
        {
            flipGroups = temp;
        }

        temp = PlayerPrefs.GetInt("SortCard");
        if (temp == null)
        {
            sortCards = 0;
        }
        else
        {
            sortCards = temp;

        }

        temp = PlayerPrefs.GetInt("FlipCard");
        Debug.Log("checkCard: " + temp);
        if (temp == null)
        {
            flipCards = 0;
            PlayerPrefs.SetInt("FlipCard",0);
        }
        else
        {
            flipCards = temp;
        }

    }

    //decides on which list to display
    public void UpdateList() {
        so = SaveManager.Load();
        if (Disp == 0)
        {
            UpdateDispGroups();
            AscendingDropDown.value = PlayerPrefs.GetInt("FlipGroup");
        }
        else if (Disp == 1)
        {
            UpdateDispCardsOfGroup();
            AscendingDropDown.value = PlayerPrefs.GetInt("FlipCard");
        }
        //game results
        else if (Disp == 2) {
            UpdateDispTestGroups();
            AscendingDropDown.value = PlayerPrefs.GetInt("FlipGroup");
        }
    }

    //reset list by removing buttons instantiated
    private void RemoveButtonsFromList() {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }


    //display list of each group
    public void UpdateDispGroups()
    {
        activateButtonsGroup();//sets colours of sort bar
        RemoveButtonsFromList();//reset list
        //displays groups
        if (!(so.cardGroups.Count == 0))
        {
            SortingGroups();

            //loop through groups within "so" and creates a button for each and assign group index
            for (int j = 0; j < so.cardGroups.Count; j++) {
                if (so.cardGroups[j].GroupName.Contains(searchContent))
                {
                    GameObject button = Instantiate(buttonTemplate) as GameObject;
                    button.SetActive(true);
                    button.GetComponent<GroupListButton>().SetGroupIndex(j);
                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                    buttons.Add(button);
                }
            }
        }
    }

    int currentGroupIndex;
    //display each of the cards within a group
    public void UpdateDispCardsOfGroup()
    {
        activateButtonsCard();//sets colours of sort bar
        RemoveButtonsFromList();//reset list

        currentGroupIndex = PlayerPrefs.GetInt("CurrentGroupIndex");//retrieve selected group index
        if (!(so.cardGroups[currentGroupIndex].cards.Count == 0))
        {
            SortingCards();
            //display each of the flashcards within the group with the selected index
            for (int j = 0; j < so.cardGroups[currentGroupIndex].cards.Count; j++)
            {
                if (so.cardGroups[currentGroupIndex].cards[j].FirstContent.Contains(searchContent))
                {
                    GameObject button = Instantiate(buttonTemplate) as GameObject;
                    button.SetActive(true);
                    button.GetComponent<CardsListButton>().setCardIndex(j);
                    button.GetComponent<CardsListButton>().setGroupIndex(currentGroupIndex);
                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                    buttons.Add(button);
                }
            }
        }
    }

    //displays the list of groups selected for practice test
    public void UpdateDispTestGroups()
    {
        activateButtonsGroup();//sets colours of sort bar
        RemoveButtonsFromList();//reset list
        //displays groups
        if (!(so.cardGroups.Count == 0))
        {
            SortingGroups();

            //display each group of flashcards
            for (int j = 0; j < so.cardGroups.Count; j++)
            {
                //filter only the cards used in the practice test
                if (so.groupsForTest.Contains(j))
                {
                    if (so.cardGroups[j].GroupName.Contains(searchContent))
                    {
                        GameObject button = Instantiate(buttonTemplate) as GameObject;
                        button.SetActive(true);
                        button.GetComponent<GroupListButton>().SetGroupIndex(j);
                        button.transform.SetParent(buttonTemplate.transform.parent, false);
                        buttons.Add(button);
                    }
                }
            }
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public GameObject searchBar;
    private string searchContent = "";
    public void onClickSearchBar() {
        searchContent = searchBar.GetComponent<InputField>().text;
        UpdateList();
    }

    public void onClickClearSearchBar() {
        searchContent = "";
        searchBar.GetComponent<InputField>().text = "";
        UpdateList();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public GameObject alphabetButton;
    public GameObject createButton;
    public GameObject scoreButton;


    private void SortingGroups() {
        if (sortGroups == 0)
        {
            so.cardGroups = so.cardGroups.OrderBy(o => o.dateCreated).ToList();
        } else if (sortGroups == 1) {
            so.cardGroups = so.cardGroups.OrderBy(o => o.GroupName).ToList();
        } else if (sortGroups == 2) {
            so.cardGroups = so.cardGroups.OrderBy(o => o.lastTestScore).ToList();
        }


        //has to reverse everytime
        if (flipGroups == 1) {
            so.cardGroups.Reverse();
            Debug.Log("DOES FLIP");
        }
        SaveManager.Save(so);
    }

    public void SortCreatedGroups()
    {
        sortGroups = 0;
        PlayerPrefs.SetInt("SortGroup", sortGroups);
        UpdateList();
    }

    public void SortAlphabetGroups()
    {
        sortGroups = 1;
        PlayerPrefs.SetInt("SortGroup", sortGroups);
        UpdateList();
    }

    public void SortScoreGroups()
    {
        sortGroups = 2;
        PlayerPrefs.SetInt("SortGroup", sortGroups);
        UpdateList();
    }
    public void SortFlipGroup()
    {
        int curOption = AscendingDropDown.value;
        if (curOption == 0)
        {
            flipGroups = 0;
        }
        else {
            flipGroups = 1;
        }

        PlayerPrefs.SetInt("FlipGroup", curOption);
        UpdateList();
    }

    private void activateButtonsGroup() {

        if (sortGroups == 0)
        {
            createButton.GetComponent<Image>().color = Color.yellow;
            alphabetButton.GetComponent<Image>().color = Color.white;
            scoreButton.GetComponent<Image>().color = Color.white;
        }
        else if (sortGroups == 1)
        {
            createButton.GetComponent<Image>().color = Color.white;
            alphabetButton.GetComponent<Image>().color = Color.yellow;
            scoreButton.GetComponent<Image>().color = Color.white;
        }
        else if (sortGroups == 2)
        {
            createButton.GetComponent<Image>().color = Color.white;
            alphabetButton.GetComponent<Image>().color = Color.white;
            scoreButton.GetComponent<Image>().color = Color.yellow;
        }
    }


    private void SortingCards()
    {
        if (sortCards == 0)
        {
            so.cardGroups[currentGroupIndex].cards = so.cardGroups[currentGroupIndex].cards.OrderBy(o => o.dateCreated).ToList();
        } else if (sortCards == 1)
        {
            so.cardGroups[currentGroupIndex].cards = so.cardGroups[currentGroupIndex].cards.OrderBy(o => o.FirstContent).ToList();
        }
        else if (sortCards == 2)
        {
            so.cardGroups[currentGroupIndex].cards = so.cardGroups[currentGroupIndex].cards.OrderBy(o => o.cardCorrect).ToList();
        }


        if (flipCards == 1)
        {
            Debug.Log("FLIP");
            so.cardGroups[currentGroupIndex].cards.Reverse();
        }
        SaveManager.Save(so);
    }

    public void SortCreatedCards()
    {
        sortCards = 0;
        PlayerPrefs.SetInt("SortCard", sortCards);
        UpdateList();
    }

    public void SortAlphabetCards()
    {
        sortCards = 1;
        PlayerPrefs.SetInt("SortCard", sortCards);
        UpdateList();
    }

    public void SortScoreCards()
    {
        sortCards = 2;
        PlayerPrefs.SetInt("SortCard", sortCards);
        UpdateList();
    }

    public Dropdown AscendingDropDown;
    public void SortFlipCard()
    {
        Debug.Log("changed: " + AscendingDropDown.value);
        int curOption = AscendingDropDown.value;
        if (curOption == 0) {
            flipCards = 0;
        } else
        {
            flipCards = 1;
        }

        PlayerPrefs.SetInt("FlipCard", flipCards);
        UpdateList();
    }

    private void activateButtonsCard()
    {
        if (sortCards == 0)
        {
            createButton.GetComponent<Image>().color = Color.yellow;
            alphabetButton.GetComponent<Image>().color = Color.white;
            scoreButton.GetComponent<Image>().color = Color.white;
        }
        else if (sortCards == 1)
        {
            createButton.GetComponent<Image>().color = Color.white;
            alphabetButton.GetComponent<Image>().color = Color.yellow;
            scoreButton.GetComponent<Image>().color = Color.white;
        }
        else if (sortCards == 2)
        {
            createButton.GetComponent<Image>().color = Color.white;
            alphabetButton.GetComponent<Image>().color = Color.white;
            scoreButton.GetComponent<Image>().color = Color.yellow;
        }
    }
}
