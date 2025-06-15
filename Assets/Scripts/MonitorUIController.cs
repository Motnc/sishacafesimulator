using UnityEngine;

public class MonitorUIController : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelSiparis;

    public void GecisYap()
    {
        panelMain.SetActive(false);
        panelSiparis.SetActive(true);
    }
}
