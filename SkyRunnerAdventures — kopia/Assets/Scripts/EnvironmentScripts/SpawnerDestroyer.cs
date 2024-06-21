using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnerDestroyer : MonoBehaviour
{

    System.Random random = new System.Random();
    public GameObject generatedPlatform;
    private Transform cameraTransform;

    [SerializeField] private float platformSegmentLength = 40f;
    [SerializeField] private float platformSegmentHeight = 30f;
    

    void Start()
    {
        // Find the main camera
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        int randomNumber = random.Next(1, 7);
        generatedPlatform = Resources.Load<GameObject>("Platforms" + randomNumber.ToString());
        if (Mathf.Abs(transform.position.y - cameraTransform.position.y) > 1.5*platformSegmentHeight)
        {
            // If the object's x is greater than the camera's x, spawn it at camera x - 3*platfLength
            if (transform.position.y > cameraTransform.position.y)
            {
                SpawnObjectY(transform.position.y - 3*platformSegmentHeight);
            }
            // If the object's x is less than the camera's x, spawn it at camera x + 3*platfLength
            else
            {
                SpawnObjectY(transform.position.y + 3*platformSegmentHeight);
            }

            // Destroy the current object
            Destroy(gameObject);
        }


        // Check if the object is further away from the camera than 1.5*platfLength on the x-axis
        if (Mathf.Abs(transform.position.x - cameraTransform.position.x) > 1.5*platformSegmentLength)
        {
            // If the object's x is greater than the camera's x, spawn it at camera x - 60
            if (transform.position.x > cameraTransform.position.x)
            {
                SpawnObjectX(transform.position.x - 3*platformSegmentLength);
            }
            // If the object's x is less than the camera's x, spawn it at camera x + 60
            else
            {
                SpawnObjectX(transform.position.x + 3*platformSegmentLength);
            }

            // Destroy the current object
            Destroy(gameObject);
        }
    }

    void SpawnObjectX(float xPosition)
    {
        // Instantiate a new object at the specified position and the same y and z positions
        GameObject newObject = Instantiate(generatedPlatform, new Vector3(xPosition, transform.position.y, transform.position.z), Quaternion.identity);
    }


    void SpawnObjectY(float yPosition)
    {
        // Instantiate a new object at the specified position and the same y and z positions
        GameObject newObject = Instantiate(generatedPlatform, new Vector3(transform.position.x, yPosition, transform.position.z), Quaternion.identity);
    }
}
