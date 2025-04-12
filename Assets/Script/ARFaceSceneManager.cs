using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zappar;
using Zappar.Obsolete;

public class ARFaceSceneManager : MonoBehaviour
{
    public ZapparFaceTrackingAnchor faceTrackingTarget;

    [Header("Anchor Khusus Kategori")]
    public Transform antingRightAnchor;
    public Transform antingLeftAnchor;
    public Transform kacamataAnchor;
    public Transform topiAnchor;
    public Transform topengAnchor;
    public Transform mahkotaAnchor;

    public Button backButton;

    private GameObject currentModelRight;
    private GameObject currentModelLeft;
    private GameObject currentModelSingle;

    void Start()
    {
        var productModel = ProductDataHolder.Instance?.ProductModel3D;
        var category = ProductDataHolder.Instance?.ProductCategory;

        if (productModel == null || category == null)
        {
            Debug.LogWarning("⚠️ Data produk atau kategori tidak lengkap!");
            return;
        }

        if (category == ProductCategory.Anting)
        {
            // Instantiate dua kali untuk anting kanan dan kiri
            currentModelRight = Instantiate(productModel);
            currentModelLeft = Instantiate(productModel);

            if (antingRightAnchor == null || antingLeftAnchor == null)
            {
                Debug.LogWarning("⚠️ Anchor anting kanan atau kiri belum di-assign!");
                return;
            }

            currentModelRight.transform.SetParent(antingRightAnchor, false);
            currentModelLeft.transform.SetParent(antingLeftAnchor, false);

            Debug.Log($"👂 Menampilkan dua model anting: {productModel.name} di anchor kanan dan kiri");
        }
        else
        {
            // Instantiate biasa untuk kategori lain
            currentModelSingle = Instantiate(productModel);
            Transform selectedAnchor = GetAnchorByCategory(category.Value);

            if (selectedAnchor == null)
            {
                Debug.LogWarning($"⚠️ Tidak ada anchor untuk kategori '{category}'!");
                return;
            }

            currentModelSingle.transform.SetParent(selectedAnchor, false);

            Debug.Log($"🎉 Menampilkan model: {productModel.name} di anchor: {selectedAnchor.name}");
        }

        // Tombol kembali
        if (backButton != null)
        {
            backButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("ProductDetail");
            });
        }
    }

    // Kategori non-anting
    Transform GetAnchorByCategory(ProductCategory category)
    {
        switch (category)
        {
            case ProductCategory.Kacamata:
                return kacamataAnchor;
            case ProductCategory.Topi:
                return topiAnchor;
            case ProductCategory.Topeng:
                return topengAnchor;
            case ProductCategory.Mahkota:
                return mahkotaAnchor;
            default:
                return null;
        }
    }
}
