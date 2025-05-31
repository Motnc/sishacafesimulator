using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SepetManager : MonoBehaviour
{
    public Transform sepetUIParent; // UI'daki sepet listesini tutacak alan
    public GameObject sepetSatirPrefab; // Tek bir satırı temsil eden prefab (Adet - Ürün - Fiyat)

    private Dictionary<string, SepetItem> sepet = new Dictionary<string, SepetItem>();

    public void AddToCart(Product product)
    {
        if (sepet.ContainsKey(product.productName))
        {
            sepet[product.productName].quantity++;
        }
        else
        {
            sepet.Add(product.productName, new SepetItem(product));
        }

        UpdateCartUI();
    }

    public void RemoveFromCart(Product product)
    {
        if (sepet.ContainsKey(product.productName))
        {
            sepet[product.productName].quantity--;

            if (sepet[product.productName].quantity <= 0)
            {
                sepet.Remove(product.productName);
            }

            UpdateCartUI();
        }
    }

    void UpdateCartUI()
    {
        foreach (Transform child in sepetUIParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in sepet.Values)
        {
            GameObject satir = Instantiate(sepetSatirPrefab, sepetUIParent);
            TextMeshProUGUI[] texts = satir.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = item.quantity.ToString();
            texts[1].text = item.product.productName;
            texts[2].text = item.GetTotalPrice().ToString("F2") + "₺";
        }
    }
}

