using UnityEngine;

public class GhostChase : GhostBehavior
{
    public void OnDisable()
    {
        ghost.scatterBehaviour.Enable();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        var node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghost.frightenedBehavior.enabled)
        {
            var direction = Vector2.zero;
            var minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }

                this.ghost.movementController.SetDirection(direction);
            }
        }
    }
}
