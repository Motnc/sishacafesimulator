using UnityEngine;
using UnityEngine.InputSystem;

public class Kargo : MonoBehaviour
{
    private ObjeBuild objeBuild;
    [SerializeField] private int assignedIndex = -1; // Kargonun verdi�i obje indeksi
    [SerializeField] private InputActionReference useInput; // Kullan�m i�in InputAction

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
        if (assignedIndex >= 0 && IsPlayerInRange()) // Oyuncu menzil i�indeyse
        {
            Debug.Log($"Kargo etkile�ime girildi, obje indeksi: {assignedIndex}");
            objeBuild.SetCurrentObject(assignedIndex);
        }
    }

    private bool IsPlayerInRange()
    {
        // Oyuncunun kargo b�lgesinde olup olmad���n� kontrol et
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
            useInput.action.performed -= OnUseInput; // Aboneli�i kald�r
            useInput.action.Disable();
        }
    }
}
