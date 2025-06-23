using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HazirlamaManager : MonoBehaviour
{
    [Header("UI Bileþenleri")]
    public Image hazirlamaBar;
    public TextMeshProUGUI durumText;

    [Header("Ürün Verisi")]
    public FoodProductSO seciliUrun;

    private bool isPreparing = false;

    public void HazirlamaBaslat()
    {
        if (seciliUrun != null && !isPreparing)
        {
            StartCoroutine(HazirlamaRutini());
        }
    }

    IEnumerator HazirlamaRutini()
    {
        isPreparing = true;
        durumText.text = "Hazýrlanýyor";
        hazirlamaBar.fillAmount = 0f;

        float zaman = 0f;
        float hedefSure = seciliUrun.preparationTime;

        while (zaman < hedefSure)
        {
            zaman += Time.deltaTime;
            hazirlamaBar.fillAmount = zaman / hedefSure;
            yield return null;
        }

        hazirlamaBar.fillAmount = 1f;
        durumText.text = "Hazýr";

        yield return new WaitForSeconds(1f); // 1 saniye göster
        durumText.text = "Bekliyor";
        hazirlamaBar.fillAmount = 0f;
        isPreparing = false;
    }
}


