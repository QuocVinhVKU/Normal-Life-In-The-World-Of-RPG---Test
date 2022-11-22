using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject thisLoot;
    public int lootChange;
}

[CreateAssetMenu]
public class LootTables : ScriptableObject
{
    public Loot[] loots;
    public GameObject LootPowerup()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for(int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChange;
            if(currentProb <= cumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }
}
