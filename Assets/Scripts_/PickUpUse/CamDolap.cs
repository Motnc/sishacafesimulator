using System.Collections.Generic;
using UnityEngine;

public class CamDolap : MonoBehaviour
{
    public List<Transform> slotPoints;
    private int currentSlotIndex = 0;

    public bool PlaceItem(GameObject item)
    {
        if (currentSlotIndex >= slotPoints.Count)
        {
            Debug.LogWarning("CamDolap'ta boþ slot kalmadý!");
            return false;
        }

        Transform targetSlot = slotPoints[currentSlotIndex];

        item.transform.SetParent(targetSlot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        currentSlotIndex++;
        return true;
    }

    public List<GameObject> GetAllPlacedItems()
    {
        List<GameObject> items = new List<GameObject>();

        foreach (Transform slot in slotPoints)
        {
            if (slot.childCount > 0)
            {
                items.Add(slot.GetChild(0).gameObject);
            }
        }

        return items;
    }

    public void RemoveItem(GameObject item)
    {
        Transform parent = item.transform.parent;
        item.transform.SetParent(null);

        // Slot geri alýnsýn
        int index = slotPoints.IndexOf(parent);
        if (index >= 0 && index < currentSlotIndex)
        {
            currentSlotIndex = index;
        }
    }
}
