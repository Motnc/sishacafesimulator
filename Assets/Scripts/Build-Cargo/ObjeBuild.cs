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
    private int currentIndex = -1; // Ba�lang��ta hi�bir obje se�ili de�il

    void Update()
    {
        if (CurrentObject == null) return; // E�er herhangi bir obje se�ili de�ilse i�lem yapma

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

            // �nizleme rengini de�i�tir
            Renderer objRenderer = CurrentObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = Color.green;
            }
        }
    }
}
