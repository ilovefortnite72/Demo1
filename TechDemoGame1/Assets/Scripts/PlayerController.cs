using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    public GameController gameController;
    public PauseMenu pauseMenu;
    

    public Vector2 PlayerPos;
    private Transform origParent;

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

    public float MaxJetPackFuel = 50;

    [SerializeField] public float JetPackFuel = 50;
    [SerializeField] private float JetPackFuelBurnRate = 10;
    [SerializeField] private float JetPackFuelRegenRate = 5;
    [SerializeField] private bool isJetPackActive = false;
    [SerializeField] private float JetPackForce = 10;
    public Slider fuelSlider;

    public ParticleSystem jetPackParticles;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        currentHealth = maxHealth;
        
        
        
    }

    //make sure the player is parented to the moving platform and doesnt slider around on it

    public void OnCollisionEnter2D(Collision2D collision)
    { 

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            origParent = transform.parent;
            transform.parent = collision.transform;
        }
    }

    //removes player from parent when they leave the platform so they can move freely

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = origParent;
        }
    }

    //check if player is hit by an obstacle and die if they are
    //also check if player has reached the end of the level and load the next level

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameController.Die();
        }

        if (collision.gameObject.CompareTag("LevelSwap"))
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    void Update()
    {
        pouseMenu();
        InputControls();
        Move();
        IsGrounded();
        Jump();
        JetPack();
        fuelSlider.value = MaxJetPackFuel / JetPackFuel;
        fuelSliderUpdate();
        

    }

    //display pause menu (bad variable name as it was causing conflicts)

    private void pouseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.PauseGame();
        }
    }
    

    //basic movement controls
    private void InputControls()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }


    //move player based on input so sprite faces the correct direction
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

    //check if player is grounded and change animation state
    public void OnLand()
    {
        animator.SetBool("IsJumping", false);
    }




    //check if player is grounded
    public bool IsGrounded()
    {
        
        if (isGrounded = Physics2D.OverlapCircle(player.position, checkDistance, whatIsGround))
        {
            animator.SetBool("IsJumping", false);
            return true;
        }
        else
        {
            return false;
        }

        
        //if (isGrounded)
        //{
        //    Debug.Log("isGrounded");
        //}
        
    }

    //simple debug to show the ground check radius

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, checkDistance);
    }

    //jump function checks if player is grounded and if they are, adds jump force and changes animation state
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //add jump force, and change variables around for animation
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            animator.SetBool("IsJumping", true);
            isGrounded = false;
        }
    }

    //jetpack fuel slider update
    private void fuelSliderUpdate()
    {
        if (fuelSlider != null)
        {
            fuelSlider.value = JetPackFuel;
            
        }
    }


    //jetpack function 

    public void JetPack()
    {
        //check if player is holding jump button and jetpack fuel is more than 0 and player is not grounded
        if (Input.GetButton("Jump") && JetPackFuel > 0 && !IsGrounded())
        {
        
            isJetPackActive = true;
            JetPackFuel -= JetPackFuelBurnRate * Time.deltaTime;
            
            rb.AddForce(rb.transform.up * JetPackForce, ForceMode2D.Impulse);
            jetPackParticles.Play();
            fuelSliderUpdate();

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
