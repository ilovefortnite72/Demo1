using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameController gameController;
    public PlayerController playerController;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Obstacle");
            transform.position = gameController.Checkpointpos;

        }
    }


}
