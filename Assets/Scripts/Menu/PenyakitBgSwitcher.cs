using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PenyakitBgSwitcher : MonoBehaviour
{
    [Header("Background UI Image")]
    public Image backgroundImage;

    [Header("List Gambar Background")]
    public List<Sprite> backgroundSprites;

    [Header("Tombol Next dan Back")]
    public Button nextButton;
    public Button backButton;

    [Header("Nama Scene Sebelumnya")]
    public string backSceneName = "SceneRuangMakan4";

    private int currentIndex = 0;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(SwitchNext);

        if (backButton != null)
            backButton.onClick.AddListener(SwitchBack);

        // Tampilkan background pertama saat start
        if (backgroundSprites != null && backgroundSprites.Count > 0 && backgroundImage != null)
        {
            currentIndex = 0;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }

        UpdateNextButtonVisibility();
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

        UpdateNextButtonVisibility();
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
            // Kembali ke scene sebelumnya jika sedang di background pertama
            SceneManager.LoadScene(backSceneName);
        }

        UpdateNextButtonVisibility();
    }

    void UpdateNextButtonVisibility()
    {
        // Sembunyikan tombol Next jika sudah di background terakhir
        if (currentIndex >= backgroundSprites.Count - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
    }
}
