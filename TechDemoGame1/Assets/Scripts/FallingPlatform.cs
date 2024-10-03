using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1;
    public Rigidbody2D rb;
    public float DespawnDelay = 0.75f;
    public float checkDistance = 1f;
    public LayerMask whatIsPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private IEnumerable WaitForSecondsRealtime(float fallDelay)
    {
        yield return new WaitForSecondsRealtime(fallDelay);
        Fall();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WaitForSecondsRealtime(fallDelay);
            
        }
    }


    private void Fall()
    {
        rb.simulated = true;
        Destroy(gameObject, DespawnDelay);
        
    }
}
