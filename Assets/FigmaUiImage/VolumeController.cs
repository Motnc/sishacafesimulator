using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public AudioMixer audioMixer;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Ba�lang��ta varsay�lan de�er verelim (�rne�in %100)
        SetVolume(volumeSlider.value);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);

        // dB'yi 0�100'e d�n��t�r (linear format)
        float linear = Mathf.InverseLerp(-80f, 0f, value); // 0-1 aras�
        int displayValue = Mathf.RoundToInt(linear * 100); // 0-100 aras�

        volumeValueText.text = displayValue.ToString() + "%";
    }
}
