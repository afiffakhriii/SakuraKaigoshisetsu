using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwitcher : MonoBehaviour
{
    [Header("Background UI Image")]
    public Image backgroundImage;

    [Header("List Gambar Background")]
    public List<Sprite> backgroundSprites;

    [Header("Tombol Next")]
    public Button nextButton;

    private int currentIndex = 0;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(SwitchBackground);

        // Set background awal
        if (backgroundSprites != null && backgroundSprites.Count > 0 && backgroundImage != null)
        {
            backgroundImage.sprite = backgroundSprites[0];
        }
    }

    void SwitchBackground()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImage == null)
            return;

        currentIndex++;

        if (currentIndex >= backgroundSprites.Count)
            currentIndex = backgroundSprites.Count - 1; // atau bisa dibuat loop: currentIndex = 0;

        backgroundImage.sprite = backgroundSprites[currentIndex];
    }
}
