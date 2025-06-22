using UnityEngine;
using UnityEngine.Events;

public class TrashBin : MonoBehaviour, IUsable
{
    [SerializeField] private UnityEvent onUse;
    public UnityEvent OnUse => onUse;

    public void Use(GameObject actor)
    {
        PickUp pickUp = actor.GetComponent<PickUp>();
        if (pickUp == null) return;

        GameObject heldItem = pickUp.GetHeldItem();
        if (heldItem != null)
        {
            Destroy(heldItem);
            pickUp.ForceClearHand();
            Debug.Log("Çöp kutusu: Elindeki eþya yok edildi.");
        }
        else
        {
            Debug.Log("Çöp kutusu: Elde eþya yok.");
        }

        onUse?.Invoke();
    }

}
