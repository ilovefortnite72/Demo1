using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1;
    [SerializeField] public Rigidbody2D rb;
    public float DespawnDelay = 0.75f;
    
    public LayerMask whatIsPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSecondsRealtime(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, DespawnDelay);

    }

    

}
