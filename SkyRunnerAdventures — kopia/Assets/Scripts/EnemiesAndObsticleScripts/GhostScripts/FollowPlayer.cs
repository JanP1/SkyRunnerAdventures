using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("If no maxDistance, set value to 0 or less")]
    [SerializeField] private float maxDistance = 10f; //Maximum distance that the enemy can run away from the player

    [SerializeField] private float timeBeforeSceneSwitch = 2f;


    [SerializeField] private GameObject playerObject; // Reference to player object
    [SerializeField] private GameObject gameOverScreen; // Reference to the gameOver prefab


    [SerializeField] private TextMeshProUGUI textMeshPro;

    [SerializeField] private Transform target; // player

    [Header("Force Magnitude will increase over time \nbased on the forceIncreaseInterval")]
    [SerializeField] private float forceMagnitude = 10f;
    [SerializeField] private float updateDelay = 1f; // Delay for updating the position of the enemy
    [SerializeField] private float forceIncreaseInterval = 60f;

    private float timePassed;
    private float lastForceChangeTime = 0f;


    private bool gameStarted = false;
    private bool gameEnded = false;
    private Rigidbody2D rb;
    private Vector3 lastTargetPosition; // Last known position of the player
    private float lastUpdateTime; // Time when the target position was last updated

    private PlayerTimer playerTimer;
    private PlayerMovement scriptToDisable;

    void Start()
    {
        scriptToDisable = playerObject.GetComponent<PlayerMovement>();


        playerTimer = playerObject.GetComponent<PlayerTimer>();

        rb = GetComponent<Rigidbody2D>();
        lastTargetPosition = target.position;
        lastUpdateTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object has a specific tag
        if (other.CompareTag("Player"))
        {
            // After colliding, the movement mechanics are turned off
            DisableScriptOnObject();

            gameEnded = true;
            playerObject.SetActive(false);

            StartCoroutine(EndGame());
        }
    }

    void Update()
    {

        // Check if the game has started or if one of the WSAD keys is pressed
        if (!gameStarted)
        {
            if (scriptToDisable.enabled && 
               (Input.GetKeyDown(KeyCode.W) || 
                Input.GetKeyDown(KeyCode.A) || 
                Input.GetKeyDown(KeyCode.S) || 
                Input.GetKeyDown(KeyCode.D)))
            {
                gameStarted = true;
            }
        }

        if (gameStarted && !gameEnded)
        {
            if (maxDistance > 0)
            {
                if (Math.Abs(gameObject.transform.position.x - target.position.x) > maxDistance)
                {
                    if (gameObject.transform.position.x > target.position.x)
                    {
                        gameObject.transform.position = new Vector3(target.position.x + maxDistance, gameObject.transform.position.y, 0);
                    }
                    else
                    {
                        gameObject.transform.position = new Vector3(target.position.x - maxDistance, gameObject.transform.position.y, 0);
                    }
                }

                if (Math.Abs(gameObject.transform.position.y - target.position.y) > maxDistance)
                {
                    if (gameObject.transform.position.y > target.position.y)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, target.position.y + maxDistance, 0);
                    }
                    else
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, target.position.y - maxDistance, 0);
                    }
                }
            }
        }

        
        timePassed = playerTimer.timer;


        if (timePassed - lastForceChangeTime >= forceIncreaseInterval)
        {
            if(forceMagnitude < 100)
            {
                forceMagnitude += 5;
                lastForceChangeTime = timePassed;
            }
            
        }


    }

    void FixedUpdate()
    {
        if (gameStarted && !gameEnded)
        {
            // Check if it's time to update the target position
            if (Time.time - lastUpdateTime >= updateDelay)
            {
                lastTargetPosition = target.position;
                lastUpdateTime = Time.time;
            }

            // Calculate the direction from ObjectA to the last known position of ObjectB
            Vector3 direction = (lastTargetPosition - transform.position).normalized;

            // Apply force towards the last known position of ObjectB
            rb.AddForce(direction * forceMagnitude);
        }
    }

    // Disable player's movement
    void DisableScriptOnObject()
    {
        // Check if playerObject is assigned
        if (playerObject != null)
        {
            // Get the PlayerMovement component attached to playerObject

            // Check if the component is found
            if (scriptToDisable != null)
            {
                // Disable the script
                scriptToDisable.enabled = false;
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on ObjectA.");
            }
        }
        else
        {
            Debug.LogWarning("ObjectA reference is not assigned.");
        }
    }


    IEnumerator EndGame()
    {
        DateTime currentDateTime = DateTime.Now;


        gameOverScreen.SetActive(true);
        playerTimer.enabled = false;


        yield return new WaitForSeconds(timeBeforeSceneSwitch);


        string newTime = textMeshPro.text;
        string newValue = currentDateTime.ToString();
        DataManagerHelper.Instance.AddNewData(newTime, newValue);

        SceneController.instance.LoadScene("Highscore");


    }


}
