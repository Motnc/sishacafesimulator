using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoalHeatingSystem : MonoBehaviour
{
    public Transform playerCamera;
    public float heatingRange = 2f;
    public LayerMask coalLayer;

    public GameObject heatingCircleObject;
    public Image heatingCircleUI;
    public float heatingDuration = 3f;

    [Tooltip("")]
    public float revertDelay = 10f;

    private float heatTimer = 0f;
    private bool isHeating = false;

    private Renderer currentCoalRenderer;
    private Color originalColor;

    public PickUp pickUpScript;

    private Coroutine revertCoroutine;

    void Start()
    {
        heatingCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHeating && IsHoldingHeater())
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, heatingRange, coalLayer))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null)
                {
                    currentCoalRenderer = rend;
                    originalColor = rend.material.color;

                    StartHeating();
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

    void StartHeating()
    {
        isHeating = true;
        heatTimer = 0f;
        heatingCircleUI.fillAmount = 0f;
        heatingCircleObject.SetActive(true);
        Debug.Log("Isıtma başladı.");
    }

    void FinishHeating()
    {
        isHeating = false;
        heatingCircleObject.SetActive(false);

        if (currentCoalRenderer != null)
        {
            currentCoalRenderer.material.color = Color.red;
            Debug.Log("Isıtma tamamlandı.");
           
            if (revertCoroutine != null)
                StopCoroutine(revertCoroutine);
            
            revertCoroutine = StartCoroutine(RevertAfterDelay(currentCoalRenderer, originalColor, revertDelay));
        }
    }

    IEnumerator RevertAfterDelay(Renderer coalRenderer, Color revertColor, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (coalRenderer != null)
        {
            coalRenderer.material.color = revertColor;
            Debug.Log("Kömür soğudu, eski haline döndü.");
        }
    }
}
