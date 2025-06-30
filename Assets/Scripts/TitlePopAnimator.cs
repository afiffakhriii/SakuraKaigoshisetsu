using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleBubbleAnimator : MonoBehaviour
{
    [Header("Title Images")]
    public Image imageTitle1;
    public Image imageTitle2;

    [Header("Animation Settings")]
    public float moveDistance = 300f;        // Geser dari kiri
    public float popDuration = 0.6f;         // Durasi pop masuk
    public float exitDuration = 0.5f;        // Durasi keluar
    public float stayDuration = 1.5f;        // Lama keduanya stay di tengah
    public int repeatCount = 2;

    [Header("Next Scene")]
    public string nextScene = "SceneMenu";

    void Start()
    {
        StartCoroutine(AnimateSequence());
    }

    IEnumerator AnimateSequence()
    {
        // Awal: pastikan keduanya tidak terlihat
        imageTitle1.gameObject.SetActive(false);
        imageTitle2.gameObject.SetActive(false);

        for (int i = 0; i < repeatCount; i++)
        {
            // Tampilkan title 1 → stay
            yield return StartCoroutine(EnterImage(imageTitle1));

            // Lalu tampilkan title 2 → sekarang keduanya tampil
            yield return StartCoroutine(EnterImage(imageTitle2));

            // Tunggu beberapa saat saat keduanya di tengah
            yield return new WaitForSeconds(stayDuration);

            // Keluarkan keduanya bersamaan
            yield return StartCoroutine(ExitImage(imageTitle1));
            yield return StartCoroutine(ExitImage(imageTitle2));
        }

        // Setelah selesai semua, pindah ke scene berikutnya
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator EnterImage(Image image)
    {
        image.gameObject.SetActive(true);
        RectTransform rect = image.rectTransform;

        Vector2 centerPos = rect.anchoredPosition;
        Vector2 offscreenLeft = centerPos - new Vector2(moveDistance, 0);
        rect.anchoredPosition = offscreenLeft;
        rect.localScale = Vector3.zero;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / popDuration;
            float eased = Mathf.SmoothStep(0, 1, t);
            float scale = Mathf.LerpUnclamped(0f, 1.1f, eased);
            if (eased > 0.7f) scale = Mathf.Lerp(1.1f, 1f, (eased - 0.7f) / 0.3f);

            rect.anchoredPosition = Vector2.Lerp(offscreenLeft, centerPos, eased);
            rect.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }

        rect.anchoredPosition = centerPos;
        rect.localScale = Vector3.one;
    }

    IEnumerator ExitImage(Image image)
    {
        RectTransform rect = image.rectTransform;
        Vector2 centerPos = rect.anchoredPosition;
        Vector2 offscreenRight = centerPos + new Vector2(moveDistance, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / exitDuration;
            float eased = Mathf.SmoothStep(0, 1, t);
            rect.anchoredPosition = Vector2.Lerp(centerPos, offscreenRight, eased);
            rect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, eased);
            yield return null;
        }

        image.gameObject.SetActive(false);
    }
}
