using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupDownDownScript : MonoBehaviour
{
    private SerializableObject so;
    private Dropdown dropdown;
    private List<string> items;

    public GameObject createGroupPanel;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        UpdateList();
        selectCurGroup();
    }

    private void DropdownItemSelected(Dropdown dropdown) {
        int index = dropdown.value;

        if (index == 0) {
            createGroupPanel.SetActive(true);
            dropdown.value = 0;
        }
    }

    public void UpdateList() {
        dropdown.options.Clear();
        items = new List<string>();
        items.Add("Add New Group +");

        so = SaveManager.Load();//loads the cards

        if (!(so.cardGroups.Count == 0))
        {
            foreach (SerializableGroupClass eachGroup in so.cardGroups)
            {
                items.Add(eachGroup.GroupName);
            }
        }

        foreach (string item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData()
            {
                text = item
            });

            dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        }

        selectCurGroup();
    }

    public void selectCurGroup() {
        int curGroupIndex = PlayerPrefs.GetInt("CurrentGroupIndex");
        string curGroupString = so.cardGroups[curGroupIndex].GroupName;

        for (int i = 0; i < dropdown.options.Count; i++) {
            if (dropdown.options[i].text == curGroupString) {
                dropdown.value = i;
                break;
            }
        }
    }
}
