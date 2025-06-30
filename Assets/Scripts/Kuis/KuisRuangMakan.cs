using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KuisRuangMakan : MonoBehaviour
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

    [Header("Tombol Next (Button UI)")]
    public Button nextButton;

    [Header("Warna Feedback")]
    public Color correctColor = new Color(0f, 1f, 0f, 0.5f); // hijau transparan
    public Color wrongColor = new Color(1f, 0f, 0f, 0.5f);   // merah transparan
    public Color defaultColor = new Color(1f, 1f, 1f, 1f);   // putih solid

    [Header("Nama Scene Berikutnya")]
    public string nextSceneName = "SceneRoomList";

    private bool answered = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        ResetBackgroundColors();

        optionA.onClick.AddListener(() => OnOptionClicked(optionA, false));
        optionB.onClick.AddListener(() => OnOptionClicked(optionB, false));
        optionC.onClick.AddListener(() => OnOptionClicked(optionC, true)); // ✅ Jawaban benar
        optionD.onClick.AddListener(() => OnOptionClicked(optionD, false));

        nextButton.onClick.AddListener(GoToNextScene);
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

        // Tampilkan jawaban benar (C) jika salah
        if (!isCorrect)
        {
            bgC.color = correctColor;
        }

        nextButton.gameObject.SetActive(true);
    }

    void GoToNextScene()
    {
        PlayerPrefs.SetInt("SudahKembaliDariKuis", 1); // ✅ Simpan kondisi kembali
        SceneManager.LoadScene(nextSceneName);         // ✅ Pindah scene
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
