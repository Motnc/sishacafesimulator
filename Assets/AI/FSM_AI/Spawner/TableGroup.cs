using UnityEngine;

[System.Serializable]
public class TableGroup
{
    public Transform table;
    public Seat[] seats = new Seat[4]; 
    public Transform moneySpawnPoint;
}
