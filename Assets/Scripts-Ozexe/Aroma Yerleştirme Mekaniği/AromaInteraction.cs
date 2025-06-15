using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AromaInteraction : MonoBehaviour
{
    public Transform playerCamera;
    public PickUp pickUpScript;
    public GameObject cleaningCircleObject;
    public Image cleaningCircleUI;

    public GameObject[] tobaccoLayers; // 3 katman
    public Transform[] targetPositions; // Nargile üstündeki hedef pozisyonlar

    public float placementDuration = 3f;
    public float interactionRange = 3f;

    private int placedLayerIndex = 0;
    private bool isPlacing = false;

    void Start()
    {
        if (cleaningCircleObject != null)
            cleaningCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPlacing && IsHoldingLid())
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                Debug.Log("Ray çarptı: " + hit.collider.name);
                if (hit.collider.CompareTag("Hookah"))
                {
                    Debug.Log("Nargileye tıklandı, işlem başlıyor.");
                    StartCoroutine(PlaceTobaccoRoutine());
                }
                else
                {
                    Debug.Log("Çarpılan obje Nargile değil.");
                }
            }
            else
            {
                Debug.Log("Ray hiçbir şeye çarpmadı.");
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
        if (placedLayerIndex >= tobaccoLayers.Length)
        {
            Debug.Log("Tüm katmanlar zaten yerleştirildi.");
            yield break;
        }

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

        // Katmanı yerleştir
        tobaccoLayers[placedLayerIndex].transform.position = targetPositions[placedLayerIndex].position;
        tobaccoLayers[placedLayerIndex].transform.rotation = targetPositions[placedLayerIndex].rotation;
        tobaccoLayers[placedLayerIndex].transform.SetParent(targetPositions[placedLayerIndex]); // Nargileye bağla

        Debug.Log($"Katman {placedLayerIndex + 1} yerleştirildi.");
        placedLayerIndex++;

        cleaningCircleObject.SetActive(false);
        isPlacing = false;
    }
}
