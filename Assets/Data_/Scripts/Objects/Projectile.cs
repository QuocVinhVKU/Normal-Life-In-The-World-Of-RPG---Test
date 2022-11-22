using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Moverment Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Life Time")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Launch(Vector2 initialVel)
    {
        myRigidbody2D.velocity = initialVel * moveSpeed;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("room"))
        {
            Destroy(this.gameObject);
        }
    }
}
