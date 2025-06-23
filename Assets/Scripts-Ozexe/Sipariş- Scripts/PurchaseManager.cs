using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject kargoKapaliPrefab;
    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;
    public GameObject sandalyePrefab2;
    public GameObject hamburgerPrefab;
    public GameObject cayPrefab;
    public GameObject mopPrefab;
    public GameObject menemenPrefab;
    public GameObject omletPrefab;
    public GameObject portakalsuyuPrefab;
    public GameObject turkkahvesiPrefab;
    public GameObject sandvicPrefab;
    public GameObject tostPrefab;

    public GameObject komurPrefab;
    public GameObject aromaPrefab;
    public GameObject isiticiPrefab;

    public Transform kargoSpawnPoint;
    public Transform hamburgerSpawnPoint;
    public Transform caySpawnPoint;
    public Transform menemenSpawnPoint;
    public Transform omletSpawnPoint;
    public Transform portakalsuyuSpawnPoint;
    public Transform turkkahvesiSpawnPoint;
    public Transform sandvicSpawnPoint;
    public Transform tostSpawnPoint;
    public Transform mopSpawnPoint;
    public Transform komurSpawnPoint;
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

    public void BuySandalye2()
    {
        SpawnKargo(sandalyePrefab2);
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
    public void BuyMop()
    {
        SpawnDirect(mopPrefab, mopSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyMenemen()
    {
        SpawnDirect(menemenPrefab, menemenSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyOmlet()
    {
        SpawnDirect(omletPrefab, omletSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyPortakalsuyu()
    {
        SpawnDirect(portakalsuyuPrefab, portakalsuyuSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyTost()
    {
        SpawnDirect(tostPrefab, tostSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyTurkkahvesi()
    {
        SpawnDirect(turkkahvesiPrefab, turkkahvesiSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuySandvic()
    {
        SpawnDirect(sandvicPrefab, sandvicSpawnPoint);
        EconomyManager.Instance.TrySpendMoney(20);
    }
    public void BuyKomur()
    {
        SpawnDirect(komurPrefab, komurSpawnPoint);
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
            case "Sandalye2":
                SpawnKargo(sandalyePrefab2);
                break;
            case "Hamburger":
                SpawnDirect(hamburgerPrefab, hamburgerSpawnPoint);
                break;
            case "Cay":
                SpawnDirect(cayPrefab, caySpawnPoint);
                break;
            case "Mop":
                SpawnDirect(mopPrefab, mopSpawnPoint);
                break;
            case "Menemen":
                SpawnDirect(menemenPrefab, menemenSpawnPoint);
                break;
            case "Omlet":
                SpawnDirect(omletPrefab, omletSpawnPoint);
                break;
            case "Portakalsuyu":
                SpawnDirect(portakalsuyuPrefab, portakalsuyuSpawnPoint);
                break;
            case "Tost":
                SpawnDirect(tostPrefab, tostSpawnPoint);
                break;
            case "Turkkahvesi":
                SpawnDirect(turkkahvesiPrefab, turkkahvesiSpawnPoint);
                break;
            case "Sandvic":
                SpawnDirect(sandvicPrefab, sandvicSpawnPoint);
                break;
            case "Komur":
                SpawnDirect(komurPrefab, komurSpawnPoint);
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



