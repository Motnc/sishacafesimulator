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
            Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Food>())
            {
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }
            if (hit.collider.GetComponent<Item>())
            {
                Debug.Log("Kullanýldý!");
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.SetParent(pickUpParent.transform, true);
                if (rb != null)
                {
                    rb.isKinematic = true;
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
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(playerCameraTransform.forward * throwForce, ForceMode.Impulse);
            }
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
