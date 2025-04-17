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

    private void OpenKargo()
    {
        isOpened = true;

        // Kargoyu açýk modelle deðiþtir
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        GameObject acikKargo = Instantiate(kargoAcikPrefab, spawnPosition, spawnRotation);

        // Açýk kargo içinde "UrunYeri" adýnda bir boþ GameObject arar.
        Transform urunYeri = acikKargo.transform.Find("UrunYeri");

        if (urunYeri != null && productPrefab != null)
        {
            // Ürünü kolinin içindeki UrunYeri noktasýnda küçük olarak spawn et
            GameObject urun = Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
            urun.transform.localScale = Vector3.one * 0.3f;  // Ürün küçük boyutta spawn olur
        }
        else if (productPrefab != null)
        {
            // Eðer "UrunYeri" bulunamazsa, yine küçük olarak yakýn pozisyona spawn
            GameObject urun = Instantiate(productPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);
            urun.transform.localScale = Vector3.one * 0.1f;
        }

        // Kapalý kargoyu yok et
        Destroy(gameObject);
    }
}

