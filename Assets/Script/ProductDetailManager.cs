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
    public Button tryProductButton;

    void Start()
    {
        if (productNameText == null) Debug.LogError("❌ productNameText belum di-assign di Inspector!");
        if (productPriceText == null) Debug.LogError("❌ productPriceText belum di-assign di Inspector!");
        if (productDescriptionText == null) Debug.LogError("❌ productDescriptionText belum di-assign di Inspector!");
        if (productImage == null) Debug.LogError("❌ productImage belum di-assign di Inspector!");
        if (backButton == null) Debug.LogError("❌ Back Button belum di-assign di Inspector!");
        if (tryProductButton == null) Debug.LogError("❌ Try Product Button belum di-assign di Inspector!");

        var data = ProductDataHolder.Instance;

        if (data == null || string.IsNullOrEmpty(data.ProductName))
        {
            Debug.LogError("❌ Tidak ada data produk yang ditemukan dari ProductDataHolder!");
            return;
        }

        productNameText.text = data.ProductName;
        productPriceText.text = data.ProductPrice;
        productDescriptionText.text = data.ProductDescription;
        productImage.sprite = data.ProductImage;

        Debug.Log($"✅ Detail Produk Loaded: {data.ProductName}, {data.ProductPrice}, model3D: {data.ProductModel3D?.name}");

        backButton.onClick.AddListener(BackToPreviousScene);

        tryProductButton.onClick.AddListener(() =>
        {
            Debug.Log($"🚀 Coba produk: {data.ProductName} | Loading AR Preview...");
            SceneManager.LoadScene("FacePreviewScene");
        });
    }

    public void BackToPreviousScene()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene", "CategoryScreen");
        Debug.Log($"🔙 Kembali ke Scene: {previousScene}");
        SceneManager.LoadScene(previousScene);
    }
}
