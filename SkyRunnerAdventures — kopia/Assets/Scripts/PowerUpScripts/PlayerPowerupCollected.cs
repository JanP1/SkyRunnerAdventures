using System.Collections;
using UnityEngine;

public class PlayerPowerupCollected : MonoBehaviour
{

    [SerializeField] private GameObject speedShaddow;
    [SerializeField] private GameObject shieldShaddow;


    [SerializeField] public float shieldTurnOffTime = 10.0f;
    [SerializeField] public float speedBoostTurnOffTime = 10.0f;


    private Coroutine boostCoroutine = null;
    private Coroutine shieldCoroutine = null;



    private float defaultSpeed;
    private float defaultDeceleration;

    private BounceOff bounceOff;
    private PlayerMovement playerMovement;
    private PowerUpMonitoring powerUpMonitoring;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        bounceOff = GetComponent<BounceOff>();
        powerUpMonitoring = GetComponent<PowerUpMonitoring>();


        defaultSpeed = playerMovement.maxSpeed;
        defaultDeceleration = playerMovement.decelerationFactor;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {



        // LifeIncrease ---------------------------------------
        if (collision.CompareTag("LifeIncrease"))
        {
            UnityEngine.Debug.Log("Picked up a life");
            bounceOff.ReactivateLives();
        }

        // SpeedIncrease --------------------------------------
        if (collision.CompareTag("SpeedIncrease"))
        {

            powerUpMonitoring.ChangeHeightSpeed(speedShaddow, 50f, speedBoostTurnOffTime);


            UnityEngine.Debug.Log("Picked up speed");

            playerMovement.maxSpeed = defaultSpeed * 1.5f;
            playerMovement.decelerationFactor = 0.5f;

            if (boostCoroutine != null)
            {
                StopCoroutine(boostCoroutine);
            }

            boostCoroutine = StartCoroutine(DisableBoostAfterDelay(speedBoostTurnOffTime));


        }

        // Resistance -----------------------------------------
        if (collision.CompareTag("Resistance"))
        {
            powerUpMonitoring.ChangeHeightShield(shieldShaddow, 50f, shieldTurnOffTime);


            bounceOff.takesDamage = false;
            UnityEngine.Debug.Log("Picked up a shield");

            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
            }

            // Turning the damage back on after a given amount of time
            shieldCoroutine = StartCoroutine(EnableDamageAfterDelay(shieldTurnOffTime));
        }

    }



    // Coroutine to enable damage after a delay
    private IEnumerator EnableDamageAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);

        // After the delay, set takesDamage back to true
        UnityEngine.Debug.Log("Player is taking damage");
        bounceOff.takesDamage = true;
    }

    private IEnumerator DisableBoostAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);

        // After the delay, set takesDamage back to true
        UnityEngine.Debug.Log("Player speed set to default");

        playerMovement.maxSpeed = defaultSpeed;
        playerMovement.decelerationFactor = defaultDeceleration;
    }


}
