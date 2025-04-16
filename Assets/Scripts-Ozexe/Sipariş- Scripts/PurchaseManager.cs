using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab; // Kapal� kargo prefab'�
    public GameObject kargoAcikPrefab;   // A��k kargo prefab'�

    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;

    public Transform spawnPoint; // T�m kargolar i�in ortak spawn noktas�

    public void BuyNargile()
    {
        SpawnKargo(nargilePrefab);
    }

    public void BuyMasa()
    {
        SpawnKargo(masaPrefab);
    }

    public void BuySandalye()
    {
        SpawnKargo(sandalyePrefab);
    }

    void SpawnKargo(GameObject productPrefab)
    {
        // Kapal� kargo prefab'�n� olu�tur
        GameObject kapaliKargo = Instantiate(kargoKapaliPrefab, spawnPoint.position, Quaternion.identity);

        // Kargo i�indeki �r�n i�in referans sakla
        KargoController kargoController = kapaliKargo.GetComponent<KargoController>();
        if (kargoController != null)
        {
            kargoController.SetProduct(productPrefab);
        }
    }
}
