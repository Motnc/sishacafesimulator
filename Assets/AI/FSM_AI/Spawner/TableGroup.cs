using UnityEngine;

[System.Serializable]
public class TableGroup
{
    public Transform table;
    public Seat[] seats = new Seat[4];
    public Transform moneySpawnPoint;
    public TableMode mode = TableMode.Normal;

    public void ToggleMode()
    {
        mode = (mode == TableMode.Normal) ? TableMode.Gambling : TableMode.Normal;
        Debug.Log("Table mode changed to: " + mode);
    }
}
