using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamarMandi2 : MonoBehaviour
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

    [Header("Animasi Gambar Berganti (Looping)")]
    public Image image1;
    public Image image2;
    public float imageSwitchInterval = 0.4f;

    private List<string> textList = new List<string>()
    {
        "Di yokushitsu ada Yuki Obaasan! Sepertinya membutuhkan pertolongan... Kita bantu Yuki Obaasan, yuk!",
        "Sebelum itu, apakah di yokushitsu ada hal yang perlu diperhatikan juga? Mari kita cari tahu."
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

        // Mulai animasi gambar looping
        if (image1 != null && image2 != null)
            imageSwapCoroutine = StartCoroutine(SwapImagesLoop());

        // Mulai animasi teks utama
        PlayCurrentText();

        // Mulai animasi teks "Ketuk layar untuk lanjut..."
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
            nextButton.gameObject.SetActive(true);

            // Hentikan animasi gambar saat semua teks selesai
            if (imageSwapCoroutine != null)
                StopCoroutine(imageSwapCoroutine);

            // Tampilkan gambar tetap (misalnya image1)
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
