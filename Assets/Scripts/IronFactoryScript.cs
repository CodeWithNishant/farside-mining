using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronFactoryScript : MonoBehaviour
{
    public int ironOreAmount;
    public int ironAmount;
    public int steelAmount;

    private bool isSmelting = false;
    private IronFactoryUIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindFirstObjectByType<IronFactoryUIManager>();
        ironOreAmount = 0;
        ironAmount = 0;
        steelAmount = 0;
    }

    // Method to add iron ore to the factory
    public void AddIronOre(int amount)
    {
        ironOreAmount += amount;
        if (!isSmelting)
        {
            StartCoroutine(SmeltingProcess());
        }
    }

    public void AddIron(int amount)
    {
        ironAmount += amount;
    }

    // Method to remove iron from the factory
    public int RemoveIron(int amount)
    {
        int amountToRemove = Mathf.Min(amount, ironAmount);
        ironAmount -= amountToRemove;
        return amountToRemove;
    }

    public void RemoveSteel(int amount)
    {
        steelAmount -= amount;
    }

    // Smelting process coroutine
    IEnumerator SmeltingProcess()
    {
        isSmelting = true;
        while (ironOreAmount >= 2)
        {
            yield return new WaitForSeconds(5.0f); // Smelting takes 5 seconds
            ironOreAmount -= 2;
            ironAmount += 1;
            Debug.Log("Smelted 2 iron ore into 1 iron. Iron Amount: " + ironAmount);
        }
        isSmelting = false;
    }

    void OnMouseDown()
    {
        uiManager.ShowIronFactoryInfo(this);
    }
}
