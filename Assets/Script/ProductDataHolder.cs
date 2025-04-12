using UnityEngine;
using static ProductCategory;

public class ProductDataHolder : MonoBehaviour
{
    public static ProductDataHolder Instance;

    public string ProductName;
    public string ProductPrice;
    public string ProductDescription;
    public Sprite ProductImage;
    public GameObject ProductModel3D;
    public ProductCategory ProductCategory;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetProduct(string name, string price, string description, Sprite image, GameObject model3D,ProductCategory category)
    {
        ProductName = name;
        ProductPrice = price;
        ProductDescription = description;
        ProductImage = image;
        ProductModel3D = model3D;
        ProductCategory = category;
    }
}
