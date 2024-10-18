using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public Vector2 Checkpointpos;
    public GameObject player;
    public Rigidbody2D rb;
    


    public Animator animator;
    public GameOverScreen GOS;


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


    public void GameOver()
    {
        GOS.Setup();
    }


    public void Die()
    {
        animator.SetBool("IsDead", true);
        player.transform.position = Checkpointpos;
        Debug.Log("You died");
        GOS.Setup();

        if (Vector2.Distance(player.transform.position, Checkpointpos)< 1)
        {
            
            animator.SetBool("IsDead", false);
            
        }

        
    }
}
