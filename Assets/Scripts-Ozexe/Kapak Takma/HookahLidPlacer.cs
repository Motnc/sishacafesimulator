using UnityEngine;

public class HookahLidPlacer : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 3f;

    public Transform hookahLidTarget; // Nargile üstündeki hedef

    private bool lidPlaced = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !lidPlaced)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Debug.Log("Raycast çarptý: " + hit.collider.name);

                if (hit.collider.CompareTag("HookahLid"))
                {
                    Debug.Log("HookahLid tag'li objeye týklandý.");

                    GameObject lid = hit.collider.gameObject;

                    // Collider devre dýþý
                    Collider col = lid.GetComponent<Collider>();
                    if (col != null)
                    {
                        col.enabled = false;
                        Debug.Log("Collider kapatýldý.");
                    }

                    // Pozisyon taþý
                    if (hookahLidTarget != null)
                    {
                        lid.transform.position = hookahLidTarget.position;
                        lid.transform.rotation = hookahLidTarget.rotation;
                        lid.transform.SetParent(hookahLidTarget);
                        Debug.Log("Kapak yerine yerleþtirildi.");
                    }
                    else
                    {
                        Debug.LogWarning("hookahLidTarget atanmadý!");
                    }

                    lidPlaced = true;
                }
                else
                {
                    Debug.Log("Tag eþleþmedi: " + hit.collider.tag);
                }
            }
            else
            {
                Debug.Log("Raycast hiçbir objeye çarpmadý.");
            }
        }
    }
}
