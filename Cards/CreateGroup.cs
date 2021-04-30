using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGroup : MonoBehaviour
{
    private SerializableObject so;
    public InputField groupNameField;
    public GameObject panel;

    public GameObject list;

    public GameObject errorObj;

    public void CreateGroupFunc()
    {
        string groupNameText = groupNameField.GetComponent<InputField>().text;

        if (groupNameText == "" || groupNameText == null)
        {
            errorObj.SetActive(true);
        }
        else {
            errorObj.SetActive(false);

            so = SaveManager.Load();

            SerializableGroupClass sc = new SerializableGroupClass();
            sc.GroupName = groupNameText;
            sc.cards = new List<SerialisableCardClass>();

            sc.dateCreated = System.DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");

            so.cardGroups.Add(sc);

            SaveManager.Save(so);
            Debug.Log("Added " + groupNameText + " to card groups");

            ClearInputField();
            UpdateDispList();
            ClosePanel();
        }
    }

    public void EditGroupNameFunc()
    {
        string groupNameText = groupNameField.GetComponent<InputField>().text;
        if (groupNameText == "" || groupNameText == null) {
            errorObj.SetActive(true);
        } else
        {
            errorObj.SetActive(false);

            so = SaveManager.Load();
            int curGroupIndex = PlayerPrefs.GetInt("CurrentGroupIndex");

            //save group
            SerializableGroupClass sc = new SerializableGroupClass();
            sc = so.cardGroups[curGroupIndex];

            //remove group
            so.cardGroups.RemoveAt(curGroupIndex);

            sc.GroupName = groupNameText;
            so.cardGroups.Add(sc);

            SaveManager.Save(so);
            Debug.Log("Added " + groupNameText + " to card groups");

            ClearInputField();
            UpdateDispList();
            ClosePanel();
        }
    }

    public Dropdown dropDownGroup;

    public void CreateGroupFuncDropDown() {
        string groupNameText = groupNameField.GetComponent<InputField>().text;

        if (groupNameText == "" || groupNameText == null)
        {
            errorObj.SetActive(true);
        }
        else
        {
            errorObj.SetActive(false);

            so = SaveManager.Load();

            SerializableGroupClass sc = new SerializableGroupClass();
            sc.GroupName = groupNameText;
            sc.cards = new List<SerialisableCardClass>();

            sc.dateCreated = System.DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");

            so.cardGroups.Add(sc);

            SaveManager.Save(so);
            Debug.Log("Added " + groupNameText + " to card groups");

            ClearInputField();
            updateDropDown();
            ClosePanel();
        }
    }



    private void updateDropDown() {
        GroupDownDownScript GDDS = dropDownGroup.GetComponent<GroupDownDownScript>();
        GDDS.UpdateList();
        dropDownGroup.GetComponent<Dropdown>().value = dropDownGroup.GetComponent<Dropdown>().options.Count;
    }






    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void ClearInputField()
    {
        groupNameField.GetComponent<InputField>().text = "";
    }

    public void UpdateDispList()
    {
        list.GetComponent<ButtonListControl>().UpdateList();
    }
}
