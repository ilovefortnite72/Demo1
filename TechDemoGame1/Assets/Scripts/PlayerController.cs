using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    public GameController gameController;


    private Rigidbody2D rb;
    private float movespeed = 10;
    private float xAxis;
    [SerializeField]
    private float JumpForce = 10;

    [Header("Health")]
    public int maxHealth = 5;
    public int currentHealth;
    public Image[] healthImages;
    public Sprite[] healthSprites;

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
        currentHealth = maxHealth;
        checkHealthAmount();
        
        
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
        animator.SetBool("IsJumping", false);
        //if (isGrounded)
        //{
        //    Debug.Log("isGrounded");
        //}
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, checkDistance);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle");
            transform.position = gameController.Checkpointpos;
            TakeDamage(5);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameController.Die();
        }
    }


    void checkHealthAmount()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (currentHealth == i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
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
