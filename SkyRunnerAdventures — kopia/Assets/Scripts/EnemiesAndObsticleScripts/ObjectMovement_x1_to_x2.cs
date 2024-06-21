/*using System.Collections;
using UnityEngine;

public class ObsticleMovement : MonoBehaviour
{
    public Transform startPosition; // x1
    public Transform endPosition;   // x2
    public float speed = 1.0f;      // Speed of movement
    public float waitingTime = 0f; // Time to wait at the ends of the journey

    private float journeyLength;
    private bool movingToEnd = true; // Flag to track the direction of movement

    void Start()
    {
        // Calculate the journey length between start and end positions
        journeyLength = Vector3.Distance(startPosition.position, endPosition.position);
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            // Calculate how far along the journey the object should be
            float startTime = Time.time;
            float distCovered = 0;
            float fracJourney = 0;

            while (fracJourney < 1.0f)
            {
                distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength;

                // Move the object towards the end position or start position based on the direction
                if (movingToEnd)
                {
                    transform.position = Vector3.Lerp(startPosition.position, endPosition.position, fracJourney);
                }
                else
                {
                    transform.position = Vector3.Lerp(endPosition.position, startPosition.position, fracJourney);
                }

                yield return null; // Wait for the next frame
            }

            // Toggle the direction
            movingToEnd = !movingToEnd;

            // Wait for the specified waiting time at the ends of the journey
            yield return new WaitForSeconds(waitingTime);
        }
    }
}
*/


using System.Collections;
using UnityEngine;

public class ObsticleMovement : MonoBehaviour
{
    public Transform startPosition; // x1
    public Transform endPosition;   // x2
    public float speed = 1.0f;      // Speed of movement
    public float waitingTime = 2.0f; // Time to wait at the ends of the journey
    public float startFraction = 0.0f; // Starting fraction of the journey (0.0 to 1.0)

    private float journeyLength;
    private bool movingToEnd = true; // Flag to track the direction of movement
    //private bool isWaiting = false; // Flag to check if waiting

    void Start()
    {
        // Calculate the journey length between start and end positions
        journeyLength = Vector3.Distance(startPosition.position, endPosition.position);

        // Set the initial position based on the starting fraction
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, startFraction);

        // Start the movement coroutine
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            // Calculate the journey length for this segment
            float startTime = Time.time;
            float distCovered = 0;
            float fracJourney = startFraction; // Start at the specified fraction

            while (fracJourney < 1.0f)
            {
                distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength + startFraction; // Continue from the start fraction

                // Move the object towards the end position or start position based on the direction
                if (movingToEnd)
                {
                    transform.position = Vector3.Lerp(startPosition.position, endPosition.position, fracJourney);
                }
                else
                {
                    transform.position = Vector3.Lerp(endPosition.position, startPosition.position, fracJourney);
                }

                yield return null; // Wait for the next frame
            }

            // Toggle the direction
            movingToEnd = !movingToEnd;

            // Wait for the specified waiting time at the ends of the journey
            yield return new WaitForSeconds(waitingTime);

            // Reset the start fraction to 0 after the first loop
            startFraction = 0.0f;
        }
    }
}
