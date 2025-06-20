using System.Collections;
using UnityEngine;

public class ObjeBuild : MonoBehaviour
{
    public GameObject[] Objects;         // Gerçek yerleþtirilecek objeler (prefab)
    public GameObject[] ObjectsPreview;  // Önizleme prefab'larý (bunlarý instantiate edeceðiz)

    private bool ObjeOlusturulabilirmi = false;
    public bool EtkilesimVarmi = false;

    public GameObject CurrentObject;
    private int currentIndex = -1;
    private bool objeYerlestirildi = false;

    void Update()
    {
        if (CurrentObject == null || objeYerlestirildi) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            CurrentObject.transform.position = hit.point;

            if (hit.transform.CompareTag("Platform") || EtkilesimVarmi)
            {
                ObjeOlusturulabilirmi = false;
            }
            else
            {
                ObjeOlusturulabilirmi = true;
            }

            if (Input.GetMouseButtonDown(0) && ObjeOlusturulabilirmi)
            {
                Instantiate(Objects[currentIndex], CurrentObject.transform.position, CurrentObject.transform.rotation);
                objeYerlestirildi = true;
                Destroy(CurrentObject);
                CurrentObject = null;
            }
        }
    }

    public void SetCurrentObject(int index)
    {
        if (index >= 0 && index < ObjectsPreview.Length)
        {
            if (CurrentObject != null)
            {
                Destroy(CurrentObject);
            }

            currentIndex = index;
            objeYerlestirildi = false; // Her yerleþtirmede sýfýrlanýr

            CurrentObject = Instantiate(ObjectsPreview[index]);
            CurrentObject.SetActive(true);

            Renderer objRenderer = CurrentObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = Color.green;
            }
            else
            {
                Debug.LogWarning("Renderer bileþeni bulunamadý!");
            }
        }
    }

    public void ResetBuildState()
    {
        objeYerlestirildi = false;
    }
}
