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

        // Sahnedeki tüm masalarý bul
        masalar.AddRange(FindObjectsOfType<MasaEXP>());
        Debug.Log("Masalar bulundu! Sayý: " + masalar.Count);
    }

    // Boþ masa bul
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
