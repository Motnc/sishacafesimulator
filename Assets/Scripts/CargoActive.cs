using UnityEngine;

public class CargoActive : MonoBehaviour
{
    private bool isPlayerInTrigger = false; // Oyuncu trigger içinde mi?

    void Update()
    {
        // Eðer oyuncu trigger içindeyse ve F tuþuna basarsa:
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.K))
        {
            // Tüm çocuk objeleri aktive et
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Trigger içine girdiðinde çalýþýr
    void OnTriggerEnter(Collider other)
    {
        // Oyuncu tag'i varsa, tetikleyelim
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    // Trigger'dan çýktýðýnda çalýþýr
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
