
using System;
using System.Collections;
using UnityEngine;

public class PlayerFollowing : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float delayTime = 0.25f; // Time to reach player
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    private PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("player");
        if(player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.enabled = false;
        }

        float mainDelay = delayTime;
        delayTime = 5f;
        StartCoroutine(ChangeDelayTime(delayTime, mainDelay, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset; 
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, delayTime);
    }


    private IEnumerator ChangeDelayTime(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current value based on elapsed time
            delayTime = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final value is set
        delayTime = endValue;
        playerMovement.enabled = true;
    }
}
