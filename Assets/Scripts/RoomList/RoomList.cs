using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Kita mulai dari ruangan yang pertama..."
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";

    void Start()
    {
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

        // Jika ingin lanjut ke teks berikutnya otomatis, aktifkan blok ini:
        /*
        if (currentIndex < textList.Count - 1)
        {
            currentIndex++;
            PlayCurrentText();
        }
        else
        {
            // Semua teks selesai
        }
        */
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        displayText.text = currentFullText;
        isTyping = false;
    }
}
