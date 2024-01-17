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
        StartCoroutine(Transition());

        SceneManager.LoadScene(1);
    }
    IEnumerator Transition()
    {
        Image.enabled = true;
        transitionAnimator.SetTrigger("Start"); // フェードインアニメーションのトリガーをセット
        yield return new WaitForSeconds(1);    // フェードインアニメーションの時間待ち
        SceneManager.LoadScene(1);     // Sceneをロード
    }

}
