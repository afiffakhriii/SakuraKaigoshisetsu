using UnityEngine;
using UnityEngine.SceneManagement;

public class Salah : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip salahClip;
    public float delayBeforeNextScene = 2f; // Bisa disesuaikan dengan panjang audio

    void Start()
    {
        PlayerPrefs.SetInt("KembaliLanjut", 1);

        if (audioSource != null && salahClip != null)
        {
            audioSource.clip = salahClip;
            audioSource.Play();
            StartCoroutine(KembaliSetelahAudio());
        }
        else
        {
            // Jika tidak ada audio, tetap lanjut setelah delay default
            StartCoroutine(KembaliTanpaAudio());
        }
    }

    System.Collections.IEnumerator KembaliSetelahAudio()
    {
        yield return new WaitForSeconds(salahClip.length);
        SceneManager.LoadScene("SceneMainKuis");
    }

    System.Collections.IEnumerator KembaliTanpaAudio()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);
        SceneManager.LoadScene("SceneMainKuis");
    }
}
