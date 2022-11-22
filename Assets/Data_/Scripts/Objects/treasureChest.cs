using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signal and Dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

    public PlayerState currentState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                //open chest
                OpenChest();
            }
            else
            {
                //chest already open
                ChestAlreadyOpen();
            }
        }
    }
    public void OpenChest()
    {
        // dialog window on
        dialogBox.SetActive(true);
        // dialog text = content text
        dialogText.text = contents.itemDecription;

        // thêm contents vào inventory
        playerInventory.currentItem = contents;
        // sinh ra signal đến player để gọi animate
        raiseItem.Raise();
        
        // đưa ra manh mối về item
        contextOff.Raise();
        // add item
        playerInventory.AddItem(contents);
        // set rương open
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.RuntimeValue = isOpen;
        
        currentState = PlayerState.stagger;
    }
    public void ChestAlreadyOpen()
    {
        playerInRange = false;
        // dialog off
        dialogBox.SetActive(false);

        // gửi signal đến player  để stop animating
        contextOff.Raise();
        raiseItem.Raise();
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger &&!isOpen)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
