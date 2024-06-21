using UnityEngine;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using TMPro;
using System;

public class BounceOff : MonoBehaviour
{

    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    [SerializeField] GameObject gameOverScreen; // Reference to the gameOver prefab

    [SerializeField] private float launchAngle = 45f; // Angle in degrees
    [SerializeField] private float launchDistance = 5f; // Distance to launch

    [SerializeField] private Rigidbody2D projectileRb;
    [SerializeField] private float movementDisabledTime = 0.75f;

    [SerializeField] private TextMeshProUGUI textMeshPro;

    [SerializeField] private float timeBeforeSceneSwitch = 2f;
    public Animator animator;


    private PlayerMovement playerMovement;
    private PlayerTimer playerTimer;
    private bool collisionHandled = false;
    
    
    public int livesLeft = 3;

    // to check if the shield is activated
    public bool takesDamage = true;

    private void Start()
    {
        // Find the GameObject with PlayerMovement script attached
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerTimer = GameObject.FindObjectOfType<PlayerTimer>();
    }


    private void Update()
    {
        if (livesLeft == 0)
        {
            StartCoroutine(EndGame());

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Enemy") && !collisionHandled)
        {

            if (takesDamage)
            {
                // Counting down the lives when the player hits an enemy or harming object
                if (livesLeft == 3)
                {
                    heart1.SetActive(false);
                    livesLeft--;
                }
                else if (livesLeft == 2)
                {
                    heart2.SetActive(false);
                    livesLeft--;
                }
                else
                {
                    heart3.SetActive(false);
                    livesLeft = 0;
                }
            }


            // If i colide i want the forces to be 0 first, so it doesn't bounce off an unconsistant amount
            if (projectileRb != null)
            {
                projectileRb.velocity = Vector2.zero;
                projectileRb.angularVelocity = 0f;

            }


            GameObject enemyObject = collision.gameObject;


            playerMovement.enabled = false; // Disable player movement

            // true if right, false if left
            if(enemyObject.transform.position.x <= gameObject.transform.position.x)
            {
                LaunchObject(true);

            }
            else 
            {
                LaunchObject(false);
            }


            Debug.Log("Collided with object with tag: Enemy");
            collisionHandled = true;
            StartCoroutine(EnablePlayerMovementAfterDelay(movementDisabledTime));
        }
    }

    private IEnumerator EnablePlayerMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.enabled = true; // Re-enable player movement
        collisionHandled = false; // Reset collision handling flag
    }

    private void LaunchObject(bool right)
    {
        if (right) {
            // Calculate launch direction
            Vector2 launchDirection = CalculateLaunchDirectionRight();
            // Apply force to the object
            projectileRb.AddForce(launchDirection, ForceMode2D.Impulse);

        }
        else
        {
            Vector2 launchDirection = CalculateLaunchDirectionLeft();
            // Apply force to the object
            projectileRb.AddForce(launchDirection, ForceMode2D.Impulse);

        }


    }

    private Vector2 CalculateLaunchDirectionRight()
    {
        // Calculate launch velocity using angle and distance
        float radAngle = launchAngle * Mathf.Deg2Rad;
        float launchSpeed = Mathf.Sqrt((Physics2D.gravity.magnitude * launchDistance * launchDistance) / (Mathf.Sin(2 * radAngle)));

        // Calculate x and y components of launch velocity
        float launchVelocityX = launchSpeed * Mathf.Cos(radAngle);
        float launchVelocityY = launchSpeed * Mathf.Sin(radAngle);

        // Calculate launch direction
        Vector2 launchDirection = new Vector2(launchVelocityX, launchVelocityY);

        return launchDirection;
    }


    private Vector2 CalculateLaunchDirectionLeft()
    {
        // Calculate launch velocity using angle and distance
        float radAngle = launchAngle * Mathf.Deg2Rad;
        float launchSpeed = Mathf.Sqrt((Physics2D.gravity.magnitude * launchDistance * launchDistance) / (Mathf.Sin(2 * radAngle)));

        // Calculate x and y components of launch velocity
        float launchVelocityX = -launchSpeed * Mathf.Cos(radAngle); // Negative for left direction
        float launchVelocityY = launchSpeed * Mathf.Sin(radAngle);

        // Calculate launch direction
        Vector2 launchDirection = new Vector2(launchVelocityX, launchVelocityY);

        return launchDirection;
    }



    public void ReactivateLives()
    {
        switch (livesLeft)
        {
            case 1:
                livesLeft = 2;
                heart2.SetActive(true);
                break;
            case 2:
                livesLeft = 3;
                heart1.SetActive(true);
                break;
        }
    }


    IEnumerator EndGame()
    {
        DateTime currentDateTime = DateTime.Now;


        animator.SetBool("isDead", true);
        gameOverScreen.SetActive(true);
        playerMovement.enabled = false;
        playerTimer.enabled = false;
        
        
        yield return new WaitForSeconds(timeBeforeSceneSwitch);


        string newTime = textMeshPro.text;
        string newValue = currentDateTime.ToString();
        DataManagerHelper.Instance.AddNewData(newTime, newValue);

        SceneController.instance.LoadScene("Highscore");


    }



}
