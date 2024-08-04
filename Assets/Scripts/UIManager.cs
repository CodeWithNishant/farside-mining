using UnityEngine;
using TMPro; // Add this line

public class ResourceUI : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI cpuText;
    public TextMeshProUGUI steelText;

    void Update()
    {
        cashText.text = "Cash: " + ResourceManager.Instance.GetResourceAmount("Cash");
        ironText.text = "Iron: " + ResourceManager.Instance.GetProductAmount("Iron");
        copperText.text = "Copper: " + ResourceManager.Instance.GetProductAmount("Copper");
        cpuText.text = "CPU: " + ResourceManager.Instance.GetProductAmount("CPU");
        steelText.text = "Steel: " + ResourceManager.Instance.GetProductAmount("Steel");
    }
}
