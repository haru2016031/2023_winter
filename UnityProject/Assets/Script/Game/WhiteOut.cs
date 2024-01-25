using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WhiteOut : MonoBehaviour
{
    public float fadeTime = 1.0f;
    public Image whitePanel;
    public Canvas canvas;
    private float time;
    private bool whiteOutFlag;      //ホワイトアウトの開始フラグ
    private Timer timer;

    private void Start()
    {
        if (whitePanel == null)
        {
            if (canvas != null)
            {
                whitePanel = canvas.GetComponentInChildren<Image>();
            }
        }

        if (whitePanel != null)
        {
            Color color = whitePanel.color;
            color.a = 0f;
            whitePanel.color = color;
        }
        timer = FindObjectOfType<Timer>();

        timer.Init();
        whiteOutFlag = false;
    }

    private void Update()
    {
        if (whiteOutFlag)
        {
            FadeIn();
        }
    }

    private void FadeIn()
    {
        if (whitePanel != null)
        {
            Color color = whitePanel.color;
            time += Time.deltaTime;
            color.a = time / fadeTime;

            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                enabled = false;

                // ホワイトアウトが完了したら次のシーンへ遷移
                LoadNextScene();
            }

            whitePanel.color = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            whiteOutFlag = true;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }
}
