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
        player = GameObject.Find("Player");

    }


}
