using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableBuildings : MonoBehaviour
{
    public GameObject thePanel;
    public TextMeshProUGUI hoverInfo;
    public string hoverInfoText;
    void OnMouseDown()
    {
        thePanel.SetActive(true);
    }

    void OnMouseOver()
    {
        hoverInfo.gameObject.SetActive(true);
        hoverInfo.text = hoverInfoText;
    }

    void OnMouseExit()
    {
        hoverInfo.gameObject.SetActive(false);
    }

    public void closeThePanel()
    {
        thePanel.SetActive(false);
    }
}

