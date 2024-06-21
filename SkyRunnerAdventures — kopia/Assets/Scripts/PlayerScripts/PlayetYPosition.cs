using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerYPosition : MonoBehaviour
{

    [SerializeField] private GameObject playerCharacter;
    public TextMeshProUGUI uiText;
    private float maxY = 0f;


    // Update is called once per frame
    void Update()
    {
        if (playerCharacter != null && uiText != null)
        {

            float playerY = playerCharacter.transform.position.y;
            

            // Updating displayed player Y
            if (playerY > maxY)
            {
                maxY = playerY;
                float displayedPlayerY = maxY / 3;
                if (uiText.text != displayedPlayerY.ToString("F2"))
                {
                    uiText.text = displayedPlayerY.ToString("F2");
                }

            }
            
        }
    }
}
