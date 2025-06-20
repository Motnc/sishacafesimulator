using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    [System.Serializable]
    public class CustomPickUp
    {
        public string objectName;
        public Transform customParent;
    }

    [Header("Ayarlar")]
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private float hitRange = 3f;
    [SerializeField] private float throwForce;

    [Header("Elde Taþýma Noktalarý")]
    [SerializeField] private Transform defaultPickUpParent;
    [SerializeField] private List<CustomPickUp> customPickUpSettings;

    [Header("Inputlar")]
    [SerializeField] private InputActionReference interactionInput, dropInput, useInput;

    private GameObject inHandItem;
    private RaycastHit hit;

    private void Start()
    {
        interactionInput.action.Enable();
        dropInput.action.Enable();
        useInput.action.Enable();

        interactionInput.action.performed += Interact;
        dropInput.action.performed += Drop;
        useInput.action.performed += Use;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (inHandItem != null) return;

        if (hit.collider != null)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Item>() || hit.collider.GetComponent<KargoDizim>())
            {
                inHandItem = hit.collider.gameObject;

                Transform targetParent = GetCustomParent(inHandItem.name);
                inHandItem.transform.SetParent(targetParent, false);
                inHandItem.transform.localPosition = Vector3.zero;
                inHandItem.transform.localRotation = Quaternion.identity;

                if (rb != null)
                    rb.isKinematic = true;

                Collider col = inHandItem.GetComponent<Collider>();
                if (col != null)
                    col.enabled = false;

                return;
            }
        }
    }

    private Transform GetCustomParent(string objectName)
    {
        foreach (var custom in customPickUpSettings)
        {
            if (objectName.Contains(custom.objectName))
                return custom.customParent;
        }
        return defaultPickUpParent;
    }

    private void Use(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                usable.Use(this.gameObject);
            }
        }
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            Collider col = inHandItem.GetComponent<Collider>();

            if (col != null)
                col.enabled = true;

            inHandItem.transform.SetParent(null);
            inHandItem.transform.position = playerCameraTransform.position + playerCameraTransform.forward * 1f;
            inHandItem.transform.localScale = Vector3.one; //  scale düzeltme

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(playerCameraTransform.forward * throwForce, ForceMode.Impulse);
            }

            inHandItem = null;
        }
    }


    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (inHandItem != null) return;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
        }
    }

    public GameObject GetHeldItem()
    {
        return inHandItem;
    }

    public Transform GetCameraTransform()
    {
        return playerCameraTransform;
    }


}
