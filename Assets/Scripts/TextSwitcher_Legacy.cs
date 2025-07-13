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
    private bool isForcedJump = false;

    void Start()
    {
        nextTextButton.gameObject.SetActive(true);
        nextTextButton.text = "Next...";
        StartCoroutine(PlayTextAtCurrentIndex());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            SkipTyping();
        }
    }

    IEnumerator PlayTextAtCurrentIndex()
    {
        if (currentIndex >= textList.Count)
        {
            GoToNextScene();
            yield break;
        }

        displayText.fontSize = fontSizeList[currentIndex];
        currentFullText = textList[currentIndex];
        currentUIText = displayText;

        isSkipping = false;
        typingCoroutine = StartCoroutine(TypeText(currentUIText, currentFullText));
        yield return typingCoroutine;

        if (!isSkipping && !isForcedJump)
        {
            yield return new WaitForSeconds(delayBetweenTexts);
            currentIndex++;
        }

        isForcedJump = false;
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
        StartCoroutine(SkipAndContinue());
    }

    IEnumerator SkipAndContinue()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        currentIndex++;
        StartCoroutine(PlayTextAtCurrentIndex());
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("SceneRoomA");
    }

    public void OnEarlyNextClicked()
    {
        if (isTyping)
        {
            SkipTyping();
            return;
        }

        if (currentIndex < 7)
        {
            currentIndex = 7; // Langsung ke teks ke-8
            isForcedJump = true;
            StopAllCoroutines();
            StartCoroutine(PlayTextAtCurrentIndex());
        }
        else if (currentIndex == 7)
        {
            // Setelah teks ke-8 (indeks 7) selesai, langsung ke scene
            GoToNextScene();
        }
        else
        {
            // Lanjutkan normal untuk teks ke-9
            currentIndex++;
            StopAllCoroutines();
            StartCoroutine(PlayTextAtCurrentIndex());
        }
    }
}
