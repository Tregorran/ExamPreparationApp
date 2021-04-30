using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayForEdit : MonoBehaviour
{
    public GameObject firstSide;
    public GameObject firstImageButton;
    //public GameObject firstRawImage;
    public GameObject secondSide;
    public GameObject secondImageButton;
    //public GameObject secondRawImage;
    public Dropdown groupSelection;

    private int cardIndex;
    private int groupIndex;
    private string firstSideText;
    private string secondSideText;
    private string firstImagePath;
    private string secondImagePath;

    private SerializableObject so;
    

    // Start is called before the first frame update
    void Start()
    {
        so = SaveManager.Load();
        cardIndex = PlayerPrefs.GetInt("CardIndex");
        groupIndex = PlayerPrefs.GetInt("GroupIndex");

        firstSideText = so.cardGroups[groupIndex].cards[cardIndex].FirstContent;
        secondSideText = so.cardGroups[groupIndex].cards[cardIndex].SecondContent;
        firstImagePath = so.cardGroups[groupIndex].cards[cardIndex].FirstImagePath;
        secondImagePath = so.cardGroups[groupIndex].cards[cardIndex].SecondImagePath;

        if (!(firstImagePath == null || firstImagePath == "")) { 
            firstImageButton.GetComponent<GetImage>().UploadNewProfileImage(firstImagePath);
        } else {
            firstSide.GetComponent<InputField>().text = firstSideText;
        }

        if (!(secondImagePath == null || secondImagePath == "")) {
            secondImageButton.GetComponent<GetImage>().UploadNewProfileImage(secondImagePath);
        }
        else {
            secondSide.GetComponent<InputField>().text = secondSideText;
        }

        groupSelection.GetComponent<Dropdown>().value = groupIndex + 2;
    }
}
