using TMPro;
using UnityEngine;

public class TimerCode : MonoBehaviour
{
    private TextMeshProUGUI timerText;
 
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
 
    public void SetTime(float secondsRemaining)
    {
        if (timerText == null) return;
 
        // never show negative time
        if (secondsRemaining < 0f) secondsRemaining = 0f;
 
        int minutes = Mathf.FloorToInt(secondsRemaining / 60f);
        int seconds = Mathf.FloorToInt(secondsRemaining % 60f);
 
        // "SS" part padded to 2 digits
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

}

