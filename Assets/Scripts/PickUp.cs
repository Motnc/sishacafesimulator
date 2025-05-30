using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private float throwForce;

    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;


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
        if (inHandItem != null)
        {
            return;
        }

        if (hit.collider != null)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Item>())
            {
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                inHandItem.transform.localPosition = Vector3.zero;
                inHandItem.transform.localRotation = Quaternion.identity;

                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                // Collider'ý devre dýþý býrak
                Collider col = inHandItem.GetComponent<Collider>();
                if (col != null)
                {
                    col.enabled = false;
                }

                return;
            }
        }
    }


    private void Use(InputAction.CallbackContext obj)
    {

    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            Collider col = inHandItem.GetComponent<Collider>();

            // Collider'ý yeniden etkinleþtir
            if (col != null)
            {
                col.enabled = true;
            }

            inHandItem.transform.SetParent(null);

            // Kutuyu biraz ileri taþý, çakýþmayý önlemek için
            inHandItem.transform.position = playerCameraTransform.position + playerCameraTransform.forward * 1f;

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

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
        }
    }
}