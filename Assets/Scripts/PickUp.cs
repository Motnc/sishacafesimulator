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

    [Header("Preview")]
    [SerializeField] private Material previewMaterial;

    private GameObject inHandItem;
    private GameObject previewObject;
    private bool isPreviewing = false;

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

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (!isPreviewing && inHandItem == null)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
            {
                hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            }
        }

        if (isPreviewing && previewObject != null)
        {
            Ray ray = playerCameraTransform.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 10f))
            {
                previewObject.transform.position = hitInfo.point;
            }
        }
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
        if (inHandItem == null) return;

        if (!isPreviewing)
        {
            StartPreview();
        }
        else
        {
            PlaceObject();
        }
    }

    private void StartPreview()
    {
        if (inHandItem == null) return;

        previewObject = Instantiate(inHandItem);
        DestroyImmediate(previewObject.GetComponent<Rigidbody>());
        DestroyImmediate(previewObject.GetComponent<Collider>());

        // Material deðiþimi
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers)
        {
            rend.material = previewMaterial;
        }

        isPreviewing = true;
    }

    private void PlaceObject()
    {
        if (previewObject == null || inHandItem == null) return;

        GameObject placedObj = Instantiate(inHandItem);
        placedObj.transform.position = previewObject.transform.position;
        placedObj.transform.rotation = previewObject.transform.rotation;

        var rb = placedObj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        var col = placedObj.GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        Destroy(previewObject);
        previewObject = null;
        isPreviewing = false;

        Destroy(inHandItem);
        inHandItem = null;
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

    public GameObject GetHeldItem()
    {
        return inHandItem;
    }

    public Transform GetCameraTransform()
    {
        return playerCameraTransform;
    }
}
