using UnityEngine;

public class SlotPoint : MonoBehaviour
{
    public bool IsAvailable => transform.childCount == 0;

    public void PlaceItem(GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public GameObject GetItem()
    {
        return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
    }

    public void RemoveItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
