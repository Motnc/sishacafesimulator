using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab;
    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;
    public GameObject hamburgerPrefab;
    public GameObject cayPrefab;
    public GameObject aromaPrefab;
    public GameObject isiticiPrefab;

    public Transform kargoSpawnPoint;
    public Transform hamburgerSpawnPoint;
    public Transform caySpawnPoint;
    public Transform aromaSpawnPoint;
    public Transform isiticiSpawnPoint;

    public void BuyNargile()
    {
        SpawnKargo(nargilePrefab);

        EconomyManager.Instance.TrySpendMoney(200);

    }

    public void BuyMasa()
    {
        SpawnKargo(masaPrefab);

        EconomyManager.Instance.TrySpendMoney(350);
    }

    public void BuySandalye()
    {
        SpawnKargo(sandalyePrefab);
        EconomyManager.Instance.TrySpendMoney(200);

    }

    public void BuyHamburger()
    {
        SpawnDirect(hamburgerPrefab, hamburgerSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(35);

    }

    public void BuyCay()
    {
        SpawnDirect(cayPrefab, caySpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }

    public void BuyAroma()
    {
        SpawnDirect(aromaPrefab, aromaSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(50);
    }

    public void BuyIsitici()
    {
        SpawnDirect(isiticiPrefab, isiticiSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(50);
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
            case "Aroma":
                SpawnDirect(aromaPrefab, aromaSpawnPoint);
                break;
            case "Isitici":
                SpawnDirect(isiticiPrefab, isiticiSpawnPoint);
                break;
            default:
                Debug.LogWarning("Bilinmeyen ürün: " + itemName);
                break;
        }
    }
}



