using UnityEngine;

public class CargoActive : MonoBehaviour
{
    private bool isPlayerInTrigger = false; // Oyuncu trigger i�inde mi?

    void Update()
    {
        // E�er oyuncu trigger i�indeyse ve F tu�una basarsa:
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.K))
        {
            // T�m �ocuk objeleri aktive et
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Trigger i�ine girdi�inde �al���r
    void OnTriggerEnter(Collider other)
    {
        // Oyuncu tag'i varsa, tetikleyelim
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    // Trigger'dan ��kt���nda �al���r
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
