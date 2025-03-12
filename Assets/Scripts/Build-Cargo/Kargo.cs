using UnityEngine;
using UnityEngine.InputSystem;

public class Kargo : MonoBehaviour
{
    private ObjeBuild objeBuild;
    [SerializeField] private int assignedIndex = -1; // Kargonun verdiði obje indeksi
    [SerializeField] private InputActionReference useInput; // Kullaným için InputAction

    private void Start()
    {
        objeBuild = GameObject.FindWithTag("GameManager").GetComponent<ObjeBuild>();

        if (useInput != null)
        {
            useInput.action.Enable();
            useInput.action.performed += OnUseInput; // Input'a abone ol
        }
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        if (assignedIndex >= 0 && IsPlayerInRange()) // Oyuncu menzil içindeyse
        {
            Debug.Log($"Kargo etkileþime girildi, obje indeksi: {assignedIndex}");
            objeBuild.SetCurrentObject(assignedIndex);
        }
    }

    private bool IsPlayerInRange()
    {
        // Oyuncunun kargo bölgesinde olup olmadýðýný kontrol et
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDestroy()
    {
        if (useInput != null)
        {
            useInput.action.performed -= OnUseInput; // Aboneliði kaldýr
            useInput.action.Disable();
        }
    }
}
