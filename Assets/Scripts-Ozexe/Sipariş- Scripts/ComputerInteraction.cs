using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class ComputerInteraction : MonoBehaviour
{
    public CinemachineCamera computerCamera;
    public GameObject computerCanvas;
    public PlayerInput playerInput;

    public Transform playerTransform;         // Karakterin Transform'u atanmalý
    public float interactDistance = 3f;       // Etkileþim mesafesi

    private bool isUsingComputer = false;

    void Start()
    {
        computerCanvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (Keyboard.current.eKey.wasPressedThisFrame && !isUsingComputer && distance <= interactDistance)
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
    }

    void ExitComputer()
    {
        isUsingComputer = false;
        computerCamera.Priority = 5;
        computerCanvas.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }
}

