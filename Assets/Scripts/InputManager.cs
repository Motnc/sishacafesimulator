using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput PlayerInput;

    public Vector2 Move {get; private set;}
    public Vector2 Look { get; private set;}
    public bool Run { get; private set;}

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;

    private void Awake()
    {
        _currentMap = PlayerInput.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
        _lookAction = _currentMap.FindAction("Look");
        _runAction = _currentMap.FindAction("Run");

        _moveAction.performed += onMove;
        _lookAction.performed += onLook;
        _runAction.performed += onRun;

        _moveAction.canceled += onMove;
        _lookAction.canceled += onLook;
        _runAction.canceled += onRun;

    }

    private void onMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
    private void onLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }
    private void onRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        _currentMap.Enable();
    }

    private void OnDisable()
    {
        _currentMap.Disable();
    }

}
