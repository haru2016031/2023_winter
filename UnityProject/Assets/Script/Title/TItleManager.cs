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
    public Animator fadeInAnimator;
    public Animator fadeOutAnimator;
    [SerializeField]
    private Image fadeOut;
    [SerializeField]
    private Image fadeIn;
    [SerializeField]
    private float delayTime = 1.5f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        fadeIn.enabled = true;
        fadeInAnimator.SetTrigger("FadeIn"); // �t�F�[�h�C���A�j���[�V�����̃g���K�[���Z�b�g

    }

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��V�[����J�ڂ���
        if (Input.anyKeyDown)
        {
            audioSource.PlayOneShot(playeVoice);

            // �V�[����J�ڂ���
            playerAnim.enabled = false;

            Invoke("LoadScene", delayTime);
        }
    }

    void LoadScene()
    {
        StartCoroutine(Transition());

    }
    IEnumerator Transition()
    {
        fadeOut.enabled = true;
        fadeOutAnimator.SetTrigger("FadeOut"); // �t�F�[�h�C���A�j���[�V�����̃g���K�[���Z�b�g
        yield return new WaitForSeconds(delayTime);    // �t�F�[�h�C���A�j���[�V�����̎��ԑ҂�
        SceneManager.LoadScene(1);     // Scene�����[�h
    }

}
