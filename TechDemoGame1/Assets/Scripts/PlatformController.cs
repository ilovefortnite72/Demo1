using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlatformController : MonoBehaviour
{

    [Header("Platform Movement")]
    [SerializeField] public float speed = 5;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;


    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(startPosition.x, startPosition.y);
    }

    private void Update()
    {

        Move();     
        
    }

    private void Move()
    {
        
        //move platform to target local position
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            //if platform reaches target position, swap target position
            Vector3 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
            //move platform to new target position
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
    }
}
