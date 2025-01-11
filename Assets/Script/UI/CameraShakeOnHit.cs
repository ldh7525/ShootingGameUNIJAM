using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraShakeOnHit : MonoBehaviour
{
    // Reference to the main camera
    private Transform cameraTransform;
    private PlayerMovement playerMove;
    [SerializeField] private Image screenOverlay;

    // Shake settings
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    // Screen flash settings
    [SerializeField] private Color hitColor = new Color(1f, 0f, 0f, 0.5f);
    [SerializeField] private float flashDuration = 0.5f;

    private Vector3 originalPosition;
    private Color originalColor;

    void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        cameraTransform = Camera.main.transform;
        if (screenOverlay != null)
        {
            originalColor = screenOverlay.color;
        }
        originalPosition = cameraTransform.localPosition;
    }

    // Call this method when damage is received
    public void TriggerShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamera());
        if (screenOverlay != null && playerMove.playerHealth > 0)
        {
            StartCoroutine(FlashScreen());
        }
    }

    private IEnumerator ShakeCamera()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude),
                originalPosition.z
            );
            cameraTransform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }

    private IEnumerator FlashScreen()
    {
        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            float lerpFactor = Mathf.PingPong(elapsed, flashDuration / 2) / (flashDuration / 2);
            screenOverlay.color = Color.Lerp(hitColor, originalColor, lerpFactor);

            elapsed += Time.deltaTime;
            yield return null;
        }
        screenOverlay.color = originalColor;
    }
}
