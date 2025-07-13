using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuangMakan3 : MonoBehaviour
{
    [Header("Teks Utama")]
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    [Header("Teks Looping")]
    public Text loopingTextUI;
    [TextArea] public string loopingText;
    public float loopTypeSpeed = 0.1f;
    public float loopDelay = 1f;

    private List<string> textList = new List<string>()
    {
        "Catat poin-poin penting dalam taisetsuna memo, ya! Sekarang mari bantu Shizu Obaasan."
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";

    void Start()
    {
        // Mulai teks utama
        PlayCurrentText();

        // Mulai teks looping
        if (loopingTextUI != null && !string.IsNullOrEmpty(loopingText))
        {
            StartCoroutine(LoopingTypeEffect());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            SkipTyping();
        }
    }

    void PlayCurrentText()
    {
        currentFullText = textList[currentIndex];
        typingCoroutine = StartCoroutine(TypeText(currentFullText));
    }

    IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        displayText.text = "";

        foreach (char c in fullText)
        {
            displayText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(delayBetweenTexts);
        ContinueToNextText();
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        displayText.text = currentFullText;
        isTyping = false;

        StartCoroutine(SkipDelayAndContinue());
    }

    IEnumerator SkipDelayAndContinue()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        ContinueToNextText();
    }

    void ContinueToNextText()
    {
        currentIndex++;

        if (currentIndex < textList.Count)
        {
            PlayCurrentText();
        }
        else
        {
            Debug.Log("Semua teks selesai. Tambahkan logika lanjut di sini jika diperlukan.");
            // Contoh: SceneManager.LoadScene("ScenePerawatanMakan");
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
}
