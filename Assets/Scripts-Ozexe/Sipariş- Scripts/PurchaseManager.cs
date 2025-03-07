using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject nargilePrefab;
    public GameObject masaPrefab;
    public GameObject sandalyePrefab;

    public Transform nargileSpawnPoint;
    public Transform masaSpawnPoint;
    public Transform sandalyeSpawnPoint;

    public void BuyNargile()
    {
        Instantiate(nargilePrefab, nargileSpawnPoint.position, Quaternion.identity);
    }

    public void BuyMasa()
    {
        Instantiate(masaPrefab, masaSpawnPoint.position, Quaternion.identity);
    }

    public void BuySandalye()
    {
        Instantiate(sandalyePrefab, sandalyeSpawnPoint.position, Quaternion.identity);
    }
}
