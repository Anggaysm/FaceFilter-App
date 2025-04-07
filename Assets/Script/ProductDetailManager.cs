using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProductDetailManager : MonoBehaviour
{
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productPriceText;
    public TextMeshProUGUI productDescriptionText;
    public Image productImage;
    public Button backButton;

    void Start()
    {
        // Validasi komponen UI
        if (productNameText == null) Debug.LogError("❌ productNameText belum di-assign di Inspector!");
        if (productPriceText == null) Debug.LogError("❌ productPriceText belum di-assign di Inspector!");
        if (productDescriptionText == null) Debug.LogError("❌ productDescriptionText belum di-assign di Inspector!");
        if (productImage == null) Debug.LogError("❌ productImage belum di-assign di Inspector!");

        // Ambil data dari ProductDataHolder
        var data = ProductDataHolder.Instance;
        if (data == null || string.IsNullOrEmpty(data.ProductName))
        {
            Debug.LogError("❌ Tidak ada data produk yang ditemukan dari ProductDataHolder!");
            return;
        }

        // Tampilkan data di UI
        productNameText.text = data.ProductName;
        productPriceText.text = data.ProductPrice;
        productDescriptionText.text = data.ProductDescription;
        productImage.sprite = data.ProductImage;

        Debug.Log($"✅ Detail Produk Loaded: {data.ProductName}, {data.ProductPrice}");

        // Event tombol kembali
        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToPreviousScene);
        }
        else
        {
            Debug.LogError("❌ Back Button belum di-assign di Inspector!");
        }
    }

    public void BackToPreviousScene()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene", "CategoryScreen");
        Debug.Log($"🔙 Kembali ke Scene: {previousScene}");
        SceneManager.LoadScene(previousScene);
    }
}
