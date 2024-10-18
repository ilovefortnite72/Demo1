using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class BoomerangManager : MonoBehaviour
{
    public Transform player;
    private Vector2 aimPos;
    public float speed;
    public Rigidbody2D rb;
    private Camera Cam;
    public bool canThrow;
    private Vector2 throwDir;
    public bool hasReturned;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
        canThrow = true;    
        hasReturned = false;
    }

    //get mouse position and throw boomerang towards mouse position
    private void Update()
    {
        aimPos = Cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            hasReturned = false;
            canThrow = false;
            StartCoroutine(Throw());

        }

        _hasReturned();
        
        
    }

    //check if boomerang has returned to player position
    private void _hasReturned()
    {
        if (Vector2.Distance(rb.position, player.position) < 0.05f)
        {
            rb.velocity = Vector2.zero;
            hasReturned = true;
            canThrow = true;
            
        }
        
    }

    //throw boomerang towards mouse position then return to player position even if player moves from original position

    private IEnumerator Throw()
    {
        while (Vector2.Distance(rb.position, aimPos) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(rb.position, aimPos, speed * Time.deltaTime);
            transform.Rotate(0, 0, 10); 
            yield return null;
        }

        hasReturned = false;
        //return boomerang to player position after reaching mouse position

        while(Vector2.Distance(rb.position, player.position) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
            transform.Rotate(0, 0, 10);
            yield return null;
        }
        
    }
}
