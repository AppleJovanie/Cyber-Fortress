using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMesh Pro namespace

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText; // Use TextMeshProUGUI instead of Text

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives + " Lives"; // Ensure there's a space before "Lives"
    }
}
