using System;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform target;
    public void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = target.transform.position;
    }
}
