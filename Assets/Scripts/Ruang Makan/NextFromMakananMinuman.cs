using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextToRuangMakan : MonoBehaviour
{
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SceneRuangMakan");
        });
    }
}
