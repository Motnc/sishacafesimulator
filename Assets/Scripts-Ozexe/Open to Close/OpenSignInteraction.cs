using UnityEngine;

public class OpenSignController : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 3f;
    public Material greenMaterial;
    public Material redMaterial;

    public GameObject openTextObject; // OPEN yazýsý objesi

    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                if (hit.collider.CompareTag("OpenSign"))
                {
                    ToggleOpenTextMaterial();
                }
            }
        }
    }

    void ToggleOpenTextMaterial()
    {
        if (openTextObject == null) return;

        var renderer = openTextObject.GetComponent<Renderer>();
        if (renderer == null) return;

        isOpen = !isOpen;
        FindObjectOfType<ShopOpenTimer>().StartTimer();

        renderer.material = isOpen ? greenMaterial : redMaterial;
    }
}
