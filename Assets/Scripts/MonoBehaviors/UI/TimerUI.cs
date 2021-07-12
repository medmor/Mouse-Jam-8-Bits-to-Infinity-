using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TimerText;
    private float Time = 0;
    Coroutine coroutine;

    public void Show()
    {
        gameObject.SetActive(true);
        coroutine = StartCoroutine(TimerCoroutine());
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    public string GetTime()
    {
        return TimerText.text;
    }
    public void SetTime(float deltaTime)
    {
        Time += deltaTime;
        System.TimeSpan t = System.TimeSpan.FromSeconds(Time);

        TimerText.text = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);
    }
    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            SetTime(1);
            yield return new WaitForSeconds(1f);
        }
    }
    public void ResetTimer()
    {
        Time = 0;
        TimerText.text = "0";
    }
}
