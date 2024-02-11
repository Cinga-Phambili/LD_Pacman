using UnityEngine;

public class GhostScatter : GhostBehavior
{

    public void OnDisable()
    {
        Debug.Log("Disabling Scatter. Enabling Chase.");
        ghost.chaseBehaviour.Enable();
    }
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        var node = other.GetComponent<Node>();

        //Debug.Log("Hit Node", other.gameObject);
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

                //Debug.Log("Can't go back the way you came. Changed to " + node.availableDirections[index]);
            }

            this.ghost.movementController.SetDirection(node.availableDirections[index]);
        }
    }
}
