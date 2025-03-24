using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProductDetailManager : MonoBehaviour
{
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productPriceText;
    public TextMeshProUGUI productDescriptionText;
    public Button backButton;

    void Start()
    {
        if (productNameText == null) Debug.LogError("❌ productNameText belum diassign di Inspector!");
        if (productPriceText == null) Debug.LogError("❌ productPriceText belum diassign di Inspector!");
        if (productDescriptionText == null) Debug.LogError("❌ productDescriptionText belum diassign di Inspector!");

        if (!PlayerPrefs.HasKey("ProductName") || !PlayerPrefs.HasKey("ProductPrice") || !PlayerPrefs.HasKey("ProductDescription"))
        {
            Debug.LogError("❌ Data produk tidak ditemukan di PlayerPrefs!");
            return;
        }

        string productName = PlayerPrefs.GetString("ProductName", "Produk Tidak Ditemukan");
        string productPrice = PlayerPrefs.GetString("ProductPrice", "Rp 0");
        string productDescription = PlayerPrefs.GetString("ProductDescription", "Tidak ada deskripsi.");

        if (productNameText != null) productNameText.text = productName;
        if (productPriceText != null) productPriceText.text = productPrice;
        if (productDescriptionText != null) productDescriptionText.text = productDescription;

        Debug.Log($"✅ Detail Produk: {productName}, {productPrice}, {productDescription}");

        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToPreviousScene);
        }
        else
        {
            Debug.LogError("❌ Back Button belum diassign di Inspector!");
        }
    }

    public void BackToPreviousScene()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene");

            if (!string.IsNullOrEmpty(previousScene))
            {
                Debug.Log($"🔄 Kembali ke Scene: {previousScene}");
                SceneManager.LoadScene(previousScene);
                return;
            }
        }

        Debug.LogWarning("⚠ Tidak ada scene sebelumnya yang tersimpan! Kembali ke default scene.");
        SceneManager.LoadScene("CategoryScreen"); // Ganti dengan scene default jika tidak ada data
    }
}
