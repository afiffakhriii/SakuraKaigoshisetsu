using UnityEngine;
using UnityEngine.SceneManagement;

public class Benar : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip benarClip;
    public float delayBeforeNextScene = 2f; // fallback jika audio tidak tersedia

    void Start()
    {
        PlayerPrefs.SetInt("KembaliLanjut", 1);

        if (audioSource != null && benarClip != null)
        {
            audioSource.clip = benarClip;
            audioSource.Play();
            StartCoroutine(KembaliSetelahAudio());
        }
        else
        {
            StartCoroutine(KembaliTanpaAudio());
        }
    }

    System.Collections.IEnumerator KembaliSetelahAudio()
    {
        yield return new WaitForSeconds(benarClip.length);
        SceneManager.LoadScene("SceneMainKuis");
    }

    System.Collections.IEnumerator KembaliTanpaAudio()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);
        SceneManager.LoadScene("SceneMainKuis");
    }
}
