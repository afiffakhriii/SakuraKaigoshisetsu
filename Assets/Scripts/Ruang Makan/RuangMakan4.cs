using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RuangMakan4 : MonoBehaviour
{
    [Header("Teks Looping (Efek Ketik)")]
    public Text loopingTextUI;
    [TextArea] public string loopingText;
    public float loopTypeSpeed = 0.1f;
    public float loopDelay = 1f;

    [Header("Teks Bernapas (Fade In / Out)")]
    public Text breathingTextUI;
    public float breathingSpeed = 1.5f;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;

    void Start()
    {
        // Mulai animasi teks ketik looping
        if (loopingTextUI != null && !string.IsNullOrEmpty(loopingText))
        {
            StartCoroutine(LoopingTypeEffect());
        }

        // Mulai animasi bernapas
        if (breathingTextUI != null)
        {
            StartCoroutine(BreathingTextEffect());
        }
    }

    IEnumerator LoopingTypeEffect()
    {
        while (true)
        {
            loopingTextUI.text = "";
            foreach (char c in loopingText)
            {
                loopingTextUI.text += c;
                yield return new WaitForSeconds(loopTypeSpeed);
            }
            yield return new WaitForSeconds(loopDelay);
        }
    }

    IEnumerator BreathingTextEffect()
    {
        Color originalColor = breathingTextUI.color;
        float alpha = minAlpha;
        bool increasing = true;

        while (true)
        {
            alpha += (increasing ? 1 : -1) * Time.deltaTime / breathingSpeed;
            alpha = Mathf.Clamp(alpha, minAlpha, maxAlpha);

            breathingTextUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (alpha >= maxAlpha)
                increasing = false;
            else if (alpha <= minAlpha)
                increasing = true;

            yield return null;
        }
    }
}
