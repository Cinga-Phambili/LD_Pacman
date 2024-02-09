using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    [Tooltip("The amount of points the ghost is worth when eaten.")]
    public int points;

    public Movement movementController { get; private set; }

    public GhostHome homeBehavior { get; private set; }
    public GhostScatter scatterBehaviour { get; private set; }
    public GhostFrightened frightenedBehavior { get; private set; }
    public GhostChase chaseBehaviour { get; private set; }

    public GhostBehavior initialBehavior;
    public Transform target;

    private GameManager gameManager;
    

    public void Awake()
    {
        this.movementController = GetComponent<Movement>();

        this.homeBehavior = GetComponent<GhostHome>();
        this.scatterBehaviour = GetComponent<GhostScatter>();
        this.frightenedBehavior = GetComponent<GhostFrightened>();
        this.chaseBehaviour = GetComponent<GhostChase>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movementController.ResetState();

        frightenedBehavior.Disable();
        chaseBehaviour.Disable();
        scatterBehaviour.Enable();

        if (homeBehavior != initialBehavior)
        {
            homeBehavior.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightenedBehavior.enabled)
            {
                gameManager.GhostEaten(this);
            }
            else
            {
                gameManager.PacmanEaten();
            }
        }
    }
}
