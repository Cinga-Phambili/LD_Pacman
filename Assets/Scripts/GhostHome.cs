using UnityEngine;
using System.Collections;

public class GhostHome : GhostBehavior
{
    public Transform insideHome;
    public Transform outsideHome;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    public void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private IEnumerator ExitTransition()
    {
        ghost.movementController.SetDirection(Vector2.up, true);
        this.ghost.movementController.rigidbody.isKinematic = true;
        this.ghost.movementController.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.insideHome.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = position;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.insideHome.position, outsideHome.position , elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = position;
            elapsed += Time.deltaTime;
            yield return null;
        }

        var randomValue = Random.Range(0, 1f);
        ghost.movementController.SetDirection(new Vector2(randomValue < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.movementController.rigidbody.isKinematic = false;
        this.ghost.movementController.enabled = true;
    }
}
