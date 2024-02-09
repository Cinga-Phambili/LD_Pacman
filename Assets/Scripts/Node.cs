using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections { get; private set; }


    public void Start()
    {
        availableDirections = new List<Vector2>();
        
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 directionToCheck)
    {
        var hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, directionToCheck, 1, obstacleLayer);
        if (hit.collider == null)
        {
            availableDirections.Add(directionToCheck);
        }
    }
}
