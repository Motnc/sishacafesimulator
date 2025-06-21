using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MicrowaveInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 2.5f;
    public LayerMask interactableLayer;

    public GameObject cookedSimitPrefab;
    public Transform plateSpawnPoint;

    public GameObject cleaningCircleObject;
    public Image cleaningCircleUI;
    public float cookTime = 3f;

    public PickUp pickUpScript;

    private GameObject placedSimit;
    private bool simitCooked = false;

    void Start()
    {
        if (cleaningCircleObject != null)
            cleaningCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
            {
                if (hit.collider.CompareTag("MicrowavePlate") && placedSimit == null)
                {
                    TryPlaceSimit();
                }
                else if (hit.collider.CompareTag("MicrowaveButton") && placedSimit != null && !simitCooked)
                {
                    StartCoroutine(CookSimit());
                }
            }
        }
    }

    void TryPlaceSimit()
    {
        GameObject heldItem = pickUpScript.GetHeldItem();
        if (heldItem != null && heldItem.name.Contains("NormalSimit"))
        {
            placedSimit = Instantiate(heldItem, plateSpawnPoint.position, plateSpawnPoint.rotation);
            placedSimit.transform.SetParent(plateSpawnPoint);
            Destroy(heldItem); // Elimizdeki simiti yok et
            pickUpScript.ForceClearHand(); // El boþalsýn
            Debug.Log("Simit tabaða yerleþtirildi.");
        }
    }

    IEnumerator CookSimit()
    {
        cleaningCircleObject.SetActive(true);
        cleaningCircleUI.fillAmount = 0f;

        float timer = 0f;
        while (timer < cookTime)
        {
            timer += Time.deltaTime;
            cleaningCircleUI.fillAmount = timer / cookTime;
            yield return null;
        }

        Destroy(placedSimit); // Normal simiti yok et
        Instantiate(cookedSimitPrefab, plateSpawnPoint.position, plateSpawnPoint.rotation, plateSpawnPoint);
        simitCooked = true;
        cleaningCircleObject.SetActive(false);
        Debug.Log("Simit piþti.");
    }
}
