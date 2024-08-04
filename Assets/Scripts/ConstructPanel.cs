using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPanel : MonoBehaviour
{
    public GameObject constructPanel;
    public bool isConstructPanelOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        constructPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideConstructPanel()
    {
        constructPanel.SetActive(false);
        isConstructPanelOpen = false;
    }

    public void ShowConstructPanel()
    {
        constructPanel.SetActive(true);
        isConstructPanelOpen = true;
    }
}
