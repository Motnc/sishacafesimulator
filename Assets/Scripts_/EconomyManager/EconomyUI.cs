using TMPro;
using UnityEngine;

public class EconomyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        // Ba�lang�� paras�n� g�ster
        UpdateMoneyText(EconomyManager.Instance.CurrentMoney);

        // Para de�i�ince UI'� g�ncelle
        EconomyManager.Instance.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDestroy()
    {
        if (EconomyManager.Instance != null)
            EconomyManager.Instance.OnMoneyChanged -= UpdateMoneyText;
    }

    private void UpdateMoneyText(int newAmount)
    {
        moneyText.text = $"{newAmount}";
    }
}
