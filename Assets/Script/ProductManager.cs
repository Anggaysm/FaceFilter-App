using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProductManager : MonoBehaviour
{
    public GameObject productPrefab; // Prefab produk
    public Transform contentPanel; // Tempat spawn produk
    public Button backButton; // Tombol kembali ke kategori

    [System.Serializable]
    public class Product
    {
        public string name;
        public Sprite image;
        public string price;
        public string description;
        public GameObject model3D; // Objek 3D interaktif
    }

    public List<Product> productList = new List<Product>(); // Daftar produk

    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoBackToCategory);
        }
        else
        {
            Debug.LogError("Back Button belum di-assign di Inspector!");
        }

        GenerateProducts();
    }

    void GenerateProducts()
    {
        if (productPrefab == null)
        {
            Debug.LogError("ERROR: Product Prefab belum di-assign di Inspector!");
            return;
        }
        if (contentPanel == null)
        {
            Debug.LogError("ERROR: Content Panel belum di-assign di Inspector!");
            return;
        }

        Debug.Log("Memulai Generate Produk...");
        foreach (var product in productList)
        {
            GameObject newProduct = Instantiate(productPrefab, contentPanel);

            Transform imageTransform = newProduct.transform.Find("ProductImage");
            Transform nameTransform = newProduct.transform.Find("ProductName");
            Transform priceTransform = newProduct.transform.Find("ProductPrice");
            Button detailButton = newProduct.transform.Find("DetailButton")?.GetComponent<Button>();

            if (imageTransform == null || nameTransform == null || priceTransform == null)
            {
                Debug.LogError("ERROR: Struktur prefab salah. Pastikan prefab memiliki ProductImage, ProductName, dan ProductPrice!");
                continue;
            }

            imageTransform.GetComponent<Image>().sprite = product.image;
            nameTransform.GetComponent<TextMeshProUGUI>().text = product.name;
            priceTransform.GetComponent<TextMeshProUGUI>().text = product.price;

            if (detailButton != null)
            {
                // Gunakan lambda agar setiap tombol memiliki data produk yang berbeda
                detailButton.onClick.AddListener(() => OpenDetail(product));
            }
            else
            {
                Debug.LogError("ERROR: Detail Button tidak ditemukan dalam prefab produk!");
            }
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
        
        // Simpan scene asal sebelum pindah ke detail
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        
        // Simpan perubahan ke PlayerPrefs
        PlayerPrefs.Save(); 

        Debug.Log($"Berpindah ke ProductDetail dengan data: {product.name}, {product.price}, {product.description}");
        SceneManager.LoadScene("ProductDetail");
    }


    public void GoBackToCategory()
    {
        string previousScene = PlayerPrefs.GetString("LastCategoryScene", "CategoryScreen");
        SceneManager.LoadScene(previousScene);
    }
}
