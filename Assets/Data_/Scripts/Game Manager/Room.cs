using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCamera;
    public Enemy[] enemies;
    public pot[] pots;
    public tree[] trees;
    public GameObject[] gameOb;

    private void Awake()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], false);
        }
        for (int i = 0; i < gameOb.Length; i++)
        {
            ChangeActivationB(gameOb[i], false);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //Active all enemies and pot

            for(int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            for (int i = 0; i < trees.Length; i++)
            {
                ChangeActivation(trees[i], true);
            }
            for (int i = 0; i < gameOb.Length; i++)
            {
                ChangeActivationB(gameOb[i], true);
            }
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
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
            for (int i = 0; i < trees.Length; i++)
            {
                ChangeActivation(trees[i], false);
            }
            for (int i = 0; i < gameOb.Length; i++)
            {
                ChangeActivationB(gameOb[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
    public void ChangeActivationB(GameObject gameObj, bool activation)
    {
        gameObj.gameObject.SetActive(activation);
    }
}
