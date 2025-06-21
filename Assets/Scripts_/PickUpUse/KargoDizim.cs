using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KargoDizim : MonoBehaviour, IUsable
{
    public List<GameObject> itemsToPlace = new List<GameObject>();
    public List<Transform> kargoSlotPoints = new List<Transform>();
    public UnityEvent OnUse { get; } = new UnityEvent();

    private Queue<GameObject> camDolaptanGeriAlinacaklar = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < itemsToPlace.Count && i < kargoSlotPoints.Count; i++)
        {
            GameObject item = itemsToPlace[i];
            Transform slot = kargoSlotPoints[i];

            item.transform.SetParent(slot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            Collider col = item.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }


    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            if (camDolaptanGeriAlinacaklar.Count == 0)
            {
                Ray ray = new Ray(FindObjectOfType<PickUp>().GetCameraTransform().position,
                                  FindObjectOfType<PickUp>().GetCameraTransform().forward);

                if (Physics.Raycast(ray, out RaycastHit hit, 3f))
                {
                    if (hit.collider.CompareTag("CamDolap"))
                    {
                        CamDolap dolap = hit.collider.GetComponent<CamDolap>();
                        if (dolap != null)
                        {
                            List<GameObject> items = dolap.GetAllPlacedItems();
                            foreach (GameObject item in items)
                            {
                                camDolaptanGeriAlinacaklar.Enqueue(item);
                            }
                        }
                    }
                }
            }

            if (camDolaptanGeriAlinacaklar.Count > 0)
            {
                GameObject item = camDolaptanGeriAlinacaklar.Dequeue();
                CamDolap camDolap = item.transform.parent?.GetComponentInParent<CamDolap>();
                if (camDolap != null)
                {
                    camDolap.RemoveItem(item);
                }

                itemsToPlace.Add(item);

                int slotIndex = itemsToPlace.Count - 1;
                Transform slot = (slotIndex < kargoSlotPoints.Count) ? kargoSlotPoints[slotIndex] : this.transform;

                item.transform.SetParent(slot);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                var rb = item.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                var col = item.GetComponent<Collider>();
                if (col != null) col.enabled = false;

                item.SetActive(true);
            }
        }
    }

    public void Use(GameObject actor)
    {
        var cameraTransform = actor.GetComponent<PickUp>()?.GetCameraTransform();
        if (cameraTransform == null) return;

        if (itemsToPlace.Count == 0) return;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("CamDolap"))
            {
                CamDolap dolap = hit.collider.GetComponent<CamDolap>();
                if (dolap != null)
                {
                    GameObject item = itemsToPlace[0];

                    bool success = dolap.PlaceItem(item);
                    if (success)
                    {
                        itemsToPlace.RemoveAt(0);
                    }
                }
            }
        }

        OnUse.Invoke();
    }
}
