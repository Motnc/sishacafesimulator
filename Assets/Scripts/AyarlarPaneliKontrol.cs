using UnityEngine;

public class AyarlarPaneliKontrol : MonoBehaviour
{
    public GameObject AyarlarPaneli;

    public void AyarButonunaTiklandi()
    {
        if (AyarlarPaneli != null)
        {
            AyarlarPaneli.SetActive(!AyarlarPaneli.activeSelf);
        }
    }
}
