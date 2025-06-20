using UnityEngine;

public class HookahLidPlacer : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 3f;

    public Transform hookahLidTarget; // Nargile �st�ndeki hedef

    private bool lidPlaced = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !lidPlaced)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Debug.Log("Raycast �arpt�: " + hit.collider.name);

                if (hit.collider.CompareTag("HookahLid"))
                {
                    Debug.Log("HookahLid tag'li objeye t�kland�.");

                    GameObject lid = hit.collider.gameObject;

                    // Collider devre d���
                    Collider col = lid.GetComponent<Collider>();
                    if (col != null)
                    {
                        col.enabled = false;
                        Debug.Log("Collider kapat�ld�.");
                    }

                    // Pozisyon ta��
                    if (hookahLidTarget != null)
                    {
                        lid.transform.position = hookahLidTarget.position;
                        lid.transform.rotation = hookahLidTarget.rotation;
                        lid.transform.SetParent(hookahLidTarget);
                        Debug.Log("Kapak yerine yerle�tirildi.");
                    }
                    else
                    {
                        Debug.LogWarning("hookahLidTarget atanmad�!");
                    }

                    lidPlaced = true;
                }
                else
                {
                    Debug.Log("Tag e�le�medi: " + hit.collider.tag);
                }
            }
            else
            {
                Debug.Log("Raycast hi�bir objeye �arpmad�.");
            }
        }
    }
}
