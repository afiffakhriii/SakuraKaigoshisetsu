using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextListRoom : MonoBehaviour
{
    [Header("UI Komponen")]
    public Text displayText;           // Text UI legacy untuk tampilkan kalimat
    public Button nextButton;          // Tombol next
    public float typeSpeed = 0.05f;    // Kecepatan efek ketik
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Kita mulai dari ruangan yang pertama...",

        "Selanjutnya ruangan yang kedua~",

        "Selanjutnya ruangan yang ketiga~",

        "Demikianlah ruangan yang ada di Sakura Kaigoshisetsu yang kita pelajari untuk saat ini. ",

        "Ruangan berikutnya akan kita pelajari di kesempatan berikutnya.  Sekarang mari uji pemahamanmu, yuk!"
    };

    private int currentIndex = 0;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(OnNextClicked);
        StartCoroutine(PlayTextSequence());
    }

    IEnumerator PlayTextSequence()
    {
        while (currentIndex < textList.Count)
        {
            yield return StartCoroutine(TypeText(textList[currentIndex]));
            currentIndex++;
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // Setelah semua teks selesai, munculkan tombol
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

    void OnNextClicked()
    {
        // Ganti ke scene kuis, atau lanjut sesuai kebutuhan
        // Contoh:
        // SceneManager.LoadScene("SceneKuisSoal");
    }
}
