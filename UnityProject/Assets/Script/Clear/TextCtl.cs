using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextCtl : MonoBehaviour
{
    public Text[] textList;
    public GameObject player;
    public AudioClip audioClip;
    public AudioClip bgmClip;
    public bool sceneFlag = false;
    private Animator animator;
    private AudioSource audioSource;
    private AudioSource bgm;
    [SerializeField]
    private string viewTextAnim = "WakeUp";
    [SerializeField]
    private float delayTime = 1.0f;
    private bool hasAnimationEnded = false;


    void Start()
    {
        foreach(var text in textList)
        {
            text.enabled = false;
        }
        animator = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //wakeup���I�������e�L�X�g�\���t���O��true
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(viewTextAnim)&&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && !hasAnimationEnded)
        {
            hasAnimationEnded = true;
            StartCoroutine(EnableText(delayTime));
        }
   }

    IEnumerator EnableText(float time)
    {
        foreach (var text in textList)
        {
            yield return new WaitForSeconds(time);  // 1�b�̒x��

            text.enabled = true;
            audioSource.PlayOneShot(audioClip);
        }
        yield return new WaitForSeconds(3.0f);  // 1�b�̒x��
        sceneFlag = true;
    }
}
