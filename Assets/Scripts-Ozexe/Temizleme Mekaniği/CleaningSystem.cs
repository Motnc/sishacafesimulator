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

    private GameObject currentTargetObject;
    private Material currentTargetMaterial;

    public Animator mopAnimator; // Mop animatör referansı
    public Animator bezAnimator; 

    void Start()
    {
        cleaningCircleObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCleaning)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange, dirtySurfaceLayer))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null)
                {
                    currentTargetObject = hit.collider.gameObject;
                    currentTargetMaterial = rend.material;

                    StartCleaning();
                }
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

    void StartCleaning()
    {
        isCleaning = true;
        cleanTimer = 0f;
        cleaningCircleUI.fillAmount = 0f;
        cleaningCircleObject.SetActive(true);

        if (mopAnimator != null)
        {
            mopAnimator.SetTrigger("Clean");
        }
        if (bezAnimator != null)
        {
            bezAnimator.SetTrigger("Clean");
        }

        Debug.Log("Temizlik başladı.");
    }

    void FinishCleaning()
    {
        isCleaning = false;
        cleaningCircleObject.SetActive(false);

        if (currentTargetObject != null)
        {
            Destroy(currentTargetObject);
        }

        Debug.Log("Temizlik tamamlandı.");
    }
}
