using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuangMakan3 : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    private List<string> textList = new List<string>()
    {
        "Catat poin-poin penting dalam taisetsuna memo, ya! Sekarang mari bantu Seiji Ojiisan."
    };

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(ShowNextText);
        StartCoroutine(PlayText(textList[currentIndex]));
    }

    void ShowNextText()
    {
        if (!isTyping && currentIndex < textList.Count - 1)
        {
            currentIndex++;
            nextButton.gameObject.SetActive(false);
            StartCoroutine(PlayText(textList[currentIndex]));
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            // Jika sudah teks terakhir, bisa tambah logic lain di sini.
        }
    }

    IEnumerator PlayText(string fullText)
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
}
