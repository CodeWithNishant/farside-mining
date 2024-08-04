using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IronFactoryUIManager : MonoBehaviour
{
    // UI components
    public GameObject factoryPanel;
    public TextMeshProUGUI oreAmountText;
    public TextMeshProUGUI ironAmountText;
    public TextMeshProUGUI steelAmountText;
    public TextMeshProUGUI workersBringingCountText;
    public Button increaseWorkersBringingButton;
    public Button decreaseWorkersBringingButton;
    public TextMeshProUGUI workersTakingCountText;
    public Button increaseWorkersTakingButton;
    public Button decreaseWorkersTakingButton;

    private IronFactoryScript currentFactory;

    // Other variables;
    private int dropdownVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        factoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropdownValueChanged(int val)
    {
        dropdownVal = val;
    }

    public void ShowIronFactoryInfo(IronFactoryScript thisIronFactory)
    {
        currentFactory = thisIronFactory;
        factoryPanel.SetActive(true);
    }

    public void HideIronFactoryInfo()
    {
        factoryPanel.SetActive(false);
    }
}
