using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float manaCost;

    public float lifeTime;
    private float lifeTimeCouter;


    void Start()
    {
        lifeTimeCouter = lifeTime;
    }
    private void Update()
    {
        lifeTimeCouter -= Time.deltaTime;
        if (lifeTimeCouter <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.GetComponent<Collider2D>().isTrigger)
        {
            Destroy(this.gameObject);
        }
        
    }
}
