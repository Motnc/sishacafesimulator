using TMPro;
using UnityEngine;

public class EconomyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        // Baþlangýç parasýný göster
        UpdateMoneyText(EconomyManager.Instance.CurrentMoney);

        // Para deðiþince UI'ý güncelle
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
