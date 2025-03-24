using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public GameObject logo;
    public GameObject loadingPanel;
    public GameObject buttonPanel;

    public float moveSpeed = 2f;
    public float moveDistance = 200f;
    public float delayBeforeLoading = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = logo.transform.position;
        targetPosition = startPosition + Vector3.up * moveDistance;

        loadingPanel.SetActive(false);
        buttonPanel.SetActive(false);

        StartCoroutine(AnimateLogo());
    }

    IEnumerator AnimateLogo()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            logo.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        logo.transform.position = targetPosition;

        yield return new WaitForSeconds(delayBeforeLoading);
        loadingPanel.SetActive(true);

        // ⏳ Tunggu 2 detik, lalu tampilkan tombol & sembunyikan loading
        yield return new WaitForSeconds(2f);
        loadingPanel.SetActive(false); // 🔥 Sembunyikan loading setelah selesai
        buttonPanel.SetActive(true);
    }

    public void LoadHomeScreen()
    {
        Debug.Log("Loading Home Screen..."); // Debugging untuk cek apakah fungsi terpanggil
        SceneManager.LoadScene("CategoryScreen"); 
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
