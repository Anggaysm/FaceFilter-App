using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProductDetailManager : MonoBehaviour
{
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productPriceText;
    public TextMeshProUGUI productDescriptionText;
    public Image productImageUI;
    public Button backButton;

    void Start()
    {
        if (productNameText == null) Debug.LogError("❌ productNameText belum diassign!");
        if (productPriceText == null) Debug.LogError("❌ productPriceText belum diassign!");
        if (productDescriptionText == null) Debug.LogError("❌ productDescriptionText belum diassign!");
        if (productImageUI == null) Debug.LogWarning("⚠ productImageUI belum diassign!");

        if (!PlayerPrefs.HasKey("ProductName") || !PlayerPrefs.HasKey("ProductPrice") || !PlayerPrefs.HasKey("ProductDescription"))
        {
            Debug.LogError("❌ Data produk tidak lengkap di PlayerPrefs!");
            return;
        }

        productNameText.text = PlayerPrefs.GetString("ProductName");
        productPriceText.text = PlayerPrefs.GetString("ProductPrice");
        productDescriptionText.text = PlayerPrefs.GetString("ProductDescription");

        // ✅ Decode gambar dari Base64
        if (PlayerPrefs.HasKey("ProductImage"))
        {
            string encodedImage = PlayerPrefs.GetString("ProductImage");
            byte[] imageBytes = System.Convert.FromBase64String(encodedImage);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageBytes))
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                if (productImageUI != null)
                    productImageUI.sprite = sprite;
            }
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToPreviousScene);
        }
        else
        {
            Debug.LogError("❌ Back Button belum diassign!");
        }
    }

    public void BackToPreviousScene()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene");

            if (!string.IsNullOrEmpty(previousScene))
            {
                Debug.Log($"🔙 Kembali ke Scene: {previousScene}");
                SceneManager.LoadScene(previousScene);
                return;
            }
        }

        Debug.LogWarning("⚠ Tidak ada scene sebelumnya. Kembali ke default.");
        SceneManager.LoadScene("CategoryScreen");
    }
}
