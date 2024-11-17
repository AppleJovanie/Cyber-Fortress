using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LiveUIManager : MonoBehaviour
{
    public TMP_Text livesText; // Text element to display lives information
    public TMP_Text damageText; // Text element to display damage taken

    private Coroutine damageFadeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the damageText at the start of the game
        if (damageText != null)
        {
            damageText.gameObject.SetActive(false);
        }
    }

    // Updates the player's remaining lives
    public void UpdateLivesUI(int currentLives)
    {
        livesText.text = $"Lives Left: {currentLives}";
    }

    // Displays the damage deduction when an enemy reaches the endpoint
    public void ShowDamage(int damage)
    {
        if (damageText != null)
        {
            damageText.gameObject.SetActive(true); // Enable the text
            damageText.text = $"-{damage} Damage!";
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1f); // Reset alpha to full

            // Fade out the damage text
            if (damageFadeCoroutine != null)
            {
                StopCoroutine(damageFadeCoroutine);
            }
            damageFadeCoroutine = StartCoroutine(FadeOutDamageText());
        }
    }

    // Coroutine to fade out the damage text over time
    private IEnumerator FadeOutDamageText()
    {
        float fadeDuration = 2f; // Duration of fade in seconds
        float startAlpha = damageText.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color color = damageText.color;
            color.a = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            damageText.color = color;
            yield return null;
        }

        // Ensure the text is fully transparent at the end
        Color finalColor = damageText.color;
        finalColor.a = 0f;
        damageText.color = finalColor;

        // Disable the damageText after fading out
        damageText.gameObject.SetActive(false);
    }
}
