using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public float timer;
    private static Timer instance;
    private bool timerRunning = true;

    private void Awake()
    {
        // 既にインスタンスが存在する場合は自分を破棄する
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // シーン遷移時に破棄されないようにする
        DontDestroyOnLoad(gameObject);

        // シングルトンパターンで唯一のインスタンスを設定
        instance = this;
    }
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        timer = 0;
    }

    public void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            var time = timer;
            int minutes = Mathf.FloorToInt(time / 60.0f);
            int seconds = Mathf.FloorToInt(time % 60.0f);
            if (timerText)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }

    public float GetTimer()
    {
        return timer;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
