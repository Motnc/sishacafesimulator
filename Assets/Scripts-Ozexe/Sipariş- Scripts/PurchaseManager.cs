using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab;
    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;
    public GameObject hamburgerPrefab;
    public GameObject cayPrefab;

    public Transform kargoSpawnPoint;
    public Transform hamburgerSpawnPoint;
    public Transform caySpawnPoint;

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
        EconomyManager.Instance.TrySpendMoney(200);

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

    private void LoadPurchasedItem(string itemName)
    {
        switch (itemName)
        {
            case "Nargile":
                SpawnKargo(nargilePrefab);
                break;
            case "Masa":
                SpawnKargo(masaPrefab);
                break;
            case "Sandalye":
                SpawnKargo(sandalyePrefab);
                break;
            case "Hamburger":
                SpawnDirect(hamburgerPrefab, hamburgerSpawnPoint);
                break;
            case "Cay":
                SpawnDirect(cayPrefab, caySpawnPoint);
                break;
            default:
                Debug.LogWarning("Bilinmeyen ürün: " + itemName);
                break;
        }
    }
}



