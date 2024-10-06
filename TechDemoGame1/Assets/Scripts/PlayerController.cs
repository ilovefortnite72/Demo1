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
    private float JumpForce = 10;

    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField]
    public Transform groundCheck;
    public float checkDistance = 0.2f;
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
        Jump();
        JetPack();
        IsGrounded();
        

    }

    private void InputControls()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

    }

    private void Move()
    {
        rb.velocity = new Vector2(xAxis * movespeed, rb.velocity.y);

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



    public void IsGrounded()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, whatIsGround);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            animator.SetBool("IsJumping", true);
            isGrounded = false;
            
        }
        
    }

    public void JetPack()
    {

        if (Input.GetButton("Jump") && JetPackFuel > 0 && !isGrounded)
        {
        
            isJetPackActive = true;
            JetPackFuel -= JetPackFuelBurnRate * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, JetPackForce);
            jetPackParticles.Play();
        
        }
        else
        {

            isJetPackActive = false;
            jetPackParticles.Stop();
        }

        if (JetPackFuel <= 0)
        {
            JetPackFuel = 0;
        }

        if (JetPackFuel < 100 && !isJetPackActive)
        {
            JetPackFuel += JetPackFuelRegenRate * Time.deltaTime;
        }
    }
}
