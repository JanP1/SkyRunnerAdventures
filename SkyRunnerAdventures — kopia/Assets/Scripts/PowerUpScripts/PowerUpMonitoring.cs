using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMonitoring : MonoBehaviour
{
    private Coroutine currentCoroutineShield = null;
    private Coroutine currentCoroutineSpeed = null;

    // Method to start the height change
    public void ChangeHeightSpeed(GameObject target, float targetHeight, float duration)
    {
        // Stop the current coroutine if it exists
        if (currentCoroutineSpeed != null)
        {
            StopCoroutine(currentCoroutineSpeed);
        }

        // Reset the height to 0 before starting the new coroutine
        RectTransform rectTransform = target.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0f);

        // Start the new coroutine and store the reference
        currentCoroutineSpeed = StartCoroutine(ChangeHeightCoroutine(target, targetHeight, duration, "Speed"));
    }



    public void ChangeHeightShield(GameObject target, float targetHeight, float duration)
    {
        // Stop the current coroutine if it exists
        if (currentCoroutineShield != null)
        {
            StopCoroutine(currentCoroutineShield);
        }

        // Reset the height to 0 before starting the new coroutine
        RectTransform rectTransform = target.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0f);

        // Start the new coroutine and store the reference
        currentCoroutineShield = StartCoroutine(ChangeHeightCoroutine(target, targetHeight, duration, "Shield"));
    }

    private IEnumerator ChangeHeightCoroutine(GameObject target, float targetHeight, float duration, string type)
    {
        RectTransform rectTransform = target.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("The target GameObject does not have a RectTransform component.");
            yield break;
        }

        float elapsedTime = 0f;
        float initialHeight = rectTransform.sizeDelta.y;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newHeight = Mathf.Lerp(initialHeight, targetHeight, elapsedTime / duration);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
            yield return null;
        }

        // Ensure the final height is set
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);

        // Reset the coroutine reference when done
        if (type == "Speed")
        {
            currentCoroutineSpeed = null;
        } 
        else
        {
            currentCoroutineShield = null;
        }
    }
}
