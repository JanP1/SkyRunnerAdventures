using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget; // Reference to the player's transform
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float xOffset = 0;

    [SerializeField] private bool changeX = true;


    void LateUpdate()
    {
        if (cameraTarget != null)
        {


            if (changeX)
            {
                Vector3 newPos = new Vector3(cameraTarget.position.x + xOffset, cameraTarget.position.y + yOffset, transform.position.z);
                transform.position = newPos;

            }
            else
            {
                Vector3 newPos = new Vector3(transform.position.x, cameraTarget.position.y + yOffset, transform.position.z);
                transform.position = newPos;

            }

        }
    }
}
