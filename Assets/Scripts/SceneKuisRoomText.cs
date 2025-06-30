using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        35,  // font size untuk index 0
        35,
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
            displayText.fontSize = fontSizeList[currentIndex];

            yield return StartCoroutine(TypeText(textList[currentIndex]));
            currentIndex++;
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // Setelah teks selesai, kamu bisa trigger sesuatu di sini jika perlu.
        // Contoh: SceneManager.LoadScene("SceneKuis") atau aktifkan UI lain.
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
