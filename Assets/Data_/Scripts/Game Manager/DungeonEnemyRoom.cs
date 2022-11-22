using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door_Dungeon_1[] doors;

    public void Start()
    {
        OpenDoor();
    }
    public int EnemiesActive()
    {
        int activeEnemies = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    public void CheckEnemies()
    {
        if (EnemiesActive() == 1)
        {
            OpenDoor();
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Active all enemies and pot

            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoor();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactive all enemies and pot
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
        }
    }
    public void CloseDoor()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }
    public void OpenDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }
}
