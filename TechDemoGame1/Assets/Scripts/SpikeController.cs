using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameObject player;
    public Collider2D spikecollidor;
    public GameController gameController;

    private void Start()
    {
        spikecollidor = GetComponent<Collider2D>();
        gameController = GetComponent<GameController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            gameController.Die();
        }
    }
}
