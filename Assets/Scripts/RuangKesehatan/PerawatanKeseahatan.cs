using System.Collections;
using UnityEngine;

public class PerawatanKeseahatan : MonoBehaviour
{
    [Header("Panel yang Akan Muncul dengan Animasi")]
    public RectTransform panel1;
    public RectTransform panel2;

    [Header("Pengaturan Animasi")]
    public float animationDuration = 0.5f;
    public float delayBeforeStart = 0.2f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Start()
    {
        // Set scale awal ke 0 (tidak terlihat)
        panel1.localScale = Vector3.zero;
        panel2.localScale = Vector3.zero;

        // Mulai animasi keduanya secara bersamaan
        StartCoroutine(AnimatePanels());
    }

    IEnumerator AnimatePanels()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        StartCoroutine(ScaleIn(panel1));
        StartCoroutine(ScaleIn(panel2));
    }

    IEnumerator ScaleIn(RectTransform panel)
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;
            float scaleValue = scaleCurve.Evaluate(t);
            panel.localScale = Vector3.LerpUnclamped(startScale, endScale, scaleValue);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.localScale = endScale;
    }
}
