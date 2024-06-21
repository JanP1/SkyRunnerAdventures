using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a specific tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Do something when colliding with an object with the specified tag
            UnityEngine.Debug.Log("Collided with object with tag: Player");
        }

       
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object has a specific tag
        if (other.CompareTag("Player"))
        {
            // Do something when colliding with a trigger object with the specified tag
            UnityEngine.Debug.Log("Triggered object with tag: YourTag");
        }

        
    }*/
}
