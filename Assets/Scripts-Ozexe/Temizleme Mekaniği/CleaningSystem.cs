using UnityEngine;
using UnityEngine.UI;

public class CleaningSystem : MonoBehaviour
{
    public Transform playerCamera;
    public float cleaningRange = 2f;
    public LayerMask dirtySurfaceLayer;

    public GameObject cleaningCircleObject;
    public Image cleaningCircleUI;
    public float cleaningDuration = 3f;

    private float cleanTimer = 0f;
    private bool isCleaning = false;
    private GameObject currentDirtyObject;

    public PickUp pickUpScript; // PickUp scriptine referans

    void Start()
    {
        cleaningCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCleaning && IsHoldingMop())
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange, dirtySurfaceLayer))
            {
                currentDirtyObject = hit.collider.gameObject;
                StartCleaning();
            }
        }

        if (isCleaning)
        {
            cleanTimer += Time.deltaTime;
            cleaningCircleUI.fillAmount = cleanTimer / cleaningDuration;

            if (cleanTimer >= cleaningDuration)
            {
                FinishCleaning();
            }
        }
    }

    bool IsHoldingMop()
    {
        if (pickUpScript == null || pickUpScript.GetHeldItem() == null)
            return false;

        return pickUpScript.GetHeldItem().name.Contains("Mop");
    }

    void StartCleaning()
    {
        isCleaning = true;
        cleanTimer = 0f;
        cleaningCircleUI.fillAmount = 0f;
        cleaningCircleObject.SetActive(true);
        Debug.Log("Temizlik baþladý.");
    }

    void FinishCleaning()
    {
        isCleaning = false;
        cleaningCircleObject.SetActive(false);

        if (currentDirtyObject != null)
        {
            Destroy(currentDirtyObject);
        }

        Debug.Log("Temizlik tamamlandý. Zemin sahneden kaldýrýldý.");
    }
}
