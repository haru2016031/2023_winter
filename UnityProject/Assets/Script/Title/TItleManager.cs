using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string targetSceneName; // 遷移先のシーン名

    public GameObject player;
    private Animator playerAnim;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
    }

    void Update()
    {
        // スペースキーが押されたらシーンを遷移する
        if (Input.anyKeyDown)
        {
            // シーンを遷移する
            playerAnim.enabled = false;

            Invoke("LoadScene", 1.5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
