using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuangMakan2 : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    private List<string> textList = new List<string>()
    {
        "Wah, ada Seiji Ojiisan! Sepertinya sedang lapar... Kita bantu Seiji Ojiisan untuk makan, yuk!",
        "Sebelum itu, ada hal yang perlu diperhatikan dalam perawatan lansia ketika makan, ada apa saja, ya...?"
    };

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(ShowNextText);
        StartCoroutine(PlayAllTexts());
    }

    void ShowNextText()
    {
        // Lanjutkan aksi setelah teks 1 & 2 selesai dan tombol next ditekan
        Debug.Log("Lanjut ke scene berikutnya...");
        // Contoh: SceneManager.LoadScene("ScenePerawatanMakan");
    }

    IEnumerator PlayAllTexts()
    {
        while (currentIndex < textList.Count)
        {
            yield return StartCoroutine(PlayText(textList[currentIndex]));
            yield return new WaitForSeconds(delayBetweenTexts);
            currentIndex++;
        }

        // Setelah semua teks selesai, baru tampilkan tombol next
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
