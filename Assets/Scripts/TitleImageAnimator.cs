using UnityEngine;
using UnityEngine.UI;

public class LoopingTitleSlideAnimator : MonoBehaviour
{
    public RectTransform titleImage;
    public float moveDuration = 1.5f;
    public float pauseDuration = 0.5f;
    public float moveDistance = 400f; // bisa disesuaikan agar gambar sepenuhnya keluar layar

    private Vector2 centerPos;
    private Vector2 leftPos;
    private Vector2 rightPos;

    void Start()
    {
        centerPos = titleImage.anchoredPosition;
        leftPos = centerPos - new Vector2(moveDistance, 0);
        rightPos = centerPos + new Vector2(moveDistance, 0);

        titleImage.anchoredPosition = leftPos;
        StartCoroutine(SlideLoop());
    }

    System.Collections.IEnumerator SlideLoop()
    {
        while (true)
        {
            // MASUK dari kiri → ke tengah
            yield return StartCoroutine(SmoothMove(titleImage, leftPos, centerPos, moveDuration));

            // Diam sebentar di tengah
            yield return new WaitForSeconds(pauseDuration);

            // KELUAR ke kanan
            yield return StartCoroutine(SmoothMove(titleImage, centerPos, rightPos, moveDuration));

            // Reset ke kiri
            titleImage.anchoredPosition = leftPos;

            // Diam sebelum muncul lagi
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    System.Collections.IEnumerator SmoothMove(RectTransform target, Vector2 start, Vector2 end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            target.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }
        target.anchoredPosition = end; // pastikan posisi akhir tepat
    }
}
