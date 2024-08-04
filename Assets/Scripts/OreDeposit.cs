using UnityEngine;
using System;

public class OreDeposit : MonoBehaviour
{
    public int oreAmount;
    public int workersCount;
    public string oreName;
    public OreDeposit selectedOreDeposit;

    private OreDepositUIManager uiManager;

    // Events to notify changes
    public event Action OnOreAmountChanged;
    public event Action OnWorkersCountChanged;

    void Start()
    {
        uiManager = FindFirstObjectByType<OreDepositUIManager>();
    }

    void OnMouseDown()
    {
        // Trigger the UI to show information about this ore deposit
        uiManager.ShowOreDepositInfo(this);
    }

    public void IncreaseWorkers()
    {
        workersCount++;
        OnWorkersCountChanged?.Invoke();
    }

    public void DecreaseWorkers()
    {
        if (workersCount > 0)
        {
            workersCount--;
            OnWorkersCountChanged?.Invoke();
        }
    }

    // Method to change ore amount
    public void ChangeOreAmount(int amount)
    {
        oreAmount += amount;
        Debug.Log("Ore amount changed: " + oreAmount);
        OnOreAmountChanged?.Invoke();
    }
}
