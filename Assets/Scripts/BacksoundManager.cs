using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BacksoundManager : MonoBehaviour
{
    private static BacksoundManager instance;
    private AudioSource audioSource;

    [Header("Bisa Diisi dari Inspector")]
    public AudioClip backsoundClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Siapkan AudioSource
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            if (backsoundClip != null)
            {
                audioSource.clip = backsoundClip;
                audioSource.Play(); // volume akan ikut media volume handphone
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
