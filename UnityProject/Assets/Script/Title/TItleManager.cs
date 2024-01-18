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
        fadeInAnimator.SetTrigger("FadeIn"); // フェードインアニメーションのトリガーをセット

    }

    void Update()
    {
        // スペースキーが押されたらシーンを遷移する
        if (Input.anyKeyDown)
        {
            audioSource.PlayOneShot(playeVoice);

            // シーンを遷移する
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
        fadeOutAnimator.SetTrigger("FadeOut"); // フェードインアニメーションのトリガーをセット
        yield return new WaitForSeconds(delayTime);    // フェードインアニメーションの時間待ち
        SceneManager.LoadScene(1);     // Sceneをロード
    }

}
