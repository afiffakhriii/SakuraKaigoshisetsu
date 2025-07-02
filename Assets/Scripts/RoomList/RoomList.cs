using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Kita mulai dari ruangan yang pertama...",
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(ShowNextText);
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
        nextButton.gameObject.SetActive(true);
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        displayText.text = currentFullText;
        isTyping = false;

        StartCoroutine(ShowNextButtonAfterDelay());
    }

    IEnumerator ShowNextButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        nextButton.gameObject.SetActive(true);
    }

    void ShowNextText()
    {
        nextButton.gameObject.SetActive(false);

        if (currentIndex < textList.Count - 1)
        {
            currentIndex++;
            PlayCurrentText();
        }
        else
        {
            // Semua teks selesai. Tambahkan logika pindah scene atau set active objek lain jika diperlukan
        }
    }
}
