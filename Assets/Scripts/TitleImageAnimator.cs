using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIIntroAnimator : MonoBehaviour
{
    [Header("Title Image Slide")]
    public RectTransform titleImage;
    public float slideDuration = 1.2f;
    public float slideDistance = 500f;

    [Header("Image Pop In")]
    public RectTransform[] popImages;
    public float imagePopDelay = 0.3f;
    public float imagePopDuration = 0.5f;

    [Header("Button Pop In")]
    public RectTransform[] popButtons;
    public float buttonPopDelay = 0.2f;
    public float buttonPopDuration = 0.4f;

    private Vector2 titleStartPos;
    private Vector2 titleTargetPos;

    void Start()
    {
        // Siapkan posisi title image dari kiri ke tengah
        titleTargetPos = titleImage.anchoredPosition;
        titleStartPos = titleTargetPos - new Vector2(slideDistance, 0);
        titleImage.anchoredPosition = titleStartPos;

        // Set semua image dan tombol ke skala 0 (belum muncul)
        foreach (var img in popImages)
            img.localScale = Vector3.zero;
        foreach (var btn in popButtons)
            btn.localScale = Vector3.zero;

        // Mulai animasi
        StartCoroutine(PlayIntroAnimation());
    }

    IEnumerator PlayIntroAnimation()
    {
        // Geser title image ke tengah
        yield return StartCoroutine(SmoothMove(titleImage, titleStartPos, titleTargetPos, slideDuration));

        // Tunggu sebentar sebelum animasi elemen lainnya
        yield return new WaitForSeconds(0.3f);

        // Munculkan semua image dengan animasi timbul
        foreach (var img in popImages)
        {
            yield return StartCoroutine(SmoothScale(img, Vector3.zero, Vector3.one, imagePopDuration));
            yield return new WaitForSeconds(imagePopDelay);
        }

        // Munculkan semua tombol dengan animasi timbul
        foreach (var btn in popButtons)
        {
            yield return StartCoroutine(SmoothScale(btn, Vector3.zero, Vector3.one, buttonPopDuration));
            yield return new WaitForSeconds(buttonPopDelay);
        }
    }

    IEnumerator SmoothMove(RectTransform target, Vector2 from, Vector2 to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            target.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }
        target.anchoredPosition = to;
    }

    IEnumerator SmoothScale(RectTransform target, Vector3 from, Vector3 to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            target.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        target.localScale = to;
    }
}
