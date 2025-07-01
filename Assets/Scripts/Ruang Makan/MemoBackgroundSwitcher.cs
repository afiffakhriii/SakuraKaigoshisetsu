using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoBackgroundSwitcher : MonoBehaviour
{
    [Header("Background UI Image")]
    public Image backgroundImage;

    [Header("List Gambar Background")]
    public List<Sprite> backgroundSprites;

    [Header("Tombol Next dan Back")]
    public Button nextButton;
    public Button backButton;

    [Header("Nama Scene Berikutnya dan Sebelumnya")]
    public string nextSceneName = "SceneKuisRuangMakan";
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
    }

    void SwitchNext()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImage == null)
            return;

        if (currentIndex == 0)
        {
            currentIndex = 1;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }
        else if (currentIndex == 1)
        {
            // Jika sudah di background ke-2, pindah ke scene kuis
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void SwitchBack()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImage == null)
            return;

        if (currentIndex == 1)
        {
            currentIndex = 0;
            backgroundImage.sprite = backgroundSprites[currentIndex];
        }
        else if (currentIndex == 0)
        {
            // Kembali ke scene sebelumnya jika sedang di background pertama
            SceneManager.LoadScene(backSceneName);
        }
    }
}
