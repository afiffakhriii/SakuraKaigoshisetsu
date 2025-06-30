using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextSwitcher_Kuis : MonoBehaviour
{
    public Text displayText;               // Text Kuis
    public Button buttonNext;
    public Button buttonBack;

    private List<string> textList = new List<string>()
    {
        "Apakah kamu mengingat nama-nama ruangan yang ada? Untuk menguji ingatanmu, kita berlatih sejenak, yuk!",
        "Pasangkan gambar ruangan yang ada dengan nama yang benar, ya!"
    };

    private int currentIndex = 0;

    void Start()
    {
        UpdateText();

        buttonNext.onClick.AddListener(NextText);
        buttonBack.onClick.AddListener(PreviousText);
    }

    void UpdateText()
    {
        displayText.text = textList[currentIndex];
        buttonBack.interactable = currentIndex > 0;
    }

    void NextText()
    {
        if (currentIndex < textList.Count - 1)
        {
            currentIndex++;
            UpdateText();
        }
        else
        {
            SceneManager.LoadScene("SceneRoomQuiz");
        }
    }

    void PreviousText()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateText();
        }
    }
}
