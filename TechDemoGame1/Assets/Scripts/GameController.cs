using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public Vector2 Checkpointpos;
    public GameObject player;
    public Rigidbody2D rb;

    public Animator animator;


    private void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
        Checkpointpos = transform.position;
    }


    void Update()
    {
        Restart();

    }


    

    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = Checkpointpos;
        }
    }

    public void Die()
    {
        player.transform.position = Checkpointpos;
        animator.SetBool("isDead", true);
        Debug.Log("You died");
    }
}
