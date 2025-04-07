using UnityEngine;
using UnityEngine.UI;

public class ProductDataHolder : MonoBehaviour
{
    public static ProductDataHolder Instance;

    public string ProductName;
    public string ProductPrice;
    public string ProductDescription;
    public Sprite ProductImage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // supaya tidak hilang ketika pindah scene
        }
        else
        {
            Destroy(gameObject); // hindari duplikat
        }
    }

    public void SetProduct(string name, string price, string description, Sprite image)
    {
        ProductName = name;
        ProductPrice = price;
        ProductDescription = description;
        ProductImage = image;
    }
}
