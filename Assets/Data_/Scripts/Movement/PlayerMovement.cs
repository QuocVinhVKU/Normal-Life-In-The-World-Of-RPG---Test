using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public GameObject[] Bow;
    public GameObject bow;

    public float timeAttack;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPos;
    public Inventory playerInventory;
    public SpriteRenderer receiveItemSprite;

    public Signal reduceMana;
    public Signal playerHit;
    public GameObject playerProjectile;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = startingPos.initialValue;
    }

    // Update is called once per frame
    private void Update()
    {
        bow.transform.position = this.transform.position;
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.interact)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.interact)
        {
            StartCoroutine(secondWeaponAttackCo());
        }

    }
    void FixedUpdate()
    {
        //is the player in an interaction
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        //else
        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }


    }
    public void PlayerFreeze()
    {
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        if (Input.GetButtonDown("attack"))
        {
            myRigidbody.constraints = RigidbodyConstraints2D.None;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(timeAttack);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
       
    }
    private IEnumerator secondWeaponAttackCo()
    {
        bowAnim();
        currentState = PlayerState.attack;

        yield return null;
        MakeArrow();
        yield return new WaitForSeconds(timeAttack);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        for(int i = 0; i< Bow.Length; i++)
        {
            Bow[i].SetActive(false);
        }
        

    }
    private void bowAnim()
    {
        if (animator.GetFloat("moveX") == 0 && animator.GetFloat("moveY") > 0)
        {
            Bow[0].SetActive(true);
        }
        else if (animator.GetFloat("moveX") == 0 && animator.GetFloat("moveY") < 0)
        {
            Bow[1].SetActive(true);
        }
        else if (animator.GetFloat("moveX") < 0 && animator.GetFloat("moveY") == 0)
        {
            Bow[2].SetActive(true);
        }
        else if (animator.GetFloat("moveX") > 0 && animator.GetFloat("moveY") == 0)
        {
            Bow[3].SetActive(true);
        }
        else if (animator.GetFloat("moveX") > 0 && animator.GetFloat("moveY") > 0)
        {
            Bow[4].SetActive(true);
        }
        else if (animator.GetFloat("moveX") < 0 && animator.GetFloat("moveY") > 0)
        {
            Bow[5].SetActive(true);
        }
        else if (animator.GetFloat("moveX") > 0 && animator.GetFloat("moveY") < 0)
        {
            Bow[6].SetActive(true);
        }
        else if (animator.GetFloat("moveX") < 0 && animator.GetFloat("moveY") < 0)
        {
            Bow[7].SetActive(true);
        }
    }
    private void MakeArrow()
    {
        if (playerInventory.currentMana > 0)
        {   
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(playerProjectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMana(arrow.manaCost);
            reduceMana.Raise();
        }
    }
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    public void RaiseItem()
    {
        if (currentState != PlayerState.interact)
        {
            animator.SetBool("receive item", true);
            currentState = PlayerState.interact;
            receiveItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            animator.SetBool("receive item", false);
            currentState = PlayerState.idle;
            receiveItemSprite.sprite = null;
        }
    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}