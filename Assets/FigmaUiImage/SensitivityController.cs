using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivityController : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;
    public MouseLook mouseLook;

    void Start()
    {
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        float savedSensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
        sensitivitySlider.value = savedSensitivity;

        SetSensitivity(savedSensitivity); // texti de güncellesin
    }

    public void SetSensitivity(float value)
    {
        mouseLook.SetSensitivity(value);
        PlayerPrefs.SetFloat("sensitivity", value);

        // Virgülden sonra 2 basamakla göster
        sensitivityValueText.text = value.ToString("F2");
    }
}
