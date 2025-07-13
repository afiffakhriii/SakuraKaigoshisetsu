using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamarMandi : MonoBehaviour
{
    [Header("Teks dan Tampilan")]
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    [Header("Animasi Gambar Bergantian")]
    public Image image1;
    public Image image2;
    public float imageSwitchInterval = 0.4f;

    private List<string> textList = new List<string>()
    {
        "Ini merupakan ruang untuk kebersihan tubuh yang biasa digunakan di Sakura Kaigoshisetsu.",
        "Kita lihat kosakata apa saja yang ada di ruangan ini, yuk!"
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private Coroutine imageSwapCoroutine;
    private string currentFullText = "";

    void Start()
    {
        // Mulai animasi gambar berganti
        imageSwapCoroutine = StartCoroutine(SwapImagesLoop());

        // Mulai teks pertama
        PlayCurrentText();
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
        ContinueOrFinish();
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
        ContinueOrFinish();
    }

    void ContinueOrFinish()
    {
        currentIndex++;

        if (currentIndex < textList.Count)
        {
            PlayCurrentText();
        }
        else
        {
            // Stop animasi gambar jika sudah selesai semua teks
            if (imageSwapCoroutine != null)
                StopCoroutine(imageSwapCoroutine);

            // Tampilkan satu gambar tetap
            image1.enabled = true;
            image2.enabled = false;

            Debug.Log("Semua teks selesai. Tambahkan aksi berikutnya di sini jika perlu.");
        }
    }

    IEnumerator SwapImagesLoop()
    {
        while (true)
        {
            image1.enabled = true;
            image2.enabled = false;
            yield return new WaitForSeconds(imageSwitchInterval);

            image1.enabled = false;
            image2.enabled = true;
            yield return new WaitForSeconds(imageSwitchInterval);
        }
    }
}
