using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher_kuisMakan : MonoBehaviour
{
    public Text displayText;
    public float typeSpeed = 0.05f;

    [TextArea(2, 5)]
    public List<string> textList = new List<string>();

    private int currentIndex = 0;

    void Start()
    {
        if (textList != null && textList.Count > 0)
        {
            StartCoroutine(TypeText(textList[currentIndex]));
        }
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
