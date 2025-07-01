using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainKuis : MonoBehaviour
{
    [Header("Gambar Soal")]
    public Image questionImage;
    public Sprite[] questionSprites;

    [Header("Teks Opsi Jawaban")]
    public Text textA;
    public Text textB;
    public Text textC;
    public Text textD;

    [Header("Button Opsi Jawaban")]
    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;

    [Header("Background Warna Opsi")]
    public Image bgA;
    public Image bgB;
    public Image bgC;
    public Image bgD;

    [Header("Tombol Navigasi Soal")]
    public Button nextButton;
    public Button backButton;

    [Header("Warna Feedback")]
    public Color correctColor = new Color(0f, 1f, 0f, 0.5f);
    public Color wrongColor = new Color(1f, 0f, 0f, 0.5f);
    public Color defaultColor = new Color(1f, 1f, 1f, 1f);

    private int currentQuestion = 0;
    private string asalScene = "";

    private string[,] options = new string[8, 4]
    {
        {"しんさつしつ", "へや", "いま", "よくしつ"},
        {"しんさつしつ", "へや", "いま", "よくしつ"},
        {"Memberikan makanan yang disuka", "Memastikan preferensi makanan", "Menjaga suhu makanan", "Memperhatikan pantangan makanan"},
        {"はみがき", "せんぱつ", "しゅよく", "せんがん"},
        {"シャワーよく", "にゅうよく", "よくそうよく", "せいしき"},
        {"にゅうよく", "てよく", "しっぷ", "そくよく"},
        {"せきどめ", "かぜぐすり", "ねつぐすり", "めぐすり"},
        {"Obat yang diminum", "Obat semprot", "Obat suntik", "Obat oles / salep"}
    };

    private char[] correctAnswers = new char[8] { 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D' };

    void Start()
    {
        asalScene = PlayerPrefs.GetString("KuisDari", "SceneMenu");

        if (PlayerPrefs.GetInt("KembaliLanjut", 0) == 0)
        {
            PlayerPrefs.DeleteAll();
        }

        currentQuestion = PlayerPrefs.GetInt("LastSoal", 0);
        PlayerPrefs.SetInt("KembaliLanjut", 0);

        LoadQuestion();

        optionA.onClick.AddListener(() => CheckAnswer('A'));
        optionB.onClick.AddListener(() => CheckAnswer('B'));
        optionC.onClick.AddListener(() => CheckAnswer('C'));
        optionD.onClick.AddListener(() => CheckAnswer('D'));

        if (nextButton != null)
            nextButton.onClick.AddListener(NextQuestion);

        if (backButton != null)
            backButton.onClick.AddListener(PreviousQuestion);
    }

    void LoadQuestion()
    {
        ResetOptionUI();

        if (currentQuestion < 0 || currentQuestion >= questionSprites.Length)
            return;

        questionImage.sprite = questionSprites[currentQuestion];
        textA.text = "A. " + options[currentQuestion, 0];
        textB.text = "B. " + options[currentQuestion, 1];
        textC.text = "C. " + options[currentQuestion, 2];
        textD.text = "D. " + options[currentQuestion, 3];

        string key = "SoalDone" + currentQuestion;
        if (PlayerPrefs.HasKey(key))
        {
            char chosen = PlayerPrefs.GetString(key)[0];
            HighlightAnswer(chosen);
            DisableAllOptions();
        }
        else
        {
            EnableAllOptions();
        }
    }

    void CheckAnswer(char chosenOption)
    {
        string key = "SoalDone" + currentQuestion;
        if (PlayerPrefs.HasKey(key)) return;

        PlayerPrefs.SetString(key, chosenOption.ToString());
        PlayerPrefs.SetInt("LastSoal", currentQuestion);

        char correct = correctAnswers[currentQuestion];

        if (chosenOption == correct)
        {
            bgFromChar(chosenOption).color = correctColor;
            PlayerPrefs.SetInt("KembaliLanjut", 1);
            SceneManager.LoadScene("SceneBenar");
        }
        else
        {
            bgFromChar(chosenOption).color = wrongColor;
            bgFromChar(correct).color = correctColor;
            PlayerPrefs.SetInt("KembaliLanjut", 1);
            SceneManager.LoadScene("SceneSalah");
        }
    }

    void NextQuestion()
    {
        if (currentQuestion < questionSprites.Length - 1)
        {
            currentQuestion++;
            LoadQuestion();
        }
        else
        {
            // Jika soal terakhir, lanjut ke SceneLast
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("SceneLast");
        }
    }

    void PreviousQuestion()
    {
        if (currentQuestion > 0)
        {
            currentQuestion--;
            LoadQuestion();
        }
        else
        {
            PlayerPrefs.DeleteAll();
            if (asalScene == "SceneRoomList4")
                SceneManager.LoadScene("SceneRoomList4");
            else
                SceneManager.LoadScene("SceneMenu");
        }
    }

    void ResetOptionUI()
    {
        bgA.color = defaultColor;
        bgB.color = defaultColor;
        bgC.color = defaultColor;
        bgD.color = defaultColor;
    }

    void DisableAllOptions()
    {
        optionA.interactable = false;
        optionB.interactable = false;
        optionC.interactable = false;
        optionD.interactable = false;
    }

    void EnableAllOptions()
    {
        optionA.interactable = true;
        optionB.interactable = true;
        optionC.interactable = true;
        optionD.interactable = true;
    }

    void HighlightAnswer(char chosen)
    {
        char correct = correctAnswers[currentQuestion];
        if (chosen == correct)
        {
            bgFromChar(chosen).color = correctColor;
        }
        else
        {
            bgFromChar(chosen).color = wrongColor;
            bgFromChar(correct).color = correctColor;
        }
    }

    Image bgFromChar(char opt)
    {
        switch (opt)
        {
            case 'A': return bgA;
            case 'B': return bgB;
            case 'C': return bgC;
            case 'D': return bgD;
            default: return null;
        }
    }
}
