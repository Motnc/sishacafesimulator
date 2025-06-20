using UnityEngine;

public class KargoUrun : MonoBehaviour
{
    [SerializeField] private int assignedIndex = 0;
    private bool isTriggered = false;
    private KargoController kargoController;

    // Bu fonksiyon kargo taraf�ndan �a�r�l�r
    public void Initialize(KargoController controller)
    {
        kargoController = controller;

        if (kargoController != null)
        {
            kargoController.CloseKargo(); // Kapama animasyonu tetikle
        }

        TriggerObjeBuild(); // Yerle�tirme sistemini burada tetikle
    }

    private void TriggerObjeBuild()
    {
        if (isTriggered) return;

        ObjeBuild objeBuild = GameObject.FindWithTag("GameManager")?.GetComponent<ObjeBuild>();
        if (objeBuild != null && assignedIndex >= 0)
        {
            objeBuild.ResetBuildState(); // Gerekirse yerle�tirme durumu s�f�rlan�r
            objeBuild.SetCurrentObject(assignedIndex);
            Debug.Log($"Obje {assignedIndex} se�ildi ve aktif edildi.");
            isTriggered = true;
        }
    }
}
