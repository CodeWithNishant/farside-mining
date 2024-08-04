using UnityEngine;
public class Resource
{
    public string Name { get; private set; }
    public int Amount { get; private set; }

    public Resource(string name, int initialAmount)
    {
        Name = name;
        Amount = initialAmount;
    }

    public void Increment(int amount)
    {
        Amount += amount;
    }

    public void Decrement(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
        }
        else
        {
            Debug.LogWarning($"Not enough {Name} to decrement by {amount}");
        }
    }
}

public class Product : Resource
{
    public Product(string name, int initialAmount) : base(name, initialAmount) { }
}
