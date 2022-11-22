using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Slider manaSlider;
    public Inventory playerInventory;

    private void Start()
    {
        manaSlider.maxValue = playerInventory.maxMana;
        manaSlider.value = playerInventory.maxMana;
        playerInventory.currentMana = playerInventory.maxMana;
    }
    private void Update()
    {
        manaSlider.value = playerInventory.currentMana;
    }
    public void AddMana()
    {
        //manaSlider.value += 10;
        //playerInventory.currentMana += 10;
        manaSlider.value = playerInventory.currentMana;
        if (manaSlider.value == manaSlider.maxValue)
        {
            playerInventory.currentMana = playerInventory.maxMana;
        }
    }
    public void DecreaseMana()
    {
        //manaSlider.value -= 10;
        //playerInventory.currentMana -= 10;
        manaSlider.value = playerInventory.currentMana;
        if(manaSlider.value < 0)
        {
            manaSlider.value = 0;
            playerInventory.currentMana = 0;
        }
    }
}
