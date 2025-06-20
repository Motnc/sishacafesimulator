using UnityEngine;

public class GlassDoorToggle : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 3f;
    public Animator glassAnimator;
    public string targetTag = "Glass";

    private bool isOpen = false;
    private bool isAnimating = false;
    public float animationCooldown = 1.2f; // Aç/kapa süresi

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Debug.Log("Çarpýlan obje: " + hit.collider.name);

                if (hit.collider.CompareTag(targetTag))
                {
                    glassAnimator.SetTrigger("Toggle");
                    isOpen = !isOpen;
                    isAnimating = true;
                    Invoke(nameof(ResetAnimationFlag), animationCooldown);
                    Debug.Log(isOpen ? "Açýlýyor" : "Kapanýyor");
                }
            }
        }
    }

    void ResetAnimationFlag()
    {
        isAnimating = false;
    }
}
