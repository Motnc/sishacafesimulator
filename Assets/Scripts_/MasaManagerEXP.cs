using System.Collections.Generic;
using UnityEngine;

public class MasaManagerEXP : MonoBehaviour
{
    public static MasaManagerEXP Instance { get; private set; }

    [HideInInspector] public List<MasaEXP> masalar = new List<MasaEXP>();

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Sahnedeki t�m masalar� bul
        masalar.AddRange(FindObjectsOfType<MasaEXP>());
        Debug.Log("Masalar bulundu! Say�: " + masalar.Count);
    }

    // Bo� masa bul
    public MasaEXP GetEmptyMasa()
    {
        foreach (var masa in masalar)
        {
            if (masa != null && masa.IsEmpty)
                return masa;
        }
        return null;
    }
}
