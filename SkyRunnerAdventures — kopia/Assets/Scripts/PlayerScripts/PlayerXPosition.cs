using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerXPosition : MonoBehaviour
{

    [SerializeField] private GameObject playerCharacter;
    public TextMeshProUGUI uiText;
    private float maxX = 0f;


    // Update is called once per frame
    void Update()
    {
        if (playerCharacter != null && uiText != null)
        {

            float playerX = playerCharacter.transform.position.x;


            // Updating displayed player X
            if (playerX > maxX)
            {
                maxX = playerX;
                float displayedPlayerX = maxX / 3;
                if (uiText.text != displayedPlayerX.ToString("F2"))
                {
                    uiText.text = displayedPlayerX.ToString("F2");
                }

            }

            

        }
    }
}
