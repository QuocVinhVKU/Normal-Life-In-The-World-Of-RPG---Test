using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Powerup
{
    public Inventory playerInventory;
    public float manaValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory.currentMana += manaValue;
            powerupSignal.Raise();
            Destroy(this.gameObject);

        }
    }

}
