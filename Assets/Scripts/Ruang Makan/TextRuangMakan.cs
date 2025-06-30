using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextRuangMakan : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public GameObject imgTextNenek;

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
    private int stage = 0; // 0 = awal, 1 = balik dari MakananMinuman, 2 = balik dari PerawatanMakan

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(HandleNextClick);
        imgTextNenek.SetActive(false);

        // Cek stage berdasarkan PlayerPrefs
        stage = PlayerPrefs.GetInt("ruangMakanStage", 0);

        if (stage == 0)
        {
            StartCoroutine(PlayInitialTexts()); // teks 1 & 2
        }
        else if (stage == 1)
        {
            currentIndex = 2;
            StartCoroutine(PlayText3WithNenek()); // teks 3
        }
        else if (stage == 2)
        {
            currentIndex = 3;
            StartCoroutine(PlayFinalTextWithNenek()); // teks 4
        }
    }

    IEnumerator PlayInitialTexts()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(TypeText(textList[i]));
            yield return new WaitForSeconds(delayBetweenTexts);
            currentIndex++;
        }

        nextButton.gameObject.SetActive(true);
    }

    IEnumerator PlayText3WithNenek()
    {
        imgTextNenek.SetActive(true);
        yield return StartCoroutine(TypeText(textList[currentIndex]));
        yield return new WaitForSeconds(delayBetweenTexts);
        nextButton.gameObject.SetActive(true);
    }

    IEnumerator PlayFinalTextWithNenek()
    {
        imgTextNenek.SetActive(true);
        yield return StartCoroutine(TypeText(textList[currentIndex]));
        yield return new WaitForSeconds(delayBetweenTexts);
        nextButton.gameObject.SetActive(true);
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

    void HandleNextClick()
    {
        if (stage == 0)
        {
            PlayerPrefs.SetInt("ruangMakanStage", 1);
            SceneManager.LoadScene("SceneMakananMinuman");
        }
        else if (stage == 1)
        {
            PlayerPrefs.SetInt("ruangMakanStage", 2);
            SceneManager.LoadScene("ScenePerawatanMakan");
        }
        else if (stage == 2)
        {
            PlayerPrefs.DeleteKey("ruangMakanStage");
            SceneManager.LoadScene("SceneRuangMakan2");
        }
    }
}
