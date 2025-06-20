using UnityEngine;
using System.Collections;

public class KargoController : MonoBehaviour
{
    public Animator kargoAnimator;
    private GameObject productPrefab;
    private bool isOpened = false;

    [SerializeField] private float spawnDelay = 1.5f;

    public void SetProduct(GameObject product)
    {
        productPrefab = product;
    }

    void Update()
    {
        if (!isOpened && Input.GetKeyDown(KeyCode.K))
        {
            if (IsPlayerInRange())
            {
                OpenKargo();
            }
        }
    }

    private void OpenKargo()
    {
        if (kargoAnimator != null)
        {
            kargoAnimator.SetTrigger("Open");
            isOpened = true;
            StartCoroutine(SpawnProductDelayed());
        }
        else
        {
            Debug.LogWarning("Animator is not assigned!");
        }
    }

    private IEnumerator SpawnProductDelayed()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnProduct();
    }

    private void SpawnProduct()
    {
        if (productPrefab == null) return;

        Transform urunYeri = transform.Find("UrunYeri");
        GameObject urun;

        if (urunYeri != null)
        {
            urun = Instantiate(productPrefab, urunYeri.position, urunYeri.rotation, urunYeri);
            urun.transform.localScale = Vector3.one * 0.3f;
        }
        else
        {
            urun = Instantiate(productPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            urun.transform.localScale = Vector3.one * 0.1f;
        }

        // Ürüne Kargo referansý gönder
        KargoUrun urunScript = urun.GetComponent<KargoUrun>();
        if (urunScript != null)
        {
            urunScript.Initialize(this); // Sadece burasý tetikliyor
            Destroy(urun);
        }
    }

    private bool IsPlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void CloseKargo()
    {
        if (kargoAnimator != null)
        {
            kargoAnimator.SetTrigger("Close");
            Debug.Log("Kargo kapatma animasyonu tetiklendi.");
        }
    }
}
