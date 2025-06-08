using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1f;

    float rotationX = 0f;
    float rotationY = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
    }
}
