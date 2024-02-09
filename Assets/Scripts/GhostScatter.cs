using UnityEngine;

public class GhostScatter : GhostBehavior
{

    public void OnDisable()
    {
        ghost.chaseBehaviour.Enable();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        var node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghost.frightenedBehavior.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -ghost.movementController.direction)
            {
                index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movementController.SetDirection(node.availableDirections[index]);
        }
    }
}
