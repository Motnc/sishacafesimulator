using UnityEngine;

public class KargoUrun : MonoBehaviour
{
    [SerializeField] private int assignedIndex = -1;
    private bool isTriggered = false;
    private KargoController kargoController;

    public void Initialize(KargoController controller)
    {
        kargoController = controller;
    }

    void Start()
    {
        Debug.Log($"KargoUrun aktifleþti: {gameObject.name}");

        if (kargoController != null)
        {
            kargoController.CloseKargo(); // Animasyonu tetikle
        }

        TriggerObjeBuild(); // ObjeBuild sistemini tetikle
    }

    private void TriggerObjeBuild()
    {
        if (isTriggered) return;

        ObjeBuild objeBuild = GameObject.FindWithTag("GameManager")?.GetComponent<ObjeBuild>();
        if (objeBuild != null && assignedIndex >= 0)
        {
            objeBuild.SetCurrentObject(assignedIndex);
            Debug.Log($"Obje {assignedIndex} seçildi ve aktif edildi.");
            isTriggered = true;
        }
    }
}
