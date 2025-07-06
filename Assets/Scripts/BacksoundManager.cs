using UnityEngine;

public class BacksoundManager : MonoBehaviour
{
    private static BacksoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan saat ganti scene
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat jika sudah ada satu
        }
    }
}
