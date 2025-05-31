using UnityEngine;
using Unity.Cinemachine;

[AddComponentMenu("Cinemachine/Extensions/Cinemachine Pan Tilt")]
[ExecuteAlways]
[SaveDuringPlay]
[DisallowMultipleComponent]
public class CinemachinePanTilt : CinemachineExtension
{
    public Vector2 sensitivity = new Vector2(100f, 100f);
    public Vector2 clampAngle = new Vector2(-80f, 80f);
    private Vector2 rotation;

    protected override void Awake()
    {
        base.Awake();
        rotation = Vector2.zero;
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (!Application.isPlaying || stage != CinemachineCore.Stage.Aim)
            return;

        Vector2 input = GetInput();

        rotation.x += input.x * sensitivity.x * deltaTime;
        rotation.y -= input.y * sensitivity.y * deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, clampAngle.x, clampAngle.y);

        Quaternion q = Quaternion.Euler(rotation.y, rotation.x, 0);
        state.RawOrientation = q;
    }

    private Vector2 GetInput()
    {
        // Yeni Input System'a uygun mouse delta
        if (UnityEngine.InputSystem.Mouse.current != null)
        {
            var delta = UnityEngine.InputSystem.Mouse.current.delta.ReadValue();
            return new Vector2(delta.x, delta.y);
        }

        // Fallback: klasik Input için (eski sistem)
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
