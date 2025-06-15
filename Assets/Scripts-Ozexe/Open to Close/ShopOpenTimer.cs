using System.Collections;
using UnityEngine;
using TMPro;

public class ShopOpenTimer : MonoBehaviour
{
    public TMP_Text timerText; // UI �zerindeki geri say�m yaz�s�
    public TMP_Text dayCompleteText; // "1. G�n Tamamland�." yaz�s�
    public float totalTime = 540f; // 9 dakika

    private bool timerRunning = false;

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            StartCoroutine(CountdownTimer());
        }
    }

    IEnumerator CountdownTimer()
    {
        float currentTime = totalTime;
        dayCompleteText.gameObject.SetActive(false);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI(currentTime);
            yield return null;
        }

        timerRunning = false;
        timerText.text = "00:00";
        dayCompleteText.gameObject.SetActive(true);
        dayCompleteText.text = "1. G�n Tamamland�.";
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
