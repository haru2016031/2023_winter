using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string targetSceneName; // �J�ڐ�̃V�[����

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
        // �X�y�[�X�L�[�������ꂽ��V�[����J�ڂ���
        if (Input.anyKeyDown)
        {
            audioSource.PlayOneShot(playeVoice);

            // �V�[����J�ڂ���
            playerAnim.enabled = false;

            Invoke("LoadScene", 1.5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
