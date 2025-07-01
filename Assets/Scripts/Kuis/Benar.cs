using UnityEngine;
using UnityEngine.SceneManagement;

public class Benar : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(KembaliKeMainKuis());
    }

    System.Collections.IEnumerator KembaliKeMainKuis()
    {
        PlayerPrefs.SetInt("KembaliLanjut", 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SceneMainKuis");
    }
}