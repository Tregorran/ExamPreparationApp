using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectColour : MonoBehaviour
{
    public string ColourSelectedHex = "#FFFFFF";
    public GameObject ColourPanel;

    public void onClickSelectColour() {
        ColourPanel.SetActive(true);
    }

    public void selectedColour() {
        GameObject selectedButton;
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        selectedButton = GameObject.Find(buttonName);

        ColourSelectedHex = ColorUtility.ToHtmlStringRGB(selectedButton.GetComponent<Image>().color);
        ColourSelectedHex = "#" + ColourSelectedHex;
        Color myColor;
        ColorUtility.TryParseHtmlString(ColourSelectedHex, out myColor);
        gameObject.GetComponent<Image>().color = myColor;

        ColourPanel.SetActive(false);
    }

    public void setColour(string hexColour) {
        ColourSelectedHex = hexColour;
        Color myColor;
        ColorUtility.TryParseHtmlString(ColourSelectedHex, out myColor);
        gameObject.GetComponent<Image>().color = myColor;
    }
}
