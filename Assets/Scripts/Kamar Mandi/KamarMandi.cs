using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamarMandi : MonoBehaviour
{
    [Header("Teks dan Tombol")]
    public Text displayText;
    public Button nextButton;
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
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(OnNextClicked);

        // Mulai animasi gambar berganti
        imageSwapCoroutine = StartCoroutine(SwapImagesLoop());

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
            nextButton.gameObject.SetActive(true);

            // Stop animasi gambar jika sudah selesai semua teks
            if (imageSwapCoroutine != null)
                StopCoroutine(imageSwapCoroutine);

            // Tampilkan satu gambar secara tetap (misalnya image1)
            image1.enabled = true;
            image2.enabled = false;
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

    void OnNextClicked()
    {
        Debug.Log("Lanjut ke scene atau aksi berikutnya...");
        // Contoh jika ingin pindah scene:
        // SceneManager.LoadScene("SceneMakananMinuman");
    }
}
