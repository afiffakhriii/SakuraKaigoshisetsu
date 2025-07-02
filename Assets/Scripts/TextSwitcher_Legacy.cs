using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextSwitcher_Legacy : MonoBehaviour
{
    [Header("Komponen UI")]
    public Text displayText;
    public Text nextTextButton;
    public Canvas canvas;

    [Header("Pengaturan Kecepatan")]
    public float typeSpeed = 0.03f;
    public float delayBetweenTexts = 1.0f;

    private List<string> textList = new List<string>()
    {
        "こんにちは！\nさくら かいごしせつ\nへ ようこそ。",
        "はじめまして。\nさくら かいごしせつの かい ごし、あさと もうします\nどうぞ よろしく\nおねがいします。",
        "ここで いっしょに\nたのしく べんきょうして\nいきましょう。",
        "Konnichiwa!\nSelamat datang di Sakura Kaigoshisetsu.",
        "Salam kenal~\nAku Asa, perawat di\nSakura Kaigoshisetsu.\nSenang bisa bekerja sama denganmu~",
        "Mari belajar bersama dengan menyenangkan di sini. Aku akan membantumu memahami tugas sehari-hari seorang kaigoshi.",
        "Yuk, belajar bareng dan jadi perawat yang penuh perhatian dan kasih sayang!",
        "Pertama-tama mari kita lihat ruangan apa saja yang ada di\nSakura Kaigoshisetsu,yuk!",
        "Harap diingat nama-nama ruangan yang ada, ya!"
    };

    private List<int> fontSizeList = new List<int>()
    {
        32, 28, 28, 32, 28, 24, 28, 28, 34
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";
    private Text currentUIText;
    private bool isSkipping = false;

    void Start()
    {
        nextTextButton.gameObject.SetActive(false);
        nextTextButton.text = "";
        StartCoroutine(PlayAllTexts());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                SkipTyping();
            }
        }
    }

    IEnumerator PlayAllTexts()
    {
        while (currentIndex < textList.Count)
        {
            displayText.fontSize = fontSizeList[currentIndex];
            currentFullText = textList[currentIndex];
            currentUIText = displayText;

            // Mulai typing
            isSkipping = false;
            typingCoroutine = StartCoroutine(TypeText(currentUIText, currentFullText));
            yield return typingCoroutine;

            // Jika tidak sedang skip (user tidak klik), tunggu delay lalu lanjut
            if (!isSkipping)
            {
                yield return new WaitForSeconds(delayBetweenTexts);
                currentIndex++;
            }
        }

        // Setelah selesai semua teks
        nextTextButton.gameObject.SetActive(true);
        currentFullText = "Next...";
        currentUIText = nextTextButton;
        typingCoroutine = StartCoroutine(TypeText(nextTextButton, currentFullText));
        yield return typingCoroutine;
    }

    IEnumerator TypeText(Text uiText, string fullText)
    {
        isTyping = true;
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (currentUIText != null)
        {
            currentUIText.text = currentFullText;
        }

        isTyping = false;
        isSkipping = true;
        StartCoroutine(SkipAndContinueAfterDelay());
    }

    IEnumerator SkipAndContinueAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        currentIndex++;
        StartCoroutine(PlayAllTexts()); // lanjutkan loop setelah skip
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("SceneRoomA");
    }
}
