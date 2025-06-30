using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundSwitcher : MonoBehaviour
{
    [Header("Background UI Image")]
    public Image backgroundImage;

    [Header("List Gambar Background")]
    public List<Sprite> backgroundSprites;

    [Header("Tombol Next dan Back")]
    public Button nextButton;
    public Button backButton;

    [Header("Scene yang Dipanggil Setelah BG Terakhir")]
    public string nextSceneName = "SceneKuisRuangMakan";
    public string backSceneName = "SceneRuangMakan2";

    private int currentIndex = 0;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(SwitchNext);

        if (backButton != null)
            backButton.onClick.AddListener(SwitchBack);

        // Tampilkan background pertama
        if (backgroundSprites != null && backgroundSprites.Count > 0 && backgroundImage != null)
        {
            currentIndex = 0;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }
    }

    void SwitchNext()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImage == null)
            return;

        if (currentIndex < backgroundSprites.Count - 1)
        {
            currentIndex++;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }
        else
        {
            // Sudah di background terakhir, pindah ke scene kuis
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void SwitchBack()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImage == null)
            return;

        if (currentIndex > 0)
        {
            currentIndex--;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }
        else
        {
            // Jika sudah di background pertama, kembali ke Scene sebelumnya
            SceneManager.LoadScene(backSceneName);
        }
    }
}
