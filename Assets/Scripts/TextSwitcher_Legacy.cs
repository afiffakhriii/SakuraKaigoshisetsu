using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextSwitcher_Legacy : MonoBehaviour
{
    public Text displayText;              // UI Text utama
    public Text nextTextButton;           // UI Text "Next »"
    public float typeSpeed = 0.03f;
    public float delayBetweenTexts = 1.5f;

    private List<string> textList = new List<string>()
    {
        "こんにちは！    \nさくら かいごしせつ     \nへ ようこそ。",

        "はじめまして。        \nさくら かいごしせつの かい ごし、あさと もうします。 \nどうぞ よろしく         \nおねがいします。",

        "ここで いっしょに         \nたのしく べんきょうして         \nいきましょう。",

        "Konnichiwa!          \nSelamat datang di Sakura Kaigoshisetsu.",

        "Salam kenal~ Aku Asa, perawat di\nSakura Kaigoshisetsu.     \nSenang bisa bekerja sama  denganmu~",

        "Mari belajar bersama dengan menyenangkan di sini. Aku akan membantumu memahami tugas    sehari-hari seorang kaigoshi.",

        "Yuk, belajar bareng dan jadi perawat yang penuh perhatian dan kasih sayang!",

        "Pertama-tama mari kita lihat ruangan apa saja yang ada di\nSakura Kaigoshisetsu,\nyuk!",

        "Harap diingat nama-nama ruangan yang ada, ya!"
    };

    private List<int> fontSizeList = new List<int>()
    {
        34,  // index 0
        28,  // index 1
        28,  // index 2
        34,  // index 3
        28,  // index 4
        24,  // index 5
        28,  // index 6
        28,  // index 7
        34   // index 8
    };

    private int currentIndex = 0;

    void Start()
    {
        nextTextButton.gameObject.SetActive(false);
        StartCoroutine(PlayAllTexts());
    }

    IEnumerator PlayAllTexts()
    {
        while (currentIndex < textList.Count)
        {
            displayText.fontSize = fontSizeList[currentIndex];

            yield return StartCoroutine(TypeText(textList[currentIndex]));
            currentIndex++;
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        nextTextButton.gameObject.SetActive(true);
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

    public void GoToNextScene()
    {
        SceneManager.LoadScene("SceneRoomA");
    }
}
