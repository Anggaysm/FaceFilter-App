using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("SplashScreen"); // Ganti dengan nama scene Home kamu
    }
}
