using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuangMakan : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    private List<string> textList = new List<string>()
    {
        "Ini merupakan ruang makan yang biasa digunakan di Sakura Kaigoshisetsu.",
        "Kita lihat kosakata apa saja yang ada di ruangan ini, yuk!"
    };

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(ShowNextText);
        StartCoroutine(PlayAllInitialTexts());
    }

    void ShowNextText()
    {
        nextButton.gameObject.SetActive(false);
        // Aksi setelah teks 1 & 2 selesai dan tombol next ditekan
        Debug.Log("Lanjut ke scene atau aksi berikutnya...");
        // Contoh jika ingin pindah scene:
        // SceneManager.LoadScene("SceneMakananMinuman");
    }

    IEnumerator PlayAllInitialTexts()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            yield return StartCoroutine(PlayText(textList[i]));
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // Setelah kedua teks selesai, tampilkan tombol Next
        nextButton.gameObject.SetActive(true);
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
    }
}
