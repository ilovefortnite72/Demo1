using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;


    private Rigidbody2D rb;
    private float movespeed = 10;
    private float xAxis;
    [SerializeField]
    private float JumpForce = 10;

    [Header("Ground Check")]
    private bool isGrounded = false;
    public Transform player;
    public float checkDistance = 2f;
    public LayerMask whatIsGround;


    [Header("JetPack")]

    [SerializeField] private float JetPackFuel = 100;
    [SerializeField] private float JetPackFuelBurnRate = 10;
    [SerializeField] private float JetPackFuelRegenRate = 5;
    [SerializeField] private bool isJetPackActive = false;
    [SerializeField] private float JetPackForce = 10;

    public ParticleSystem jetPackParticles;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        InputControls();
        Move();
        IsGrounded();
        Jump();
        JetPack();

    }

    private void InputControls()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

    }

    private void Move()
    {
        rb.velocity = new Vector2(xAxis * movespeed, rb.velocity.y);
        //change animation direction based on movement
        animator.SetFloat("Speed", Mathf.Abs(xAxis));
        if (xAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (xAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
  
    }

    public void OnLand()
    {
        animator.SetBool("IsJumping", false);
    }


    //check if player is grounded
    public void IsGrounded()
    {
        
        isGrounded = Physics2D.OverlapCircle(player.position, checkDistance, whatIsGround);
        //if (isGrounded)
        //{
        //    Debug.Log("isGrounded");
        //}
        
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //add jump force, and change variables around for animation
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            animator.SetBool("IsJumping", true);
            isGrounded = false;
            
        }
        
    }

    public void JetPack()
    {
        //check if player is holding jump button and jetpack fuel is more than 0 and player is not grounded
        if (Input.GetButton("Jump") && JetPackFuel > 0 && !isGrounded)
        {
        
            isJetPackActive = true;
            JetPackFuel -= JetPackFuelBurnRate * Time.deltaTime;
            rb.AddForce(rb.transform.up * JetPackForce, ForceMode2D.Impulse);
            jetPackParticles.Play();
        
        }
        else
        {
            //return to normal state
            isJetPackActive = false;
            jetPackParticles.Stop();
        }
        //ensure jetpack cant go below 0
        if (JetPackFuel <= 0)
        {
            JetPackFuel = 0;
        }
        //regerentaing jetpack fuel
        if (JetPackFuel < 100 && !isJetPackActive)
        {
            JetPackFuel += JetPackFuelRegenRate * Time.deltaTime;
        }
        //ensure jetpack fuel cant go above 100
        if (JetPackFuel > 100)
        {
            JetPackFuel = 100;
        }
    }
}
