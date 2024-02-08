using System;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    
    protected GameManager GameManager;

    protected virtual void Eat()
    {
        GameManager.PelletEaten(this);        
    }
    
    public void Start()
    {
        GameManager = GameObject.FindObjectOfType<GameManager>();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
