using System;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    public int ironOreAmount;
    public int copperOreAmount;
    public int ironAmount;
    public int copperAmount;
    public int steelAmount;
    public int cpuAmount;

    private WarehouseUIManager uiManager;

    // Resource manager
    public ResourceManager resourceManager;

    // Events to notify changes
    public event Action OnIronOreAmountChanged;
    public event Action OnCopperOreAmountChanged;
    public event Action OnIronAmountChanged;
    public event Action OnCopperAmountChanged;
    public event Action OnSteelAmountChanged;
    public event Action OnCpuAmountChanged;

    void Start()
    {
        uiManager = FindFirstObjectByType<WarehouseUIManager>();
    }

    void OnMouseDown()
    {
        // Trigger the UI to show information about this warehouse
        uiManager.ShowWarehouseInfo(this);
    }

    public void ChangeIronOreAmount(int amount)
    {
        ironOreAmount += amount;
        resourceManager.IncrementResource("IronOre", amount);
        OnIronOreAmountChanged?.Invoke();
    }

    public void ChangeCopperOreAmount(int amount)
    {
        copperOreAmount += amount;
        resourceManager.IncrementResource("CopperOre", amount);
        OnCopperOreAmountChanged?.Invoke();
    }

    public void ChangeIronAmount(int amount)
    {
        ironAmount += amount;
        OnIronAmountChanged?.Invoke();
    }

    public void ChangeCopperAmount(int amount)
    {
        copperAmount += amount;
        OnCopperAmountChanged?.Invoke();
    }

    public void ChangeSteelAmount(int amount)
    {
        steelAmount += amount;
        OnSteelAmountChanged?.Invoke();
    }

    public void ChangeCpuAmount(int amount)
    {
        cpuAmount += amount;
        OnCpuAmountChanged?.Invoke();
    }
}
