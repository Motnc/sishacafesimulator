using UnityEngine;
using UnityEngine.UI;

public class CoalHeatingSystem : MonoBehaviour
{
    public Transform playerCamera;
    public float heatingRange = 2f;
    public LayerMask heatableLayer;

    public GameObject heatingCircleObject;
    public Image heatingCircleUI;
    public float heatingDuration = 3f;

    public Material heatedMaterial;
    public float revertDelay = 10f;

    public PickUp pickUpScript; // Elinde tuttuğun item yönetimi
    public GameObject coal; // Kömür objesi referansı

    public GameObject tong; // Maşa objesi
    public Transform tongCoalHoldPoint; // Maşanın ortasında kömürün duracağı boş GameObject
    public GameObject nargile; // Nargile prefabı
    public Transform nargileCoalHoldPoint; // Nargilenin üstünde kömürün duracağı boş GameObject

    private bool isHeating = false;
    private float heatTimer = 0f;
    private Renderer currentCoalRenderer;
    private Material originalMaterial;

    void Start()
    {
        heatingCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHeating)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, heatingRange))
            {
                GameObject clickedObj = hit.collider.gameObject;

                // Eğer elimizde kömür ısıtıcısı varsa ve kömüre tıklanmışsa ısıtmaya başla
                if (IsHoldingHeater() && clickedObj == coal)
                {
                    StartHeating();
                    currentCoalRenderer = coal.GetComponent<Renderer>();
                    originalMaterial = currentCoalRenderer.material;
                }
                // Elimde maşa varsa ve kömüre tıklanmışsa kömürü maşaya taşı
                else if (IsHoldingTong() && clickedObj == coal)
                {
                    PlaceCoalOnTong();
                }
                // Kömür maşanın üzerinde ve nargileye tıklanmışsa kömürü nargileye taşı
                else if (clickedObj == nargile && IsCoalOnTong())
                {
                    PlaceCoalOnNargile();
                }
            }
        }

        if (isHeating)
        {
            heatTimer += Time.deltaTime;
            heatingCircleUI.fillAmount = heatTimer / heatingDuration;

            if (heatTimer >= heatingDuration)
            {
                FinishHeating();
            }
        }
    }

    bool IsHoldingHeater()
    {
        if (pickUpScript == null || pickUpScript.GetHeldItem() == null)
            return false;

        return pickUpScript.GetHeldItem().name.Contains("Isitici");
    }

    bool IsHoldingTong()
    {
        if (pickUpScript == null || pickUpScript.GetHeldItem() == null)
            return false;

        return pickUpScript.GetHeldItem().name.Contains("Masa") == false && pickUpScript.GetHeldItem().name.Contains("Tong");
        // Yukarıdaki satırı elindeki maşa objenin adını uygun şekilde yazarsan değiştir
    }

    void StartHeating()
    {
        isHeating = true;
        heatTimer = 0f;
        heatingCircleUI.fillAmount = 0f;
        heatingCircleObject.SetActive(true);
        Debug.Log("Kömür ısıtma başladı.");
    }

    void FinishHeating()
    {
        isHeating = false;
        heatingCircleObject.SetActive(false);

        if (currentCoalRenderer != null && heatedMaterial != null)
        {
            currentCoalRenderer.material = heatedMaterial;
            Debug.Log("Kömür ısıtıldı!");

            Invoke(nameof(RevertToOriginal), revertDelay);
        }
    }

    void RevertToOriginal()
    {
        if (currentCoalRenderer != null && originalMaterial != null)
        {
            currentCoalRenderer.material = originalMaterial;
            Debug.Log("Kömür tekrar soğudu.");
        }
    }

    void PlaceCoalOnTong()
    {
        coal.transform.SetParent(tongCoalHoldPoint);
        coal.transform.localPosition = Vector3.zero;
        coal.transform.localRotation = Quaternion.identity;
        Debug.Log("Kömür maşanın ortasına yerleştirildi.");
    }

    void PlaceCoalOnNargile()
    {
        coal.transform.SetParent(nargileCoalHoldPoint);
        coal.transform.localPosition = Vector3.zero;
        coal.transform.localRotation = Quaternion.identity;
        Debug.Log("Kömür nargilenin üzerine yerleştirildi.");
    }

    bool IsCoalOnTong()
    {
        return coal.transform.IsChildOf(tongCoalHoldPoint);
    }
}
