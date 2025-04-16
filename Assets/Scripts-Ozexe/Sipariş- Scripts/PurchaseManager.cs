using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab; // Kapalý kargo prefab'ý
    public GameObject kargoAcikPrefab;   // Açýk kargo prefab'ý

    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;

    public Transform spawnPoint; // Tüm kargolar için ortak spawn noktasý

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
        // Kapalý kargo prefab'ýný oluþtur
        GameObject kapaliKargo = Instantiate(kargoKapaliPrefab, spawnPoint.position, Quaternion.identity);

        // Kargo içindeki ürün için referans sakla
        KargoController kargoController = kapaliKargo.GetComponent<KargoController>();
        if (kargoController != null)
        {
            kargoController.SetProduct(productPrefab);
        }
    }
}
