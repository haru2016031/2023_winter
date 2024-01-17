using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private GameObject player;
    private Animator playerAnim;
    public AudioClip playeVoice;
    AudioSource audioSource;
    public Animator transitionAnimator;
    [SerializeField]
    private Image Image;

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
        StartCoroutine(Transition());

        SceneManager.LoadScene(1);
    }
    IEnumerator Transition()
    {
        Image.enabled = true;
        transitionAnimator.SetTrigger("Start"); // �t�F�[�h�C���A�j���[�V�����̃g���K�[���Z�b�g
        yield return new WaitForSeconds(1);    // �t�F�[�h�C���A�j���[�V�����̎��ԑ҂�
        SceneManager.LoadScene(1);     // Scene�����[�h
    }

}
