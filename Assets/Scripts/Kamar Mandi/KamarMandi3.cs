using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamarMandi3 : MonoBehaviour
{
    [Header("Teks Utama")]
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    [Header("Teks Animasi Looping (misalnya: 'Ketuk untuk lanjut...')")]
    public Text loopingTextUI;
    [TextArea] public string loopingText = "Ketuk layar untuk lanjut...";
    public float loopTypeSpeed = 0.07f;
    public float loopDelay = 1f;

    [Header("Dua Gambar Berganti")]
    public Image image1;
    public Image image2;
    public float imageSwitchInterval = 0.4f;

    private List<string> textList = new List<string>()
    {
        "Catat poin-poin penting dalam taisetsuna memo, ya! Sekarang mari bantu Yuki Obaasan."
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private Coroutine imageSwapCoroutine;
    private string currentFullText = "";

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(OnNextClicked);

        // Mulai animasi gambar
        if (image1 != null && image2 != null)
            imageSwapCoroutine = StartCoroutine(SwapImagesLoop());

        PlayCurrentText();

        // Mulai animasi teks looping
        if (loopingTextUI != null && !string.IsNullOrEmpty(loopingText))
            StartCoroutine(LoopingTypeEffect());
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
            nextButton.gameObject.SetActive(true);

            // Hentikan animasi gambar setelah teks selesai
            if (imageSwapCoroutine != null)
                StopCoroutine(imageSwapCoroutine);

            image1.enabled = true;
            image2.enabled = false;
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
            nextButton.gameObject.SetActive(true);

            if (imageSwapCoroutine != null)
                StopCoroutine(imageSwapCoroutine);

            image1.enabled = true;
            image2.enabled = false;
        }
    }

    void OnNextClicked()
    {
        Debug.Log("Lanjut ke scene berikutnya...");
        // Contoh:
        // SceneManager.LoadScene("ScenePerawatanMakan");
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
