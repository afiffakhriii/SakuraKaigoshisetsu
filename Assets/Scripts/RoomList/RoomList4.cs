using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomList4 : MonoBehaviour
{
    public Text displayText;
    public Button nextButton;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Demikianlah ruangan yang ada di Sakura Kaigoshisetsu yang kita pelajari untuk saat ini. ",
        "Ruangan berikutnya akan kita pelajari di kesempatan berikutnya. Sekarang mari uji pemahamanmu, yuk!"
    };

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(OnNextClicked);
        StartCoroutine(PlayAllTexts());
    }

    IEnumerator PlayAllTexts()
    {
        yield return StartCoroutine(PlayText(textList[0]));
        yield return new WaitForSeconds(delayBetweenTexts);

        currentIndex++;
        yield return StartCoroutine(PlayText(textList[1]));
        yield return new WaitForSeconds(delayBetweenTexts);

        nextButton.gameObject.SetActive(true);
    }

    IEnumerator PlayText(string fullText)
    {
        isTyping = true;
        displayText.text = "";

        foreach (char c in fullText)
        {
            displayText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    void OnNextClicked()
    {
        PlayerPrefs.SetString("KuisDari", "SceneRoomList4");
        SceneManager.LoadScene("SceneMainKuis");
    }
}