using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleButtonFeatures : MonoBehaviour
{
    private SerializableObject so;
    public InputField groupNameField;
    public GameObject panel;
    public Dropdown dropDownGroup;

    public string SceneToLoad;

    public GameObject theText;
    public AudioSource clearSound;
    public GameObject thePanel;

    public GameObject list;

    //Store name entered in field to the serialized list
    /*public void CreateGroupFunc() {
        CreateGroup();

        GroupDownDownScript GDDS = dropDownGroup.GetComponent<GroupDownDownScript>();
        GDDS.UpdateList();
        dropDownGroup.GetComponent<Dropdown>().value = dropDownGroup.GetComponent<Dropdown>().options.Count;
        ClearInputField();
        ClosePanel();
    }*/

    public void UpdateDisp()
    {
        list.GetComponent<ButtonListControl>().UpdateList();
    }

    /*public void CreateGroup() {
        string groupNameText = groupNameField.GetComponent<InputField>().text;
        so = SaveManager.Load();

        SerializableCardClass sc = new SerializableCardClass();
        sc.cards = new List<string>();
        sc.cards.Add(groupNameText);
        so.cardGroups.Add(sc);

        SaveManager.Save(so);
        Debug.Log("Added " + groupNameText + " to card groups");
    }*/

    public void ClosePanel() {
        panel.SetActive(false);
    }

    public void ClearInputField() {
        groupNameField.GetComponent<InputField>().text = "";
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    private int togglePanel = 0;
    public void TogglePanel() {
        if (togglePanel == 0)
        {
            panel.SetActive(true);
            togglePanel = 1;
        }
        else if (togglePanel == 1)
        {
            panel.SetActive(false);
            togglePanel = 0;
        }    
    }
    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ClearText()
    {
        theText.GetComponent<InputField>().text = "";
        clearSound.Play();
    }
    public void CancelButton()
    {
        thePanel.SetActive(false);
    }

    public void CloseButton()
    {
        thePanel.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
