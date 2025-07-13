using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamarKesehatan3 : MonoBehaviour
{
    [Header("Teks Utama")]
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    [Header("Teks Animasi Looping (misalnya: 'Ketuk untuk lanjut...')")]
    public Text loopingTextUI;
    [TextArea] public string loopingText = "Ketuk layar untuk lanjut...";
    public float loopTypeSpeed = 0.07f;
    public float loopDelay = 1f;

    private List<string> textList = new List<string>()
    {
        "Jangan lupa dicatat, ya! Yuk kita bantu Haruko Obaasan."
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";

    void Start()
    {
        PlayCurrentText();

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

        currentIndex++;
        if (currentIndex < textList.Count)
        {
            PlayCurrentText();
        }
        else
        {
            Debug.Log("Semua teks selesai. Tambahkan transisi scene di sini jika perlu.");
            // Contoh: SceneManager.LoadScene("ScenePerawatanMakan");
        }
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        displayText.text = currentFullText;
        isTyping = false;

        StartCoroutine(SkipDelayThenContinue());
    }

    IEnumerator SkipDelayThenContinue()
    {
        yield return new WaitForSeconds(delayBetweenTexts);

        currentIndex++;
        if (currentIndex < textList.Count)
        {
            PlayCurrentText();
        }
        else
        {
            Debug.Log("Semua teks selesai (dari skip). Tambahkan aksi jika perlu.");
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
