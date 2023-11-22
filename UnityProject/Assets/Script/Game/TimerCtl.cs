using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCtl : MonoBehaviour
{
    void Start()
    {
        var obj = FindObjectOfType<Timer>();
        obj.timerText = GetComponent<Text>();
    }
}
