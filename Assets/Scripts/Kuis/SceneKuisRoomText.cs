using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneKuisRoomText : MonoBehaviour
{
    public Text displayText;              // UI Text utama
    public float typeSpeed = 0.03f;
    public float delayBetweenTexts = 1f;

    private List<string> textList = new List<string>()
    {
        "Pasangkan gambar ruangan yang ada dengan nama yang benar, yuk!",
        "Jangan lupa nama-nama ruangan yang ada, ya~"
    };

    private List<int> fontSizeList = new List<int>()
    {
        35, 35
    };

    private int currentIndex = 0;

    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";
    private bool isSkipping = false;

    void Start()
    {
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

            isSkipping = false;
            typingCoroutine = StartCoroutine(TypeText(currentFullText));
            yield return typingCoroutine;

            if (!isSkipping)
            {
                yield return new WaitForSeconds(delayBetweenTexts);
                currentIndex++;
            }
        }

        // Setelah semua teks selesai, kamu bisa trigger sesuatu di sini.
        // Contoh: SceneManager.LoadScene("SceneKuis");
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
        StartCoroutine(PlayAllTexts());
    }
}
