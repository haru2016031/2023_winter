using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryCount : MonoBehaviour
{
    public Text retryCntText;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        if(timer != null && retryCntText != null)
        {
            float num = timer.GetRetryCnt();
            retryCntText.text = num.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
