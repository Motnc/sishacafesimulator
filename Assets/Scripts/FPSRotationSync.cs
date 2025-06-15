using UnityEngine;

public class FPSRotationSync : MonoBehaviour
{
    public Transform cameraTransform;  // IdleCamera'nýn transform'u
    public Transform characterBody;    // Player karakterin ana objesi

    void LateUpdate()
    {
        // Sadece Y ekseninde döndür (saða-sola)
        Vector3 camEuler = cameraTransform.rotation.eulerAngles;
        characterBody.rotation = Quaternion.Euler(0, camEuler.y, 0);
    }
}
