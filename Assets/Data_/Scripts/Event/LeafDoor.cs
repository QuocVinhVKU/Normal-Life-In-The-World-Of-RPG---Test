using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafDoor : Door_Dungeon_1
{
    public PlayerMovement playerMovement;
    public Signal eventLeaf;

    public override void InteractDoor()
    {

        if (playerInRange && thisDoorType == DoorType.key)
        {

            // does player have a key?
            if (playerInventory.numberOfKey > 0)
            {
                playerMovement.PlayerFreeze();
                eventLeaf.Raise();
                if (Input.GetButtonDown("attack"))
                {
                    // remove a player's key
                    playerInventory.numberOfKey--;
                    // if Player have key, call open method
                    Open();
                    
                }
                
            }

        }
    }
}

