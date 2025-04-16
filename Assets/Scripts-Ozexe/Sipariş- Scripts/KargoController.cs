using UnityEngine;

public class KargoController : MonoBehaviour
{
    public GameObject kargoAcikPrefab; // Açýk koli prefab
    private GameObject productPrefab;  // Ürün prefab

    private bool isOpened = false;
    private GameObject player;
    public float acmaMesafesi = 3f; // Oyuncunun açma mesafesi

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetProduct(GameObject product)
    {
        productPrefab = product;
    }

    private void Update()
    {
        if (!isOpened && Input.GetKeyDown(KeyCode.K))
        {
            if (player != null && Vector3.Distance(transform.position, player.transform.position) <= acmaMesafesi)
            {
                OpenKargo();
            }
        }
    }

    private void OpenKargo()
    {
        isOpened = true;

        // Kapalý koliyi yok etmeden önce pozisyonu ve rotasyonu al
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        // Açýk koliyi sahneye ekle
        GameObject acikKargo = Instantiate(kargoAcikPrefab, spawnPosition, spawnRotation);

        // Açýk koli prefab'ýnýn içinde "UrunYeri" isimli boþ bir transform olmalý!
        Transform urunYeri = acikKargo.transform.Find("UrunYeri");

        if (urunYeri != null && productPrefab != null)
        {
            Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
        }
        else if (productPrefab != null) // Eðer UrunYeri bulunamazsa, kolinin üstüne býrak.
        {
            Instantiate(productPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);
        }

        // Kapalý koliyi sahneden sil
        Destroy(gameObject);
    }
}

