using UnityEngine;

public class MoneyGain : MonoBehaviour
{
    public int Amount { get; private set; }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EconomyManager.Instance.AddMoney(Amount);
            Debug.Log($"Oyuncu {Amount} kazand�!");
            Destroy(gameObject); // Paray� yok et
        }
    }
}
