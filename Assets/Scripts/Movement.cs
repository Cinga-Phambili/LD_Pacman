using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private Vector2 initialDirection;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float footprint;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }



    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    public void Start()
    {
        ResetState();
    }

    public void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
        
        float angle = Mathf.Atan2(direction.y, -direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, -Vector3.forward);
    }

    public void FixedUpdate()
    {
        var position = this.rigidbody.position;
        var translation = speedMultiplier * speed * Time.fixedDeltaTime * direction;
        
        rigidbody.MovePosition(position + translation);
    }

    public void ResetState()
    {
        speedMultiplier = 1.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;

        this.enabled = true;
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        var hit = Physics2D.BoxCast(transform.position, Vector2.zero * footprint, 0f, direction, 1.5f,obstacleLayer);
        return hit.collider != null;
    }
}