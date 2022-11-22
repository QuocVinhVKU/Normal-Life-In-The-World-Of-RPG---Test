using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenLeafDoor : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialogTitle;
    public bool needface = false;
    public GameObject Face;
    public Image faceImage;
    public Sprite faceChange;
    [Header("event")]
    public GameObject[] gameOb;
    public bool eventDone = false;

    public void Awake()
    {
        dialogTitle = dialogTitle.Replace("<br>", "\n");

    }

    void Update()
    {
        if (eventDone == true)
        {
            openDialog();
            eventL();
        }

    }
    public void doneEvent()
    {
        eventDone = true;
    }
    public void openDialog()
    {

        if (needface == true)
        {
            faceImage.sprite = faceChange;
            Face.SetActive(true);

            dialogBox.SetActive(true);
            dialogText.text = dialogTitle;
        }
        else
        {
            Face.SetActive(false);
            dialogBox.SetActive(true);
            dialogText.text = dialogTitle;
        }
        if (playerState != PlayerState.interact)
        {
            playerState = PlayerState.interact;
        }
        else
        {
            playerState = PlayerState.idle;
        }
        

    }
    public void eventL()
    {
        if (Input.GetButtonDown("attack"))
        {
            Face.SetActive(false);
            dialogBox.SetActive(false);

            for (int i = 0; i < gameOb.Length; i++)
            {
                gameOb[i].SetActive(false);
            }
        }

    }




}
