using System.Collections;
using UnityEngine;

public class PriceCanvasCs : MonoBehaviour
{
    public float fadeOutTime = 0.7f;
    public float moveDistance = 20.0f;

    public CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;

        StartCoroutine(FadeOutAndMove());
    }

    IEnumerator FadeOutAndMove()
    {
        float elapsedTime = 0f;
        float initialY = originalPosition.y;

        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);
            canvasGroup.alpha = alpha;

            float newY = Mathf.Lerp(initialY, initialY + moveDistance, elapsedTime / fadeOutTime);
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
