using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuangKesehatan2 : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.2f;

    private List<string> textList = new List<string>()
    {
        "Hari ini merupakan jadwal Haruko Obaasan untuk pemeriksaan kesehatan. Kita bantu, yuk!",
        "Sebelum itu, ada hal yang perlu diperhatikan dalam perawatan lansia ketika membersihkan tubuh, ada apa saja, ya...?"
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
