using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    
    public GameObject dialogBox;
    public Text dialogText;
    public string dialogTitle;
    public bool needface = false;
    public GameObject Face;
    public Image faceImage;
    public Sprite faceChange;
    // Start is called before the first frame update
    public void Awake()
    {
       dialogTitle = dialogTitle.Replace("<br>", "\n");
       
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("attack") || Input.GetKeyDown(KeyCode.Return)) && playerInRange)
        {
            if (Face == null || faceImage == null || faceChange == null)
            {
                dialogBox.SetActive(true);
                dialogText.text = dialogTitle;
            }
            else if (dialogBox.activeInHierarchy)
            {
                Face.SetActive(false);
                dialogBox.SetActive(false);
            }
            else
            {
                if(needface == true) 
                {
                    faceImage.sprite = faceChange;
                    Face.SetActive(true);
                    
                    dialogBox.SetActive(true);
                    dialogText.text = dialogTitle;
                }
                else
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialogTitle;
                }
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
            if (Face != null)
            {
                Face.SetActive(false);
            }
            
        }
    }
}