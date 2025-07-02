using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoRMTextSwitcher : MonoBehaviour
{
    [Header("UI Komponen")]
    public RectTransform textContainer;
    public Text mainText;

    [Header("Teks dan Font Size")]
    [TextArea(5, 20)] public string textJapan;
    [TextArea(5, 20)] public string textIndonesia;

    public int fontSizeJapan = 32;
    public int fontSizeIndonesia = 36;

    [Header("Transisi Slide")]
    public float slideDuration = 0.7f;
    public float slideDistance = 1000f;

    [Header("Tombol Navigasi")]
    public Button nextButton;
    public Button backButton;

    [Header("Scene Navigasi")]
    public string nextSceneName = "SceneKuisRuangMakan";
    public string backSceneName = "SceneRuangMakan4";

    private int currentIndex = 0;
    private List<string> texts = new List<string>();
    private List<int> fontSizes = new List<int>();
    private bool isSliding = false;

    void Start()
    {
        texts.Add(textJapan);
        texts.Add(textIndonesia);

        fontSizes.Add(fontSizeJapan);
        fontSizes.Add(fontSizeIndonesia);

        nextButton.onClick.AddListener(SwitchNext);
        backButton.onClick.AddListener(SwitchBack);

        // Mulai dengan teks pertama muncul dari atas
        StartCoroutine(SlideInInitialText());
    }

    IEnumerator SlideInInitialText()
    {
        isSliding = true;

        Vector2 centerPos = textContainer.anchoredPosition;
        Vector2 startPos = centerPos + new Vector2(0, slideDistance);
        textContainer.anchoredPosition = startPos;

        ShowTextInstant(currentIndex);

        float elapsed = 0;
        while (elapsed < slideDuration)
        {
            textContainer.anchoredPosition = Vector2.Lerp(startPos, centerPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textContainer.anchoredPosition = centerPos;
        isSliding = false;
    }

    void ShowTextInstant(int index)
    {
        mainText.fontSize = fontSizes[index];
        mainText.text = texts[index];
    }

    void SwitchNext()
    {
        if (isSliding) return;

        if (currentIndex == texts.Count - 1)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            currentIndex++;
            StartCoroutine(SlideTextUp(currentIndex));
        }
    }

    void SwitchBack()
    {
        if (isSliding) return;

        if (currentIndex == 0)
        {
            SceneManager.LoadScene(backSceneName);
        }
        else
        {
            currentIndex--;
            StartCoroutine(SlideTextUp(currentIndex));
        }
    }

    IEnumerator SlideTextUp(int newIndex)
    {
        isSliding = true;

        Vector2 centerPos = textContainer.anchoredPosition;
        Vector2 endPos = centerPos + new Vector2(0, slideDistance);

        // Slide keluar ke atas
        float elapsed = 0;
        while (elapsed < slideDuration)
        {
            textContainer.anchoredPosition = Vector2.Lerp(centerPos, endPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textContainer.anchoredPosition = endPos;

        // Ganti teks
        ShowTextInstant(newIndex);

        // Masuk kembali dari atas ke tengah
        Vector2 newStart = centerPos + new Vector2(0, slideDistance);
        textContainer.anchoredPosition = newStart;

        elapsed = 0;
        while (elapsed < slideDuration)
        {
            textContainer.anchoredPosition = Vector2.Lerp(newStart, centerPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textContainer.anchoredPosition = centerPos;
        isSliding = false;
    }
}
