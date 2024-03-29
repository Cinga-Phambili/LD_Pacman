using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    private Movement movementController;

    public void Awake()
    {
        movementController = GetComponent<Movement>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementController.SetDirection(Vector2.up);
        }
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementController.SetDirection(Vector2.down);
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementController.SetDirection(Vector2.left);
        }
        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementController.SetDirection(Vector2.right);
        }
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movementController.ResetState();
    }
}
