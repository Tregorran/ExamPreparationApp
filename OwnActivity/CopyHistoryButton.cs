using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyHistoryButton : MonoBehaviour
{
    public GameObject CurInputField;
    public GameObject historyInputField;
    public GameObject historyContentPanel;
    public GameObject historyListPanel;

    public void CopyHistory() {
        historyContentPanel.SetActive(false);
        historyListPanel.SetActive(false);
        CurInputField.GetComponent<InputField>().text += "\n" + historyInputField.GetComponent<InputField>().text;
    }
}
