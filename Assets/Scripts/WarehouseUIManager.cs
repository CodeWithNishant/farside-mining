using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarehouseUIManager : MonoBehaviour
{
    public GameObject warehousePanel;
    public TextMeshProUGUI ironOreAmountText;
    public TextMeshProUGUI copperOreAmountText;
    public TextMeshProUGUI ironAmountText;
    public TextMeshProUGUI copperAmountText;
    public TextMeshProUGUI steelAmountText;
    public TextMeshProUGUI cpuAmountText;

    private Warehouse currentWarehouse;

    void Start()
    {
        warehousePanel.SetActive(false); // Hide the panel initially
    }

    public void ShowWarehouseInfo(Warehouse warehouse)
    {
        currentWarehouse = warehouse;

        // Subscribe to events
        warehouse.OnIronOreAmountChanged += UpdateIronOreAmount;
        warehouse.OnCopperOreAmountChanged += UpdateCopperOreAmount;
        warehouse.OnIronAmountChanged += UpdateIronAmount;
        warehouse.OnCopperAmountChanged += UpdateCopperAmount;
        warehouse.OnSteelAmountChanged += UpdateSteelAmount;
        warehouse.OnCpuAmountChanged += UpdateCpuAmount;

        // Update the panel with warehouse information
        UpdateIronOreAmount();
        UpdateCopperOreAmount();
        UpdateIronAmount();
        UpdateCopperAmount();
        UpdateSteelAmount();
        UpdateCpuAmount();

        // Show the panel
        warehousePanel.SetActive(true);
    }

    public void CloseWarehouseInfoPanel()
    {
        // Unsubscribe from events
        if (currentWarehouse != null)
        {
            currentWarehouse.OnIronOreAmountChanged -= UpdateIronOreAmount;
            currentWarehouse.OnCopperOreAmountChanged -= UpdateCopperOreAmount;
            currentWarehouse.OnIronAmountChanged -= UpdateIronAmount;
            currentWarehouse.OnCopperAmountChanged -= UpdateCopperAmount;
            currentWarehouse.OnSteelAmountChanged -= UpdateSteelAmount;
            currentWarehouse.OnCpuAmountChanged -= UpdateCpuAmount;
        }

        warehousePanel.SetActive(false);
    }

    void UpdateIronOreAmount()
    {
        ironOreAmountText.text = "Iron Ore : " + currentWarehouse.ironOreAmount.ToString();
    }

    void UpdateCopperOreAmount()
    {
        copperOreAmountText.text = "Copper Ore : " + currentWarehouse.copperOreAmount.ToString();
    }

    void UpdateIronAmount()
    {
        ironAmountText.text = "Iron : " + currentWarehouse.ironAmount.ToString();
    }

    void UpdateCopperAmount()
    {
        copperAmountText.text = "Copper : " + currentWarehouse.copperAmount.ToString();
    }

    void UpdateSteelAmount()
    {
        steelAmountText.text = "Steel : " + currentWarehouse.steelAmount.ToString();
    }

    void UpdateCpuAmount()
    {
        cpuAmountText.text = "CPU : " + currentWarehouse.cpuAmount.ToString();
    }
}
