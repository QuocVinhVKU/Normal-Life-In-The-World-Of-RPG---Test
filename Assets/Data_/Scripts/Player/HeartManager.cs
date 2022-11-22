using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    /*    public Image[] hearts;
        public Sprite fullHeart;
        public Sprite halfFullHeart;
        public Sprite emptyHeart;
        public FloatValue heartContainers;
        public FloatValue playerCurrentHealth;

        void Start()
        {
            InitHearts();
        }

        public void InitHearts()
        {
            for(int i = 0; i < heartContainers.initialValue; i++)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
        }
        public void UpdateHearts()
        {
            float tempHealth = playerCurrentHealth.RuntimeValue / 2;
            for(int i = 0; i < heartContainers.initialValue; i++)
            {
                if (i <= tempHealth - 1)
                {
                    //full health
                    hearts[i].sprite = fullHeart;
                }
                else if(i >= tempHealth)
                {
                    //emty health
                    hearts[i].sprite = emptyHeart;
                }
                else
                {
                    //haft full health
                    hearts[i].sprite = halfFullHeart;
                }
            }
        }*/
    public Slider healthSlider;
    public FloatValue playerMaxHealth;
    public FloatValue playerCurrentHealth;
    private void Start()
    {
        healthSlider.maxValue = playerMaxHealth.initialValue;
        healthSlider.value = playerMaxHealth.initialValue;
        playerCurrentHealth.RuntimeValue = playerMaxHealth.initialValue;
    }
    private void Update()
    {
        updateHealth();
    }
    public void updateHealth()
    {
        healthSlider.value = playerCurrentHealth.RuntimeValue;
    }
}
