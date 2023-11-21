using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float timer;

    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer % 60.0f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log(timerText.text);
        Debug.Log(timer);
    }
}
