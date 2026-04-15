using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public bool fadeInOnStart = true;
    public bool startHidden = true;

    private Coroutine runningFade;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private IEnumerator Start()
    {
        if (startHidden)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (fadeInOnStart)
        {
            yield return FadeIn();
        }
    }

    public void FadeInUI()
    {
        StartFade(FadeIn());
    }

    public void FadeOutUI()
    {
        StartFade(FadeOut());
    }

    public void FadeOutAfterDelay(float delay)
    {
        StartCoroutine(FadeOutDelayed(delay));
    }

    private IEnumerator FadeOutDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return FadeOut();
    }

    private void StartFade(IEnumerator fadeRoutine)
    {
        if (runningFade != null)
        {
            StopCoroutine(runningFade);
        }

        runningFade = StartCoroutine(fadeRoutine);
    }

    public IEnumerator FadeIn()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        runningFade = null;
    }

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        runningFade = null;
    }
}
