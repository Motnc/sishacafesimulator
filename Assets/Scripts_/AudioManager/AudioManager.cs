using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Sound Clips")]
    public AudioClip mopSound;
    public AudioClip moneySound;
    public AudioClip pickUpSound;
    public AudioClip lidSound;
    public AudioClip heaterSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // sahne geçiþinde kaybolmaz
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Genel SFX oynatma
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // Tek tek sesler için özel fonksiyonlar
    public void PlayMopSound() => PlaySFX(mopSound);
    public void PlayMoneySound() => PlaySFX(moneySound);
    public void PlayPickUpSound() => PlaySFX(pickUpSound);
    public void PlayLidSound() => PlaySFX(lidSound);
    public void PlayHeaterSound() => PlaySFX(heaterSound);
}
