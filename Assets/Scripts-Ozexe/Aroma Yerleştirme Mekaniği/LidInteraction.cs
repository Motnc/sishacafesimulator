using UnityEngine;

public class LidInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 2f;
    public LayerMask lidLayer; // Sadece kapaðýn layer'ý

    private Animator animator;
    private bool hasOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug çizgi görseli
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionRange, Color.red);

        if (Input.GetMouseButtonDown(0) && !hasOpened)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, lidLayer))
            {
                if (hit.collider.CompareTag("AromaLid"))
                {
                    animator.SetTrigger("Open");
                    hasOpened = true;
                    Debug.Log("Kapaða týklandý ve açýldý.");
                }
                else
                {
                    Debug.Log("Ray çarptý ama tag yanlýþ: " + hit.collider.tag);
                }
            }
            else
            {
                Debug.Log("Ray kapaða çarpmadý.");
            }
        }
    }
}
