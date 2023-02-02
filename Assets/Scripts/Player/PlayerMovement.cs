using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Velocidades
    public float speed = 5.0f;
    public float jumpSpeed = 7.0f;
    public float dashSpeed = 25.0f;
    //Intervalos
    public float dashCooldown = 1.5f;
    public float dashDuration = .3f;
    //booleanos
    public bool canDash;
    public bool isDashing;
    public bool isGround;
    public bool facingRight;
    //Objetos
    public GameObject player;
    public Rigidbody2D rb;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        canDash = true;
    }

    
    void Update()
    {
        Walk();
        Jump();
        Dash();
        Dashing();
        RefreshDash();
    }

    public void Walk() 
    {
        float movement = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3 (movement * speed, rb.velocity.y, 0f);
        rb.velocity = move;

        if(movement > 0)
            facingRight = true;
        if (movement < 0)
            facingRight = false;
    }

    public void Jump() 
    {
        if(Input.GetKeyDown("up") && isGround) 
        {
        float jump = Input.GetAxis("Vertical");
        rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isGround = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D col) 
    {
        if ( col.gameObject.tag == "ground" )
            isGround = true;
    }

    public void Dash() 
    {
        if (Input.GetKeyDown("z") && canDash && !isDashing) 
        {
            isDashing = true;
            canDash = false;
        }

    }

    public void Dashing() 
    {
        if(isDashing)
        {
            dashDuration -= Time.deltaTime;
            if (!facingRight) 
            {   
                rb.AddRelativeForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);   
            }
            else if (facingRight)
            {
                rb.AddRelativeForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
            }           
        }
        if (dashDuration <= 0) 
        {
            isDashing = false;
        }
    }

    public void RefreshDash() 
    {
        if(!canDash)
            {
                dashCooldown -= Time.deltaTime;
                if ( dashCooldown <= 0 )
                {
                    dashCooldown = 1.5f;
                    dashDuration = .3f;
                    canDash = true;
                }
            }
    }
}