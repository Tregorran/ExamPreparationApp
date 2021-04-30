using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTemplates : MonoBehaviour
{
    public GameObject createGroupPanel;
    public GameObject addGroupTemplatePanel;
    public GameObject toggleUI;
    private SerializableObject so;
    public GameObject ListControl;

    //checks if already added template
    public void addButtonCheckGroupTemplate() {
        int createTemps = PlayerPrefs.GetInt("CreateCardTemps");
        if (createTemps == 1)
        {
            addGroupTemplatePanel.SetActive(true);
        }
        else
        {
            createGroupPanel.SetActive(true);
        }
    }

    //if the user choose not to see template panel
    public void checkToggle()
    {
        if (toggleUI.GetComponent<Toggle>().isOn == true)
        {
            PlayerPrefs.SetInt("CreateCardTemps", 0);
        }
    }

    public void closePanel()
    {
        checkToggle();
        addGroupTemplatePanel.SetActive(false);
    }

    //hard codes the flashcard templates into "so"
    public void createNotifTemps()
    {
        PlayerPrefs.SetInt("CreateCardTemps", 0);
        closePanel();
        so = SaveManager.Load();

        //sets flashcard group details
        SerializableGroupClass newGroup = new SerializableGroupClass();
        newGroup.GroupName = "Example flashcard group";
        newGroup.dateCreated = System.DateTime.Now.ToString("YYYY/MM/DD/HH/mm/ss");
        newGroup.cards = new List<SerialisableCardClass>();

        //adds the newcards to the new flashcard group
        newGroup.cards.Add(createCard("Write the word on the front", "Write the definition or important point on the back and keep " +
            "it short, to the point and in your own words."));
        newGroup.cards.Add(createCard("Goodbye (To French) #Translation", "Au revoir"));
        newGroup.cards.Add(createCard("heritage #Definition", "noun; practices that are handed down from the past by tradition"));
        newGroup.cards.Add(createCard("4x4 = ? #Answer", "4x4 = 16"));
        newGroup.cards.Add(createCard("How tall is Mount Everest? #Facts", "8,848 metres above sea level"));

        //create an image example card
        SerialisableCardClass newCard = new SerialisableCardClass();
        string newPath = saveImage();
        newCard.FirstContent = "Weight #add your own images";
        newCard.SecondImagePath = newPath;
        newCard.dateCreated = System.DateTime.Now.ToString("YYYY/MM/DD/HH/mm/ss");
        newGroup.cards.Add(newCard);

        so.cardGroups.Add(newGroup);
        SaveManager.Save(so);

        ListControl.GetComponent<ButtonListControl>().UpdateList();//update scrollable list to show new group of flashcards
    }

    public Texture2D imageToSave;

    //saves an image to be used in a flashcard
    private string saveImage() {
        string newPath = "";
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(imageToSave, "GalleryTest", "Image.png", (success, path) => 
        newPath = path
        );
        return newPath;
    }

    //assigns sides to a new card
    private SerialisableCardClass createCard(string firstSide, string secondSide) {
        SerialisableCardClass newCard = new SerialisableCardClass();
        newCard.FirstContent = firstSide;
        newCard.SecondContent = secondSide;
        newCard.dateCreated = System.DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");
        return newCard;
    }
}
