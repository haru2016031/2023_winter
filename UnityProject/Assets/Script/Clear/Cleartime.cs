using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cleartime : MonoBehaviour
{
    public Text clearTimeText;
    private Timer timer;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        if (timer != null && clearTimeText != null)
        {
            float time = timer.GetTimer();
            int minutes = Mathf.FloorToInt(time / 60.0f);
            int seconds = Mathf.FloorToInt(time % 60.0f);
            timer.StopTimer();
            clearTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}