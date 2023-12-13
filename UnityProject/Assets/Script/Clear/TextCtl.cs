using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtl : MonoBehaviour
{
    public Text clearText;
    public Text clearTimeText;
    public GameObject player;
    public AudioClip audioClip;
    private Animator animator;
    private AudioSource audioSource;
    private bool clearTextSondFlag = false;
    private bool clearTimeSondFlag = false;
    void Start()
    {
        clearText.enabled = false;
        clearTimeText.enabled = false;
        animator = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sway"))
        {
            clearText.enabled = true;

            if(!clearTextSondFlag)
            {
                audioSource.PlayOneShot(audioClip);
                clearTextSondFlag = true;
            }
            StartCoroutine(EnableClearTime(1.0f));
        }
    }

    IEnumerator EnableClearTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        clearTimeText.enabled = true;

        if(!clearTimeSondFlag)
        {
            audioSource.PlayOneShot(audioClip);
            clearTimeSondFlag = true;
        }
    }
}
