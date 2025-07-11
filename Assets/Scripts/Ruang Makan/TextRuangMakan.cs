using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneRuangMakan : MonoBehaviour
{
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    private List<string> textList = new List<string>()
    {
        "Ini merupakan ruang makan yang biasa digunakan di Sakura Kaigoshisetsu.",
        "Kita lihat kosakata apa saja yang ada di ruangan ini, yuk!",
        "Wah, ada Seiji Ojiisan! Sepertinya sedang lapar... Kita bantu Seiji Ojiisan untuk makan, yuk!",
        "Catat poin-poin penting dalam taisetsuna memo, ya! Sekarang mari bantu Seiji Ojiisan."
    };

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlayAllTexts());
    }

    IEnumerator PlayAllTexts()
    {
        while (currentIndex < textList.Count)
        {
            yield return StartCoroutine(TypeText(textList[currentIndex]));
            yield return new WaitForSeconds(delayBetweenTexts);
            currentIndex++;
        }
    }

    IEnumerator TypeText(string fullText)
    {
        displayText.text = "";
        foreach (char c in fullText)
        {
            displayText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
