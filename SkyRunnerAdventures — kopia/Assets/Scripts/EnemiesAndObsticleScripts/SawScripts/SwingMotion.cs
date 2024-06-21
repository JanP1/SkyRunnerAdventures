using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float switchDistance = 2f; // Threshold distance for switching motion
    [SerializeField] private Transform centeredObject; // Object to compare positions with
    //[SerializeField] private float distanceToSpeed = 100f; // to get smooth swing motion
    //[SerializeField] private float minimumDistance = 0.01f;

    private float distance;

    private bool isMovingRight = true;
    private float centeredObjectX; // Starting x-position of the other object

    void Start()
    {
        if (centeredObject != null)
        {
            centeredObjectX = centeredObject.position.x;
        }
    }

    void Update()
    {

        // Check if the other object's starting x-position is set and valid
        if (centeredObject != null)
        {
            // Calculate the distance between the other object's starting x-position and the pendulum's current x-position
            distance = Mathf.Abs(centeredObjectX - transform.position.x);

            // Check if the distance exceeds the switchDistance
            if (distance >= switchDistance)
            {
                // Change motion direction
                isMovingRight = !isMovingRight;

            }
            // Rotate the pendulum
            RotatePendulum();
        }


    }

    void RotatePendulum()
    {
        // Determine the rotation direction
        int direction = isMovingRight ? 1 : -1;

        float angle = direction * rotationSpeed * Time.deltaTime;




        // Apply the rotation around the Z-axis
        transform.RotateAround(anchorPoint.position, new Vector3(0, 0, 1), angle);
    }


}

