using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer defaultBody;
    public SpriteRenderer frightenedBody;
    public SpriteRenderer flashingBody;
    public SpriteRenderer eatenBody;

    private bool eaten;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.defaultBody.gameObject.SetActive(false);
        this.frightenedBody.gameObject.SetActive(true);

        
        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        
        this.defaultBody.gameObject.SetActive(true);
        this.frightenedBody.gameObject.SetActive(false);
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            frightenedBody.gameObject.SetActive(false);
            flashingBody.gameObject.SetActive(true);
        }
    }

    private void Eaten()
    {
        eaten = true;
        
        var position = this.ghost.homeBehavior.insideHome.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;
        
        this.ghost.homeBehavior.Enable(this.duration);

        flashingBody.gameObject.SetActive(false);
        frightenedBody.gameObject.SetActive(false);
        defaultBody.gameObject.SetActive(false);
        eatenBody.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        this.ghost.movementController.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movementController.speedMultiplier = 1.0f;
        this.eaten = false;
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }
        }
    }
}
