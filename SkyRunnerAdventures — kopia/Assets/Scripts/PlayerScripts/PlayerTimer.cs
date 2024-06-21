using UnityEngine;
using TMPro;

public class PlayerTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TMP text component
    private bool hasMoved = false;    // To check if the player has moved

    [HideInInspector]
    public float timer = 0f;         // Timer value

    void Start()
    {
        // Ensure the timer text is set to 0 at the start
        timerText.text = "0:00";
    }

    void Update()
    {
        // Check if the player has pressed W, A, S, or D keys
        if (!hasMoved && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            hasMoved = true; // Player has moved
        }

        // If the player has moved, start the timer
        if (hasMoved)
        {
            timer += Time.deltaTime; // Update the timer
            UpdateTimerText(); // Update the TMP text
        }
    }

    // Method to update the TMP text with the formatted timer
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
