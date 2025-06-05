using UnityEngine;
using UnityEngine.InputSystem;

public class Kargo : MonoBehaviour
{
    private ObjeBuild objeBuild;
    [SerializeField] private int assignedIndex = -1;
    [SerializeField] private InputActionReference useInput;

    private bool used = false;

    private void Start()
    {
        objeBuild = GameObject.FindWithTag("GameManager").GetComponent<ObjeBuild>();

        if (useInput != null)
        {
            useInput.action.Enable();
            useInput.action.performed += OnUseInput;
        }
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        if (!used && assignedIndex >= 0 && IsPlayerInRange())
        {
            Debug.Log($"Kargo etkileþime girildi, obje indeksi: {assignedIndex}");
            objeBuild.SetCurrentObject(assignedIndex);
            used = true;
        }
    }

    private bool IsPlayerInRange()
    {
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
            useInput.action.performed -= OnUseInput;
            useInput.action.Disable();
        }
    }
}
