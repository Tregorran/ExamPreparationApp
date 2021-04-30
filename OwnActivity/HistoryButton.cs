using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryButton : MonoBehaviour
{
    private int index;
    public GameObject dateText;
    private SerializableObject so;

    public GameObject historyContentPanel;
    public GameObject historyDateField;
    public GameObject historyInputField;

    public void setIndex(int newIndex) {
        index = newIndex;
        loadData();
    }


    private void loadData() {
        so = SaveManager.Load();

        dateText.GetComponent<Text>().text = so.dayActivityWritten[index];
    }

    public void displayContentButton() {
        historyContentPanel.SetActive(true);
        historyDateField.GetComponent<Text>().text = so.dayActivityWritten[index];
        historyInputField.GetComponent<InputField>().text = so.ownActivityContent[index];
    }
}