using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab;
    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;
    public GameObject hamburgerPrefab;
    public GameObject cayPrefab;

    public Transform kargoSpawnPoint;  // Koli için ortak spawn noktasý
    public Transform hamburgerSpawnPoint; // Hamburger için özel nokta
    public Transform caySpawnPoint;    // Çay için özel nokta

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

    public void BuyHamburger()
    {
        SpawnDirect(hamburgerPrefab, hamburgerSpawnPoint);
    }

    public void BuyCay()
    {
        SpawnDirect(cayPrefab, caySpawnPoint);
    }

    private void SpawnKargo(GameObject productPrefab)
    {
        GameObject kapaliKargo = Instantiate(kargoKapaliPrefab, kargoSpawnPoint.position, Quaternion.identity);
        KargoController kargoController = kapaliKargo.GetComponent<KargoController>();
        if (kargoController != null)
        {
            kargoController.SetProduct(productPrefab);
        }
    }

    private void SpawnDirect(GameObject productPrefab, Transform spawnPoint)
    {
        Instantiate(productPrefab, spawnPoint.position, Quaternion.identity);
    }
}

