using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door_Dungeon_1 : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicCollider;

    //private void Start()
    //{
    //    doorSprite = GetComponent<SpriteRenderer>();
    //}
    private void Update()
    {
        InteractDoor();
    }
    public virtual void InteractDoor()
    {
        if (Input.GetButtonDown("attack"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                // does player have a key?
                if (playerInventory.numberOfKey > 0)
                {
                    // remove a player's key
                    playerInventory.numberOfKey--;
                    // if Player have key, call open method
                    Open();
                }

            }
        }
    }
    public void Open()
    {
        // turn off door's sprite renderer
        doorSprite.enabled = false;
        // set open true
        open = true;
        // turn off the door box collider
        physicCollider.enabled = false;

    }
    public void Close()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = true;
        //set open to true  
        open = false;
        //turn off the door's box collider
        physicCollider.enabled = true;
    }
}
