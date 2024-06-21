using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRemoval : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collided with has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Picked up a powerup");
            // Destroy the collided object
            Destroy(gameObject);
        }
    }
}
