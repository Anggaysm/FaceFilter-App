using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CategoryManager : MonoBehaviour
{
    public GameObject categoryPrefab; // Prefab kategori
    public Transform content; // Tempat kategori ditampilkan (Content di ScrollView)

    [System.Serializable]
    public class CategoryData
    {
        public string categoryName;
        public Sprite categoryIcon;
        
        #if UNITY_EDITOR
        public SceneAsset sceneAsset; // Scene yang bisa di-drag and drop di Inspector
        #endif
        
        [HideInInspector] public string sceneName; // Nama scene yang akan di-load
    }

    public List<CategoryData> categories; // List kategori

    void Start()
    {
        if (categoryPrefab == null || content == null)
        {
            Debug.LogError("CategoryPrefab atau Content belum di-assign di Inspector!");
            return;
        }

        GenerateCategories();
    }

    void GenerateCategories()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject); // Hapus item sebelumnya agar tidak bertumpuk
        }

        foreach (var category in categories)
        {
            GameObject newCategory = Instantiate(categoryPrefab, content);
            newCategory.transform.localScale = Vector3.one; // Pastikan ukuran tetap normal

            // Ambil komponen UI dari prefab
            TMP_Text categoryText = newCategory.transform.Find("CategoryText")?.GetComponent<TMP_Text>();
            Image categoryImage = newCategory.transform.Find("CategoryIcon")?.GetComponent<Image>();
            Button categoryButton = newCategory.GetComponent<Button>(); // Ambil Button

            if (categoryText != null) categoryText.text = category.categoryName;
            else Debug.LogError("CategoryText tidak ditemukan di prefab!");

            if (categoryImage != null && category.categoryIcon != null)
                categoryImage.sprite = category.categoryIcon;
            else Debug.LogError("CategoryIcon tidak ditemukan atau belum diisi di Inspector!");

            // Tambahkan event listener pada tombol
            if (categoryButton != null)
            {
                string targetScene = category.sceneName; // Simpan nilai untuk delegate
                categoryButton.onClick.AddListener(() => LoadCategoryScene(targetScene));
            }
            else
            {
                Debug.LogError("Button tidak ditemukan pada prefab kategori!");
            }
        }
    }

    void LoadCategoryScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene tujuan belum ditentukan di CategoryData!");
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CategoryManager))]
public class CategoryManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CategoryManager script = (CategoryManager)target;

        foreach (var category in script.categories)
        {
            if (category.sceneAsset != null)
            {
                category.sceneName = category.sceneAsset.name; // Ambil nama scene dari asset
            }
        }
    }
}
#endif
