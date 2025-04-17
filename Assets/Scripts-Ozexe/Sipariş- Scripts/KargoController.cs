using UnityEngine;

public class KargoController : MonoBehaviour
{
    public GameObject kargoAcikPrefab; // A��k kargo hali
    private GameObject productPrefab;  // ��indeki �r�n

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

        // Kargoyu a��k modelle de�i�tir
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        GameObject acikKargo = Instantiate(kargoAcikPrefab, spawnPosition, spawnRotation);

        // A��k kargo i�inde "UrunYeri" ad�nda bir bo� GameObject arar.
        Transform urunYeri = acikKargo.transform.Find("UrunYeri");

        if (urunYeri != null && productPrefab != null)
        {
            // �r�n� kolinin i�indeki UrunYeri noktas�nda k���k olarak spawn et
            GameObject urun = Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
            urun.transform.localScale = Vector3.one * 0.3f;  // �r�n k���k boyutta spawn olur
        }
        else if (productPrefab != null)
        {
            // E�er "UrunYeri" bulunamazsa, yine k���k olarak yak�n pozisyona spawn
            GameObject urun = Instantiate(productPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);
            urun.transform.localScale = Vector3.one * 0.1f;
        }

        // Kapal� kargoyu yok et
        Destroy(gameObject);
    }
}

