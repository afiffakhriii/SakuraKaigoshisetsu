using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KuisRoomBController : MonoBehaviour
{
    [Header("Opsi Jawaban (Drag Button A–D ke sini)")]
    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;

    [Header("Background Opsi Jawaban (anak dari tombol)")]
    public Image bgA;
    public Image bgB;
    public Image bgC;
    public Image bgD;

    [Header("Teks Penjelasan")]
    public GameObject textA;
    public GameObject textB;

    [Header("Tombol Next (gunakan UI Button, bukan Text)")]
    public Button nextButton;

    [Header("Text di dalam tombol Next")]
    public Text nextButtonText;

    [Header("Warna Feedback")]
    public Color correctColor = new Color(0f, 1f, 0f, 0.5f);
    public Color wrongColor = new Color(1f, 0f, 0f, 0.5f);
    public Color defaultColor = new Color(1f, 1f, 1f, 1f);

    [Header("Nama Scene Berikutnya")]
    public string nextSceneName = "SceneKuisRoomC";

    [Header("Animasi Ketik")]
    public float typeSpeed = 0.03f;

    private int clickCount = 0;
    private HashSet<Button> clickedButtons = new HashSet<Button>();
    private HashSet<Button> correctAnswers;

    void Start()
    {
        correctAnswers = new HashSet<Button> { optionA, optionC };

        correctColor.a = 0.5f;
        wrongColor.a = 0.5f;
        defaultColor.a = 1f;

        textA.SetActive(false);
        textB.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButtonText.text = "";

        ResetBackgroundColors();

        optionA.onClick.AddListener(() => OnOptionClicked(optionA));
        optionB.onClick.AddListener(() => OnOptionClicked(optionB));
        optionC.onClick.AddListener(() => OnOptionClicked(optionC));
        optionD.onClick.AddListener(() => OnOptionClicked(optionD));

        nextButton.onClick.AddListener(GoToNextScene);
    }

    void ResetBackgroundColors()
    {
        bgA.color = defaultColor;
        bgB.color = defaultColor;
        bgC.color = defaultColor;
        bgD.color = defaultColor;
    }

    void OnOptionClicked(Button btn)
    {
        if (clickCount >= 2 || clickedButtons.Contains(btn))
            return;

        clickedButtons.Add(btn);
        clickCount++;

        Image bg = GetBackgroundForButton(btn);

        if (correctAnswers.Contains(btn))
        {
            bg.color = correctColor;
        }
        else
        {
            bg.color = wrongColor;
        }

        if (clickCount == 2)
        {
            ShowFinalFeedback();
        }
    }

    void ShowFinalFeedback()
    {
        // Warnai jawaban benar yang belum diklik
        foreach (Button btn in correctAnswers)
        {
            if (!clickedButtons.Contains(btn))
            {
                GetBackgroundForButton(btn).color = correctColor;
            }
        }

        // Tandai jawaban salah yang diklik
        foreach (Button btn in clickedButtons)
        {
            if (!correctAnswers.Contains(btn))
            {
                GetBackgroundForButton(btn).color = wrongColor;
            }
        }

        textA.SetActive(true);
        textB.SetActive(true);

        StartCoroutine(ShowNextButtonWithTyping());
    }

    IEnumerator ShowNextButtonWithTyping()
    {
        nextButton.gameObject.SetActive(true);

        string fullText = "Next...";
        nextButtonText.text = "";

        foreach (char c in fullText)
        {
            nextButtonText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    Image GetBackgroundForButton(Button btn)
    {
        if (btn == optionA) return bgA;
        if (btn == optionB) return bgB;
        if (btn == optionC) return bgC;
        if (btn == optionD) return bgD;
        return null;
    }
}
