using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float verticalInput;
    private float speed = 5;
    private bool isLadder;
    private bool isClimbing;


    [SerializeField] private Rigidbody2D rb;

    //check if player is on ladder

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    
    //check if player is off ladder

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (isLadder == true && Mathf.Abs(verticalInput)>0f)
        {
            isClimbing = true;
        }
    }

    //movement for climbing on ladder

    private void FixedUpdate()
    {
        if (isClimbing == true)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * speed);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
