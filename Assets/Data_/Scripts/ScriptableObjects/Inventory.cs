using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKey;
    public int coins;
    public float maxMana = 100;
    public float currentMana;

    public void OnEnable()
    {
        currentMana = maxMana;
    }
    public void ReduceMana(float manaCost)
    {
        currentMana -= manaCost;
    }
    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKey++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
