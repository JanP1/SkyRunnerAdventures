using System;
using System.Collections;
using UnityEngine;

public class GhostPositionTracker : MonoBehaviour
{
    public GameObject objectA;
    public GameObject circleObject;
    public float distanceTrackerMin = 15f;
    public float distanceTrackerMax = 50f;
    
    
    
    private GameObject cicleColorObject;

    public float circleOffsetX = 13f; // Offset from the edge of the camera viewport
    public float circleOffsetY = 7f;




    private SpriteRenderer spriteRenderer;
    private Renderer objectRenderer;
    private Camera mainCamera;

    private bool isDelayed = false;

    void Start()
    {

        mainCamera = GetComponent<Camera>();
        objectRenderer = objectA.GetComponent<Renderer>();


        if (circleObject != null)
        {
            // Find the child object by name
            Transform childTransform = circleObject.transform.Find("ColorCircle");

            // Check if the child was found
            if (childTransform != null)
            {
                cicleColorObject = childTransform.gameObject;

                // Now you have a reference to the child object, you can manipulate it as needed
                Debug.Log("Child object found: " + cicleColorObject.name);
                
                spriteRenderer = cicleColorObject.GetComponent<SpriteRenderer>();

            }
            else
            {
                Debug.LogWarning("Child object not found!");
            }
        }
        else
        {
            Debug.LogError("Prefab reference is not assigned!");
        }

    }

    void Update()
    {
        if (!isDelayed)
        {
            StartCoroutine(TrackerDelay(2f));
        }
        else
        {

            if (objectA != null && circleObject != null)
            {
                if (!objectRenderer.isVisible)
                {
                    float newXPosition = 0;
                    float newYPosition = 0;

                    float ghostX = objectA.transform.position.x;
                    float ghostY = objectA.transform.position.y;

                    float cameraX = mainCamera.transform.position.x;
                    float cameraY = mainCamera.transform.position.y;

                    if (ghostX > (cameraX + circleOffsetX))
                    {
                        newXPosition = cameraX + circleOffsetX;
                    }
                    else if (ghostX < (cameraX - circleOffsetX))
                    {
                        newXPosition = cameraX - circleOffsetX;
                    }
                    else
                    {
                        newXPosition = ghostX;
                    }

                    if (ghostY > (cameraY + circleOffsetY))
                    {
                        newYPosition = cameraY + circleOffsetY;
                    }
                    else if (ghostY < (cameraY - circleOffsetY))
                    {
                        newYPosition = cameraY - circleOffsetY;
                    }
                    else
                    {
                        newYPosition = ghostY;
                    }

                    circleObject.transform.position = new Vector3(newXPosition, newYPosition, circleObject.transform.position.z);

                    circleObject.SetActive(true);


                    // -- Calculating distance camera <-> ghost --

                    float ghostDistance = CalculateDistance(cameraX, cameraY, ghostX, ghostY);



                    if (ghostDistance < distanceTrackerMin)
                    {
                        ghostDistance = 0f;
                    }
                    else
                    {
                        ghostDistance -= distanceTrackerMin;
                    }
                    if (ghostDistance > distanceTrackerMax)
                    {
                        ghostDistance = distanceTrackerMax - distanceTrackerMin;
                    }




                    // --
                    // --- Changing the color depending on the distance -----
                    float greenValue = ghostDistance / (distanceTrackerMax - distanceTrackerMin);
                    Color currentColor = spriteRenderer.color;
                    currentColor.g = greenValue;
                    spriteRenderer.color = currentColor;
                }
                else
                {
                    circleObject.SetActive(false);
                }
            }
            
        }
    }


    static float CalculateDistance(float x1, float y1, float x2, float y2)
    {
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }


    private IEnumerator TrackerDelay(float duration)
    {
        circleObject.SetActive(false);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        isDelayed  = true;
    }
}
