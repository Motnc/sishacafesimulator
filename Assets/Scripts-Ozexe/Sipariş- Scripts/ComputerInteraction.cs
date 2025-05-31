using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class ComputerInteraction : MonoBehaviour
{
    public CinemachineCamera computerCamera;
    public GameObject computerCanvas;
    public PlayerInput playerInput;

    private bool isUsingComputer = false;



    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!isUsingComputer)
                EnterComputer();
            else
                ExitComputer();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame && isUsingComputer)
        {
            ExitComputer();
        }
    }

    void EnterComputer()
    {
        isUsingComputer = true;
        computerCamera.Priority = 10;
        computerCanvas.SetActive(true);
        playerInput.SwitchCurrentActionMap("Computer");
    }

    void ExitComputer()
    {
        isUsingComputer = false;
        computerCamera.Priority = 1;
        computerCanvas.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }

    void SetCanvasFullScreen()
    {
        if (computerCanvas != null)
        {
            RectTransform rt = computerCanvas.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
        }
    }
}
