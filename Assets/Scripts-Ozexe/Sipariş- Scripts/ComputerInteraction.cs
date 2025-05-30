using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class ComputerInteraction : MonoBehaviour
{
    public CinemachineCamera computerCamera;
    public GameObject computerCanvas;
    public PlayerInput playerInput;

    private bool isUsingComputer = false;

    void Start()
    {
        computerCanvas.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && !isUsingComputer)
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
        playerInput.SwitchCurrentActionMap("Computer"); // Input Map deðiþtir
    }

    void ExitComputer()
    {
        isUsingComputer = false;
        computerCamera.Priority = 5;
        computerCanvas.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player"); // Input Map geri al
    }
}

