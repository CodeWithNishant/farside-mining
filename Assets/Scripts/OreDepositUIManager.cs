using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OreDepositUIManager : MonoBehaviour
{
    public GameObject orePanel;
    public TextMeshProUGUI oreTitleText;
    public TextMeshProUGUI oreAmountText;
    public TextMeshProUGUI workersCountText;
    public Button increaseWorkersButton;
    public Button decreaseWorkersButton;
    private OreDeposit currentOreDeposit;
    public WorkerManager workerManager;

    void Start()
    {
        orePanel.SetActive(false); // Hide the panel initially
    }

    public void ShowOreDepositInfo(OreDeposit oreDeposit)
    {
        if (currentOreDeposit != null)
        {
            UnsubscribeFromEvents(currentOreDeposit);
        }

        currentOreDeposit = oreDeposit;

        // Subscribe to events
        SubscribeToEvents(currentOreDeposit);

        // Update the panel with ore deposit information
        oreAmountText.text = "Ore Amount: " + oreDeposit.oreAmount.ToString();
        workersCountText.text = "Workers: " + oreDeposit.workersCount.ToString();

        // Show the panel
        orePanel.SetActive(true);
    }

    private void SubscribeToEvents(OreDeposit oreDeposit)
    {
        oreDeposit.OnOreAmountChanged += UpdateOreAmountText;
        oreDeposit.OnWorkersCountChanged += UpdateWorkersCountText;
    }

    private void UnsubscribeFromEvents(OreDeposit oreDeposit)
    {
        oreDeposit.OnOreAmountChanged -= UpdateOreAmountText;
        oreDeposit.OnWorkersCountChanged -= UpdateWorkersCountText;
    }

    private void UpdateOreAmountText()
    {
        if (currentOreDeposit != null)
        {
            oreAmountText.text = "Ore Amount: " + currentOreDeposit.oreAmount.ToString();
        }
    }

    private void UpdateWorkersCountText()
    {
        if (currentOreDeposit != null)
        {
            workersCountText.text = "Workers: " + currentOreDeposit.workersCount.ToString();
        }
    }

    public void IncreaseWorkers()
    {
        if (currentOreDeposit != null)
        {
            currentOreDeposit.IncreaseWorkers();
        }
    }

    public void DecreaseWorkers()
    {
        if (currentOreDeposit != null)
        {
            currentOreDeposit.DecreaseWorkers();
        }
    }

    public void closeOreDepositInfoPanel()
    {
        orePanel.SetActive(false);

        if (currentOreDeposit != null)
        {
            UnsubscribeFromEvents(currentOreDeposit);
            currentOreDeposit = null;
        }
    }

    public void mineCurrentOpenMine()
    {
        workerManager.AssignMiningWork(currentOreDeposit);
    }

    public void removeRobotFromOpenMine()
    {
        workerManager.RobotFinishedWork(currentOreDeposit);
    }
}