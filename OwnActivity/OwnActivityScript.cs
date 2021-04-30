using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnActivityScript : MonoBehaviour
{
    public GameObject TimeDateText;
    public GameObject ActivityInputField;
    private SerializableObject so;
    void Start()
    {
        so = SaveManager.Load();
        buttons = new List<GameObject>();
        string day;
        string curActivityContent;

        //retrieve text from storage if exists
        if (so.dayActivityWritten.Count > 0)
        {
            day = so.dayActivityWritten[so.dayActivityWritten.Count - 1];
            curActivityContent = so.ownActivityContent[so.ownActivityContent.Count - 1];
        }
        else {
            //create new entry for current day
            so.ownActivityContent = new List<string>();
            so.dayActivityWritten = new List<string>();
            string newDate = System.DateTime.Now.ToString("dd/MM/yyyy");
            so.dayActivityWritten.Add(newDate);
            so.ownActivityContent.Add("");
            day = newDate;
            curActivityContent = "";
            SaveManager.Save(so);
        }

        //check if the text date is the same as the current date
        if (day == System.DateTime.Now.ToString("dd/MM/yyyy"))
        {
            ActivityInputField.GetComponent<InputField>().text = curActivityContent;
        }
        else {
            //create new entry
            so.dayActivityWritten.Add(System.DateTime.Now.ToString("dd/MM/yyyy"));
            so.ownActivityContent.Add("");

            SaveManager.Save(so);
            so = SaveManager.Load();

            //only keep entries from last 7 days
            while (so.ownActivityContent.Count > 7)
            {
                so.ownActivityContent.RemoveAt(0);
                so.dayActivityWritten.RemoveAt(0);
            }
        }
    }

    public GameObject historyPanel;
    public GameObject buttonTemplate;
    private List<GameObject> buttons;
    //removes buttons from before updating history list
    private void RemoveButtonsFromHistory()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

    //instantiates a list of history buttons
    public void historyFunc()
    {
        RemoveButtonsFromHistory();
        historyPanel.SetActive(true);

        so = SaveManager.Load();

        //display the previous history dates//Skip first one
        for (int i = so.dayActivityWritten.Count - 2; i >= 0; i--) {

            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<HistoryButton>().setIndex(i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
        }
    }

    public GameObject savingText;

    private float totalTime = 1;
    private float time = 1;
    public GameObject savingImage;
    public GameObject savedImage;
    // Update is called once per frame
    void Update()
    {
        so = SaveManager.Load();

        //get current times
        string day = System.DateTime.Now.DayOfWeek.ToString();
        string date = System.DateTime.Now.ToString("dd/MM/yyyy");
        string hour = System.DateTime.Now.Hour.ToString();
        string minute = System.DateTime.Now.Minute.ToString("00");
        string second = System.DateTime.Now.Second.ToString();

        //updating time and date UI
        TimeDateText.GetComponent<Text>().text = (day + "\n" + date + "\n" + hour + ":" + minute);

        //performs the saving of the inputs from the user in the inputfield

        string saveText = so.ownActivityContent[so.ownActivityContent.Count - 1];//get text saved
        string textInput = ActivityInputField.GetComponent<InputField>().text;//get user input

        //Check if the saved text is the same as in inputfield
        if (!(saveText == textInput)) {

            time = totalTime;
            savingText.GetComponent<Text>().text = "Saving";
            savingImage.SetActive(true);
            savedImage.SetActive(false);

            //if not same update saved text
            if (so.ownActivityContent.Count > 0)
            {
                so.ownActivityContent[so.ownActivityContent.Count - 1] = ActivityInputField.GetComponent<InputField>().text;
            }

            if (so.dayActivityWritten.Count > 0)
            {
                so.dayActivityWritten[so.dayActivityWritten.Count - 1] = date;
            }
            SaveManager.Save(so);
        }

        //A delay for saving icon
        if (saveText == textInput) {
            time -= Time.deltaTime;

            if (time < 0) {
                savingText.GetComponent<Text>().text = "Saved";
                savingImage.SetActive(false);
                savedImage.SetActive(true);
            }
        }
    }
}
