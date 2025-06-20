using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AromaInteraction : MonoBehaviour
{
    public Transform playerCamera;
    public PickUp pickUpScript;
    public GameObject cleaningCircleObject;
    public Image cleaningCircleUI;

    public GameObject tobaccoPiece; // Tek tütün parçası
    public Transform targetPosition; // Yerleşeceği nokta

    public float placementDuration = 3f;
    public float interactionRange = 3f;

    private bool isPlacing = false;
    private bool isPlaced = false;

    void Start()
    {
        if (cleaningCircleObject != null)
            cleaningCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPlacing && !isPlaced && IsHoldingLid())
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    StartCoroutine(PlaceTobaccoRoutine());
                }
            }
        }
    }

    bool IsHoldingLid()
    {
        var held = pickUpScript.GetHeldItem();
        return held != null && held.CompareTag("AromaLid");
    }

    IEnumerator PlaceTobaccoRoutine()
    {
        isPlacing = true;
        cleaningCircleObject.SetActive(true);
        cleaningCircleUI.fillAmount = 0f;

        float timer = 0f;
        while (timer < placementDuration)
        {
            timer += Time.deltaTime;
            cleaningCircleUI.fillAmount = timer / placementDuration;
            yield return null;
        }

        tobaccoPiece.transform.position = targetPosition.position;
        tobaccoPiece.transform.rotation = targetPosition.rotation;
        tobaccoPiece.transform.SetParent(targetPosition); // Nargileye bağla

        cleaningCircleObject.SetActive(false);
        isPlacing = false;
        isPlaced = true;

        Debug.Log("Tütün yerleştirildi.");
    }
}
