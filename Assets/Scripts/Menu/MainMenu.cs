using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MulaiKuis()
    {
        PlayerPrefs.SetString("KuisDari", "SceneMenu");
        SceneManager.LoadScene("SceneMainKuis");
    }
}