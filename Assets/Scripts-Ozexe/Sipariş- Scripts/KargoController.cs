using UnityEngine;

public class KargoController : MonoBehaviour
{
    public GameObject kargoAcikPrefab; // A��k koli prefab
    private GameObject productPrefab;  // �r�n prefab

    private bool isOpened = false;
    private GameObject player;
    public float acmaMesafesi = 3f; // Oyuncunun a�ma mesafesi

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

        // Kapal� koliyi yok etmeden �nce pozisyonu ve rotasyonu al
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        // A��k koliyi sahneye ekle
        GameObject acikKargo = Instantiate(kargoAcikPrefab, spawnPosition, spawnRotation);

        // A��k koli prefab'�n�n i�inde "UrunYeri" isimli bo� bir transform olmal�!
        Transform urunYeri = acikKargo.transform.Find("UrunYeri");

        if (urunYeri != null && productPrefab != null)
        {
            Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
        }
        else if (productPrefab != null) // E�er UrunYeri bulunamazsa, kolinin �st�ne b�rak.
        {
            Instantiate(productPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);
        }

        // Kapal� koliyi sahneden sil
        Destroy(gameObject);
    }
}

