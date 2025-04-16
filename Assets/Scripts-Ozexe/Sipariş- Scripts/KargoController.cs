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

    void OpenKargo()
    {
        isOpened = true;

        // Kapal� kargoyu a��k kargo ile de�i�tir
        GameObject acikKargo = Instantiate(kargoAcikPrefab, transform.position, Quaternion.identity);

        // �r�n� i�inden ��kar ve aktif et
        if (productPrefab != null)
        {
            Instantiate(productPrefab, acikKargo.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        // Eski kapal� kargoyu yok et
        Destroy(gameObject);
    }
}

