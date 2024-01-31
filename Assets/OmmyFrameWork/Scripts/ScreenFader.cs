using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeOverlay;
    public float fadeDuration = 1.0f;

    private Coroutine currentFadeCoroutine;
    private Coroutine waitAndStartFadeOutCoroutine;

    private void Start()
    {
        if (fadeOverlay == null)
        {
            Debug.LogError("Fade Overlay Image not assigned. Please assign it in the inspector.");
        }
        else
        {
            FadeIn();
        }
    }

    // Call this function to start the fade-in process
    public void FadeIn()
    {
        StartFadeCoroutine(1.0f, 0.0f);
    }

    // Call this function to start the fade-out process
    public void FadeOut()
    {
        StartFadeCoroutine(0.0f, 1.0f);
    }

    // Call this function to start the fade-in and fade-out process
    public void FadeInOut()
    {
        StartFadeCoroutine(0.0f, 1.0f);
        waitAndStartFadeOutCoroutine = StartCoroutine(WaitAndStartFadeOut());
    }

    private void StartFadeCoroutine(float startAlpha, float targetAlpha)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        currentFadeCoroutine = StartCoroutine(Fade(startAlpha, targetAlpha));
    }

    IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        Color currentColor = fadeOverlay.color;
        float timer = 0.0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            currentColor.a = alpha;
            fadeOverlay.color = currentColor;
            yield return null;
        }
    }

    IEnumerator WaitAndStartFadeOut()
    {
        yield return new WaitForSeconds(fadeDuration);
        StartFadeCoroutine(1.0f, 0.0f);
        waitAndStartFadeOutCoroutine = null; // Reset the coroutine reference after starting a new one
    }
}
