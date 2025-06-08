using UnityEngine;

public class AyarlarPaneliKontrol : MonoBehaviour
{
    public GameObject AyarlarPaneli;

    public GameObject SesPaneli;
    public GameObject OynanisPaneli;

    public void AyarButonunaTiklandi()
    {
        if (AyarlarPaneli != null)
        {
            AyarlarPaneli.SetActive(!AyarlarPaneli.activeSelf);
        }
    }

    public void SesButonunaTiklandi()
    {
        if (SesPaneli != null && OynanisPaneli != null)
        {
            bool yeniDurum = !SesPaneli.activeSelf;
            SesPaneli.SetActive(yeniDurum);
            OynanisPaneli.SetActive(false);
        }
    }

    public void OynanisButonunaTiklandi()
    {
        if (SesPaneli != null && OynanisPaneli != null)
        {
            bool yeniDurum = !OynanisPaneli.activeSelf;
            OynanisPaneli.SetActive(yeniDurum);
            SesPaneli.SetActive(false);
        }
    }
}
