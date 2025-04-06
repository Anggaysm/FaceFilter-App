using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProductManager : MonoBehaviour
{
    public GameObject productPrefab;
    public Transform contentPanel;
    public Button backButton;

    [System.Serializable]
    public class Product
    {
        public string name;
        public Sprite image;
        public string price;
        public string description;
        public GameObject model3D;
    }

    public List<Product> productList = new List<Product>();

    void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(GoBackToCategory);
        else
            Debug.LogError("Back Button belum di-assign di Inspector!");

        GenerateProducts();
    }

    void GenerateProducts()
    {
        if (productPrefab == null || contentPanel == null)
        {
            Debug.LogError("ERROR: Product Prefab atau Content Panel belum di-assign!");
            return;
        }

        foreach (var product in productList)
        {
            GameObject newProduct = Instantiate(productPrefab, contentPanel);

            Transform imageTransform = newProduct.transform.Find("ProductImage");
            Transform nameTransform = newProduct.transform.Find("ProductName");
            Transform priceTransform = newProduct.transform.Find("ProductPrice");
            Button detailButton = newProduct.transform.Find("DetailButton")?.GetComponent<Button>();

            if (imageTransform == null || nameTransform == null || priceTransform == null)
            {
                Debug.LogError("ERROR: Struktur prefab salah!");
                continue;
            }

            imageTransform.GetComponent<Image>().sprite = product.image;
            nameTransform.GetComponent<TextMeshProUGUI>().text = product.name;
            priceTransform.GetComponent<TextMeshProUGUI>().text = product.price;

            if (detailButton != null)
                detailButton.onClick.AddListener(() => OpenDetail(product));
        }
    }

    public void OpenDetail(Product product)
    {
        if (product == null)
        {
            Debug.LogError("Produk tidak ditemukan!");
            return;
        }

        PlayerPrefs.SetString("ProductName", product.name);
        PlayerPrefs.SetString("ProductPrice", product.price);
        PlayerPrefs.SetString("ProductDescription", product.description);

        // ✅ Simpan gambar produk (jika ada)
        if (product.image != null)
        {
            Texture2D texture = product.image.texture;
            byte[] imageBytes = texture.EncodeToPNG();
            string encodedImage = System.Convert.ToBase64String(imageBytes);
            PlayerPrefs.SetString("ProductImage", encodedImage);
        }
        else
        {
            PlayerPrefs.DeleteKey("ProductImage");
        }

        // Simpan scene asal
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);

        PlayerPrefs.Save();

        Debug.Log($"➡ Ke Detail Produk: {product.name}, {product.price}, {product.description}");
        SceneManager.LoadScene("ProductDetail");
    }

    public void GoBackToCategory()
    {
        string previousScene = PlayerPrefs.GetString("LastCategoryScene", "CategoryScreen");
        SceneManager.LoadScene(previousScene);
    }
}
