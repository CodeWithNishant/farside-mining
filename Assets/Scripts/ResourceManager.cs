using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<string, Resource> resources;
    private Dictionary<string, Product> products;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            resources = new Dictionary<string, Resource>();
            products = new Dictionary<string, Product>();

            // Initialize main resources
            AddResource("Cash", 10000);
            AddResource("IronOre", 0);
            AddResource("CopperOre", 0);

            // Initialize products
            AddProduct("Iron", 100);
            AddProduct("Copper", 50);
            AddProduct("CPU", 50);
            AddProduct("Steel", 150);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddResource(string name, int initialAmount)
    {
        if (!resources.ContainsKey(name))
        {
            resources[name] = new Resource(name, initialAmount);
        }
    }

    public void AddProduct(string name, int initialAmount)
    {
        if (!products.ContainsKey(name))
        {
            products[name] = new Product(name, initialAmount);
        }
    }

    public void IncrementResource(string name, int amount)
    {
        if (resources.ContainsKey(name))
        {
            resources[name].Increment(amount);
        }
    }

    public void DecrementResource(string name, int amount)
    {
        if (resources.ContainsKey(name))
        {
            resources[name].Decrement(amount);
        }
    }

    public void IncrementProduct(string name, int amount)
    {
        if (products.ContainsKey(name))
        {
            products[name].Increment(amount);
        }
    }

    public void DecrementProduct(string name, int amount)
    {
        if (products.ContainsKey(name))
        {
            products[name].Decrement(amount);
        }
    }

    public int GetResourceAmount(string name)
    {
        return resources.ContainsKey(name) ? resources[name].Amount : -1;
    }

    public int GetProductAmount(string name)
    {
        return products.ContainsKey(name) ? products[name].Amount : -1;
    }
}
