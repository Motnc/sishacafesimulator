using UnityEngine;

public class KargoController : MonoBehaviour
{
    public GameObject kargoAcikPrefab; // Açýk kargo hali
    private GameObject productPrefab;  // Ýçindeki ürün

    private bool isOpened = false;

    public void SetProduct(GameObject product)
    {
        productPrefab = product;
    }

    void Update()
    {
        if (!isOpened && Input.GetKeyDown(KeyCode.K))
        {
            OpenKargo();
        }
    }

    void OpenKargo()
    {
        isOpened = true;

        // Kapalý kargoyu açýk kargo ile deðiþtir
        GameObject acikKargo = Instantiate(kargoAcikPrefab, transform.position, Quaternion.identity);

        // Ürünü içinden çýkar ve aktif et
        if (productPrefab != null)
        {
            Instantiate(productPrefab, acikKargo.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        // Eski kapalý kargoyu yok et
        Destroy(gameObject);
    }
}

