using System.Collections;
using System.Collections.Generic;
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
                // Gerçek objeyi instantiate et
                Instantiate(Objects[currentIndex], CurrentObject.transform.position, CurrentObject.transform.rotation);

                objeYerlestirildi = true;

                // Preview objeyi yok et
                Destroy(CurrentObject);
                CurrentObject = null;
            }
        }
    }

    public void SetCurrentObject(int index)
    {
        if (index >= 0 && index < ObjectsPreview.Length && !objeYerlestirildi)
        {
            // Önce var olan preview objeyi yok et
            if (CurrentObject != null)
            {
                Destroy(CurrentObject);
            }

            currentIndex = index;

            // Prefab'ý sahneye instantiate et
            CurrentObject = Instantiate(ObjectsPreview[index]);

            // Emin olmak için aktif hale getir
            CurrentObject.SetActive(true);

            // Artýk sahnede olduðu için Renderer.material eriþimi güvenli!
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
}
