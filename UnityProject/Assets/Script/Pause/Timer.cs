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
        // ���ɃC���X�^���X�����݂���ꍇ�͎�����j������
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // �V�[���J�ڎ��ɔj������Ȃ��悤�ɂ���
        DontDestroyOnLoad(gameObject);

        // �V���O���g���p�^�[���ŗB��̃C���X�^���X��ݒ�
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
