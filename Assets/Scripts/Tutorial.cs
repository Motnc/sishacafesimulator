using UnityEngine;
using UnityEngine.InputSystem;

public class PanelToggle : MonoBehaviour
{
    public GameObject hedefPanel;
    private bool isOpen = false;

    void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            hedefPanel.SetActive(isOpen);
        }
    }
}


