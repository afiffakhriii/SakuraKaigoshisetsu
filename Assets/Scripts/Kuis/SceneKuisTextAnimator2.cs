using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneKuisTextAnimator2 : MonoBehaviour
{
    [Header("UI Komponen")]
    public Text displayText;
    public float typeSpeed = 0.03f;
    public float delayBetweenTexts = 1f;

    private List<string> textList = new List<string>()
    {
        "Apakah jawabanmu benar? Selanjutnya mari lihat aktivitas apa saja yang ada di Sakura Kaigoshisetsu."
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";
    private bool isSkipping = false;

    void Start()
    {
        StartCoroutine(PlayTextSequence());
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

    IEnumerator PlayTextSequence()
    {
        while (currentIndex < textList.Count)
        {
            currentFullText = textList[currentIndex];
            isSkipping = false;

            typingCoroutine = StartCoroutine(TypeText(currentFullText));
            yield return typingCoroutine;

            if (!isSkipping)
            {
                yield return new WaitForSeconds(delayBetweenTexts);
                currentIndex++;
            }
        }

        // Setelah semua teks ditampilkan, lanjut ke scene berikutnya
        PlayerPrefs.SetInt("DariSceneTransisiKuisRoom2", 1);
        SceneManager.LoadScene("SceneRoomList");
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
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        displayText.text = currentFullText;
        isTyping = false;
        isSkipping = true;

        StartCoroutine(SkipAndContinueAfterDelay());
    }

    IEnumerator SkipAndContinueAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        currentIndex++;
        StartCoroutine(PlayTextSequence());
    }
}
