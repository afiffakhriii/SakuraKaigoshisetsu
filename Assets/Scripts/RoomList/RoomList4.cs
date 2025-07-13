using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomList4 : MonoBehaviour
{
    [Header("UI Komponen")]
    public Text displayText;
    public float typeSpeed = 0.05f;
    public float delayBetweenTexts = 1.3f;

    private List<string> textList = new List<string>()
    {
        "Demikianlah ruangan yang ada di Sakura Kaigoshisetsu yang kita pelajari untuk saat ini.",
        "Ruangan berikutnya akan kita pelajari di kesempatan berikutnya. Sekarang mari uji pemahamanmu, yuk!"
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string currentFullText = "";

    void Start()
    {
        PlayCurrentText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            SkipTyping();
        }
    }

    void PlayCurrentText()
    {
        currentFullText = textList[currentIndex];
        typingCoroutine = StartCoroutine(TypeText(currentFullText));
    }

    IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        displayText.text = "";

        foreach (char c in fullText)
        {
            displayText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(delayBetweenTexts);

        ContinueToNextText();
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        displayText.text = currentFullText;
        isTyping = false;

        StartCoroutine(SkipDelayAndContinue());
    }

    IEnumerator SkipDelayAndContinue()
    {
        yield return new WaitForSeconds(delayBetweenTexts);
        ContinueToNextText();
    }

    void ContinueToNextText()
    {
        currentIndex++;

        if (currentIndex < textList.Count)
        {
            PlayCurrentText();
        }
        else
        {
            // Semua teks selesai, bisa lanjut ke scene berikutnya atau aktifkan UI lain
            Debug.Log("Selesai semua teks. Tambahkan logika lanjutan di sini jika perlu.");
        }
    }
}
