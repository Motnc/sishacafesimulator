using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    [SerializeField] private int startingMoney = 1000;
    public int CurrentMoney { get; private set; }

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentMoney = startingMoney;
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount <= 0) return false;

        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney);
            Debug.Log($"Para harcandý: -{amount}. Kalan: {CurrentMoney}");
            return true;
        }
        else
        {
            Debug.Log("Yetersiz bakiye!");
            return false;
        }
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;

        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
        Debug.Log($"Para eklendi: +{amount}. Toplam: {CurrentMoney}");
    }

    public bool HasEnoughMoney(int amount)
    {
        return CurrentMoney >= amount;
    }
}
