using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public string itemName; // "Masa", "Sandalye", "Nargile"

    public void Artir()
    {
        EventManager.OnItemQuantityChanged?.Invoke(itemName, +1);
    }

    public void Azalt()
    {
        EventManager.OnItemQuantityChanged?.Invoke(itemName, -1);
    }
}
