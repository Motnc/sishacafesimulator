using UnityEngine;

public class MasaEXP : MonoBehaviour
{
    public bool IsEmpty = true;
    public SeatEXP[] seats;
    public Transform table;
    public Transform exitPoint;
    public GameObject moneyPrefab;
    public Transform moneySpawnPoint;

    private void Start()
    {
        // Eðer sahnede MasaManager varsa kendini ekler
        if (MasaManagerEXP.Instance != null && !MasaManagerEXP.Instance.masalar.Contains(this))
        {
            MasaManagerEXP.Instance.masalar.Add(this);
            Debug.Log($"{name} masasý MasaManager’a eklendi.");
        }
    }
}
