using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    
    private Vector2 aimPos;
    public float speed;
    public Rigidbody2D rb;
    private Camera Cam;
    public GameObject Brang;
    public Transform BrangTrans;
    public bool canThrow;
    public float timer;
    public float returnTime;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
        
    }

    private void Update()
    {
        aimPos = Cam.ScreenToWorldPoint(Input.mousePosition);

        HasReturned();

        Debug.Log("AimPos: " + aimPos);
        Debug.Log(canThrow);


        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            Throw();
        }

        
    }

    private void HasReturned()
    {
        if (Vector2.Distance(rb.position, aimPos) < 0.5f)
        {
            rb.velocity = Vector2.zero;
            canThrow = true;
        }
    }

    private void Throw()
    {
        Debug.Log("Throwing Boomerang");
        canThrow = false;
        //move boomerang to mouse position and back again after
        Vector2 throwDir = (aimPos - rb.position).normalized;
        rb.velocity = new Vector2(throwDir.x * speed, throwDir.y * speed);
        BrangTrans.position = rb.position;

    }
}
