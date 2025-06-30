using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    public Graphic targetGraphic; // Bisa Image atau Text
    public float blinkSpeed = 1.5f;

    void Update()
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        Color c = targetGraphic.color;
        c.a = alpha;
        targetGraphic.color = c;
    }
}
