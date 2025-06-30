using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneKuisTextAnimator2 : MonoBehaviour
{
    [Header("UI Komponen")]
    public Text displayText;           // Text UI legacy untuk tampilkan kalimat
    public Button nextButton;          // Tombol next
    public float typeSpeed = 0.03f;    // Kecepatan efek ketik
    public float delayBetweenTexts = 1f;

    private List<string> textList = new List<string>()
    {
        "Apakah jawabanmu benar? Selanjutnya mari lihat aktivitas apa saja yang ada di Sakura Kaigoshisetsu."
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
        // ✅ Set flag bahwa kita datang dari SceneTransisiKuisRoom2
        PlayerPrefs.SetInt("DariSceneTransisiKuisRoom2", 1);

        // ✅ Pindah ke SceneRoomList
        SceneManager.LoadScene("SceneRoomList");
    }
}
