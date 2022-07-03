using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float jumpSpeed = 3f;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheckL;
    [SerializeField] private Transform groundCheckR;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    [SerializeField] private float fallMultiplier = 2f;
    private float jumpBufferTime = 0.05f;
    private float jumpBufferCounter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        // Left/Right Movement
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
       
        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime; 
        }

        // Checks if player is grounded 
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
        || Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))
        || Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Coyote Time & Jump Buffering

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKey("w") || Input.GetKey("space") || Input.GetKey("up"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //the jump itself
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        //check if tag is enemy
        if (col.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("Hit");
            
            //change player color to red
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //return player color to white after .3 seconds
            Invoke("returnToWhite", 0.1f);
            
            Debug.Log("hit obstacle");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    void returnToWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
