using UnityEngine;

public class KargoController : MonoBehaviour
{
    public GameObject kargoAcikPrefab;
    private GameObject productPrefab;

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

        
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        GameObject acikKargo = Instantiate(kargoAcikPrefab, spawnPosition, spawnRotation);

        
        Transform urunYeri = acikKargo.transform.Find("UrunYeri");

        if (urunYeri != null && productPrefab != null)
        {
            
            GameObject urun = Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
            urun.transform.localScale = Vector3.one * 0.3f;  
        }
        else if (productPrefab != null)
        {
            
            GameObject urun = Instantiate(productPrefab, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);
            urun.transform.localScale = Vector3.one * 0.1f;
        }

        
        Destroy(gameObject);
    }
}



