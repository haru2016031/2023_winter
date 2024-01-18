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
    private Image image;
    [SerializeField]
    private float delayTime = 1.5f;

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

            Invoke("LoadScene", delayTime);
        }
    }

    void LoadScene()
    {
        StartCoroutine(Transition());

    }
    IEnumerator Transition()
    {
        image.enabled = true;
        transitionAnimator.SetTrigger("Fade"); // �t�F�[�h�C���A�j���[�V�����̃g���K�[���Z�b�g
        yield return new WaitForSeconds(delayTime);    // �t�F�[�h�C���A�j���[�V�����̎��ԑ҂�
        SceneManager.LoadScene(1);     // Scene�����[�h
    }

}
