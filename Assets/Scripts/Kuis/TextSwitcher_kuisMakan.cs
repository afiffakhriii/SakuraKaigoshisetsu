using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher_kuisMakan : MonoBehaviour
{
    public Text displayText;
    public float typeSpeed = 0.05f;

    private List<string> textList = new List<string>()
    {
        "Isilah bagian rumpang pada tanda kurung di atas!"
    };

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(TypeText(textList[currentIndex]));
    }

    IEnumerator TypeText(string fullText)
    {
        displayText.text = "";
        foreach (char c in fullText)
        {
            displayText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
