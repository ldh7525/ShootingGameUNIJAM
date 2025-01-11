using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;  // 페이드 지속 시간 (초 단위)

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        DateTime startTime = DateTime.Now;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while ((DateTime.Now - startTime).TotalSeconds < fadeDuration)
        {
            double elapsed = (DateTime.Now - startTime).TotalSeconds;
            color.a = Mathf.Lerp(1f, 0f, (float)(elapsed / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        DateTime startTime = DateTime.Now;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while ((DateTime.Now - startTime).TotalSeconds < fadeDuration)
        {
            double elapsed = (DateTime.Now - startTime).TotalSeconds;
            color.a = Mathf.Lerp(0f, 1f, (float)(elapsed / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }
}
