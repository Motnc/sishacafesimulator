using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool IsOccupied { get; private set; } = false;

    public void Occupy()
    {
        IsOccupied = true;
    }

    public void Free()
    {
        IsOccupied = false;
    }
}
