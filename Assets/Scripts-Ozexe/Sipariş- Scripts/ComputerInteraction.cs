using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class ComputerInteraction : MonoBehaviour
{
    public CinemachineCamera computerCamera;
    public GameObject computerCanvas;
    public PlayerInput playerInput;
    public GameObject crosshairUI;

    public Transform player;
    public float interactionDistance = 2.5f;

    private bool isUsingComputer = false;

    void Start()
    {
        computerCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (crosshairUI != null)
            crosshairUI.SetActive(true);
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && !isUsingComputer && IsNearComputer())
        {
            EnterComputer();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && isUsingComputer)
        {
            ExitComputer();
        }
    }

    void EnterComputer()
    {
        isUsingComputer = true;
        computerCamera.Priority = 20;
        computerCanvas.SetActive(true);
        playerInput.SwitchCurrentActionMap("Computer");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (crosshairUI != null)
            crosshairUI.SetActive(false);
    }

    void ExitComputer()
    {
        isUsingComputer = false;
        computerCamera.Priority = 5;
        computerCanvas.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (crosshairUI != null)
            crosshairUI.SetActive(true);
    }

    bool IsNearComputer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        return distance <= interactionDistance;
    }
}


