using UnityEngine;

public class KargoUrun : MonoBehaviour
{
    [SerializeField] private int assignedIndex = 0;
    private bool isTriggered = false;
    private KargoController kargoController;

    // Bu fonksiyon kargo tarafýndan çaðrýlýr
    public void Initialize(KargoController controller)
    {
        kargoController = controller;

        if (kargoController != null)
        {
            kargoController.CloseKargo(); // Kapama animasyonu tetikle
        }

        TriggerObjeBuild(); // Yerleþtirme sistemini burada tetikle
    }

    private void TriggerObjeBuild()
    {
        if (isTriggered) return;

        ObjeBuild objeBuild = GameObject.FindWithTag("GameManager")?.GetComponent<ObjeBuild>();
        if (objeBuild != null && assignedIndex >= 0)
        {
            objeBuild.ResetBuildState(); // Gerekirse yerleþtirme durumu sýfýrlanýr
            objeBuild.SetCurrentObject(assignedIndex);
            Debug.Log($"Obje {assignedIndex} seçildi ve aktif edildi.");
            isTriggered = true;
        }
    }
}
