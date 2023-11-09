using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string targetSceneName; // 遷移先のシーン名

    private GameObject player;
    private Animator playerAnim;
    public AudioClip playeVoice;
    AudioSource audioSource;
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // スペースキーが押されたらシーンを遷移する
        if (Input.anyKeyDown)
        {
            audioSource.PlayOneShot(playeVoice);

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
