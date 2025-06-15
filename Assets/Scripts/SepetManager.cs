using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SepetManager : MonoBehaviour
{
    public Transform sepetUIParent;
    public GameObject sepetSatirPrefab;

    private Dictionary<ProductSO, SepetItem> sepet = new Dictionary<ProductSO, SepetItem>();

    public void AddToCart(ProductSO product)
    {
        Debug.Log("AddToCart çağrıldı: " + product.productName);

        if (sepet.ContainsKey(product))
        {
            sepet[product].quantity++;
        }
        else
        {
            sepet.Add(product, new SepetItem(product));
        }

        UpdateCartUI();
    }

    public void RemoveFromCart(ProductSO product)
    {
        if (sepet.ContainsKey(product))
        {
            sepet[product].quantity--;

            if (sepet[product].quantity <= 0)
                sepet.Remove(product);
        }

        UpdateCartUI();
    }

    void UpdateCartUI()
    {
        Debug.Log("Sepet UI güncelleniyor...");

        foreach (Transform child in sepetUIParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in sepet.Values)
        {
            GameObject satir = Instantiate(sepetSatirPrefab, sepetUIParent);

            // Ölçek ve pozisyon bozulmasın
            RectTransform rt = satir.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            rt.localRotation = Quaternion.identity;

            TextMeshProUGUI[] texts = satir.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length >= 3)
            {
                texts[0].text = item.quantity.ToString();              // Adet
                texts[1].text = item.product.productName;              // Ürün adı
                texts[2].text = item.GetTotalPrice().ToString("F2") + " TL"; // Fiyat
            }
        }
    }
}











