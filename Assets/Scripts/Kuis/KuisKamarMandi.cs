using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class KuisKamarMandi : MonoBehaviour
{
    [Header("Teks Narasi (Typing Effect)")]
    public Text typingText;
    [TextArea(2, 5)] public string typingContent = "Yuki Obaasan membutuhkan bantuan di kamar mandi. Yuk bantu dengan menjawab pertanyaan berikut!";
    public float typeSpeed = 0.04f;

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

    [Header("Tombol Next (Button UI)")]
    public Button nextButton;

    [Header("Warna Feedback")]
    public Color correctColor = new Color(0f, 1f, 0f, 0.5f); // hijau transparan
    public Color wrongColor = new Color(1f, 0f, 0f, 0.5f);   // merah transparan
    public Color defaultColor = new Color(1f, 1f, 1f, 1f);   // putih solid

    [Header("Nama Scene Berikutnya")]
    public string nextSceneName = "SceneRoomList3";

    private bool answered = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        ResetBackgroundColors();

        optionA.onClick.AddListener(() => OnOptionClicked(optionA, false));
        optionB.onClick.AddListener(() => OnOptionClicked(optionB, true));  // ✅ Jawaban benar
        optionC.onClick.AddListener(() => OnOptionClicked(optionC, false));
        optionD.onClick.AddListener(() => OnOptionClicked(optionD, false));

        nextButton.onClick.AddListener(GoToNextScene);

        if (typingText != null)
        {
            StartCoroutine(TypeTextEffect());
        }
    }

    IEnumerator TypeTextEffect()
    {
        typingText.text = "";
        foreach (char c in typingContent)
        {
            typingText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void ResetBackgroundColors()
    {
        bgA.color = defaultColor;
        bgB.color = defaultColor;
        bgC.color = defaultColor;
        bgD.color = defaultColor;
    }

    void OnOptionClicked(Button btn, bool isCorrect)
    {
        if (answered) return;
        answered = true;

        Image bg = GetBackgroundForButton(btn);
        bg.color = isCorrect ? correctColor : wrongColor;

        if (!isCorrect)
        {
            bgB.color = correctColor;
        }

        nextButton.gameObject.SetActive(true);
    }

    void GoToNextScene()
    {
        PlayerPrefs.SetInt("SudahKembaliDariKuis", 1);
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
