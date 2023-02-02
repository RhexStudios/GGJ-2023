using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float patrolTime = 3f;
    public bool facingRight = false;
    public Rigidbody2D rb;
    public GameObject Enemie;

    void Start()
    {
        Enemie = GameObject.FindWithTag("Enemie");
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        
    }

    void Update()
    {
        Walk();
    }

    public void Walk()
    {

        if (!facingRight)
        {
            Vector3 move = new Vector3(-1 * speed, rb.velocity.y, 0f);
            rb.velocity = move;

            patrolTime -= Time.deltaTime;
            if (patrolTime <= 0)
                facingRight = true;
        }
        if (facingRight)
        {
            Vector3 move = new Vector3(1 * speed, rb.velocity.y, 0f);
            rb.velocity = move;

            patrolTime -= Time.deltaTime;
            if (patrolTime <= 0)
                facingRight = false;
        }
    }

}
