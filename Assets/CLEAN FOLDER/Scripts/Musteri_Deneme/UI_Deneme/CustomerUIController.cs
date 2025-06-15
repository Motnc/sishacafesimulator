using UnityEngine;
using TMPro;

public class CustomerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderText;

    private void Start()
    {
        SetOrderText(""); // Ba�lang��ta bo� yaz�
    }

    public void SetOrderText(string text)
    {
        orderText.text = text;
        orderText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    public void DeleteOrderText()
    {
        SetOrderText("");
    }
}
