using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextListRoom : MonoBehaviour
{
    [Header("UI Komponen")]
    public Text displayText;
    public Button nextButton;
    public Button backButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Kita mulai dari ruangan yang pertama...",
        "Selanjutnya ruangan yang kedua~",
        "Selanjutnya ruangan yang ketiga~",
        "Demikianlah ruangan yang ada di Sakura Kaigoshisetsu yang kita pelajari untuk saat ini.",
        "Ruangan berikutnya akan kita pelajari di kesempatan berikutnya. Sekarang mari uji pemahamanmu, yuk!"
    };

    private int currentIndex = 0;
    private bool dariTransisi = false;
    private bool kembaliDariKuis = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);

        nextButton.onClick.AddListener(OnNextClicked);
        backButton.onClick.AddListener(OnBackClicked);

        // ✅ Cek apakah kembali dari SceneRuangMakan (perlu reset ke awal)
        if (PlayerPrefs.GetInt("DariSceneRuangMakan", 0) == 1)
        {
            currentIndex = 0;
            PlayerPrefs.SetInt("DariSceneRuangMakan", 0); // reset flag
        }
        else if (PlayerPrefs.GetInt("DariSceneTransisiKuisRoom2", 0) == 1)
        {
            dariTransisi = true;
            currentIndex = 0;
            PlayerPrefs.SetInt("DariSceneTransisiKuisRoom2", 0);
        }
        else if (PlayerPrefs.GetInt("SudahKembaliDariKuis", 0) == 1)
        {
            kembaliDariKuis = true;
            currentIndex = 1;
        }

        StartCoroutine(PlayTextSequence());
    }

    IEnumerator PlayTextSequence()
    {
        if (currentIndex < textList.Count)
        {
            displayText.text = "";
            yield return StartCoroutine(TypeText(textList[currentIndex]));
            yield return new WaitForSeconds(delayBetweenTexts);
            nextButton.gameObject.SetActive(true);
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

    void OnNextClicked()
    {
        nextButton.gameObject.SetActive(false);

        if (currentIndex == 0)
        {
            SceneManager.LoadScene("SceneRuangMakan");
        }
        else if (currentIndex == 1)
        {
            SceneManager.LoadScene("SceneKamarMandi");
        }
        else
        {
            currentIndex++;
            StartCoroutine(PlayTextSequence());
        }
    }

    void OnBackClicked()
    {
        if (PlayerPrefs.GetInt("DariSceneRuangMakan", 0) == 1)
        {
            SceneManager.LoadScene("SceneRuangMakan");
        }
        else if (dariTransisi)
        {
            SceneManager.LoadScene("SceneTransisiKuisRoom2");
        }
        else if (kembaliDariKuis)
        {
            SceneManager.LoadScene("SceneKuisRuangMakan");
        }
        else
        {
            Debug.Log("Tidak ada asal sebelumnya, kembali ke MainMenu (opsional)");
            // SceneManager.LoadScene("MainMenu");
        }
    }
}
