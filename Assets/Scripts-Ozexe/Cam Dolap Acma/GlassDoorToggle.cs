using UnityEngine;

public class GlassDoorToggle : MonoBehaviour
{
    private Animator animator;

    [Header("Raycast Ayarlarý")]
    public Camera playerCamera;
    public float rayDistance = 3f;
    public string glassTag = "Glass"; // Cam objelerinin tag'i

    void Start()
    {
        animator = GetComponent<Animator>();
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                // Sadece bu objeye týklanýrsa
                if (hit.collider.gameObject == gameObject && hit.collider.CompareTag(glassTag))
                {
                    animator.SetTrigger("Toggle");
                    Debug.Log($"Cam týklandý: {gameObject.name} – Toggle tetiklendi.");
                }
            }
        }
    }
}
