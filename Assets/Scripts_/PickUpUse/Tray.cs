using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IUsable
{
    [Header("Tepsideki Slotlar")]
    [SerializeField] private List<Transform> foodSlots;

    private int currentSlotIndex = 0;

    public void Use(GameObject actor)
    {
        PickUp pickUp = actor.GetComponent<PickUp>();
        if (pickUp == null) return;

        if (pickUp.GetHeldItem() != this.gameObject)
        {
            Debug.Log("Tepsi elde deðil, iþlem iptal.");
            return;
        }

        Transform cam = pickUp.GetCameraTransform();

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 5f))
        {
            GameObject lookedObj = hit.collider.gameObject;

            Debug.Log($"Raycast vurdu: {lookedObj.name}, Tag: {lookedObj.tag}");

            // Eðer vurulan objenin tag'ý NPC deðilse parent kontrol et
            if (!lookedObj.CompareTag("NPC") && lookedObj.transform.parent != null)
            {
                if (lookedObj.transform.parent.CompareTag("NPC"))
                {
                    lookedObj = lookedObj.transform.parent.gameObject;
                    Debug.Log($"Parent NPC bulundu: {lookedObj.name}");
                }
            }

            FoodItem food = lookedObj.GetComponent<FoodItem>();

            if (lookedObj.CompareTag("NPC"))
            {
                CustomerCollisionHandlerEXP customer = lookedObj.GetComponent<CustomerCollisionHandlerEXP>();
                if (customer == null)
                {
                    Debug.LogWarning("NPC tag'lý objede CustomerCollisionHandlerEXP bileþeni yok.");
                    return;
                }

                if (currentSlotIndex == 0)
                {
                    Debug.Log("Tepsi boþ, teslim edilecek yemek yok.");
                    return;
                }

                if (!IsOrderActive(customer))
                {
                    Debug.Log("Sipariþ aktif deðil, teslim edilmedi.");
                    return;
                }

                for (int i = 0; i < currentSlotIndex; i++)
                {
                    if (foodSlots[i].childCount > 0)
                    {
                        GameObject foodObj = foodSlots[i].GetChild(0).gameObject;

                        customer.DeliverFood(foodObj);

                        foodObj.transform.SetParent(null);
                        Destroy(foodObj);
                    }
                }

                currentSlotIndex = 0;

                Debug.Log("Tüm yemekler müþteriye teslim edildi.");
                return;
            }

            if (food != null)
            {
                if (currentSlotIndex >= foodSlots.Count)
                {
                    Debug.Log("Tepsi dolu, yemek eklenemiyor.");
                    return;
                }

                Transform targetSlot = foodSlots[currentSlotIndex];

                Vector3 originalScale = food.transform.localScale;

                food.transform.SetParent(targetSlot);
                food.transform.localPosition = Vector3.zero;
                food.transform.localRotation = Quaternion.identity;

                food.transform.localScale = originalScale;

                Rigidbody rb = food.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                Collider col = food.GetComponent<Collider>();
                if (col != null) col.enabled = false;

                currentSlotIndex++;

                if (pickUp.GetHeldItem() == food.gameObject)
                {
                    pickUp.ForceClearHand();
                }

                Debug.Log($"Yemek {food.name} tepsiye kondu. Slot {currentSlotIndex}/{foodSlots.Count}");
                return;
            }

            Debug.Log("Bakýlan obje yemek veya NPC deðil, iþlem yapýlmadý.");
        }
        else
        {
            Debug.Log("Raycast hiçbir objeye vurmadý.");
        }
    }

    public UnityEngine.Events.UnityEvent OnUse => null;

    private bool IsOrderActive(CustomerCollisionHandlerEXP customer)
    {
        var controller = customer.GetComponent<CustomerFSMControllerEXP>();
        if (controller != null)
        {
            return !controller.Data.IsOrderDelivered;
        }
        return false;
    }
}
