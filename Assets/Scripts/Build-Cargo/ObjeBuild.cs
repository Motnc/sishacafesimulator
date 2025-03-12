using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeBuild : MonoBehaviour
{
    public GameObject[] Objects;
    public GameObject[] ObjectsPreview;

    private bool ObjeOlusturulabilirmi = false;
    public bool EtkilesimVarmi = false;
    public GameObject CurrentObject;
    private int currentIndex = -1; // Baþlangýçta hiçbir obje seçili deðil

    void Update()
    {
        if (CurrentObject == null) return; // Eðer herhangi bir obje seçili deðilse iþlem yapma

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
            }
        }
    }

    public void SetCurrentObject(int index)
    {
        if (index >= 0 && index < ObjectsPreview.Length)
        {
            if (CurrentObject != null)
            {
                CurrentObject.SetActive(false);
            }

            currentIndex = index;
            CurrentObject = ObjectsPreview[index];
            CurrentObject.SetActive(true);

            // Önizleme rengini deðiþtir
            Renderer objRenderer = CurrentObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = Color.green;
            }
        }
    }
}
