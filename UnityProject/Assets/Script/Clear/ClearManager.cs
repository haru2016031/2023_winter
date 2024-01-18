using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClearManager: MonoBehaviour
{
    [SerializeField]
    private Text clearTimeText;
    [SerializeField]
    private  float delayTime = 1.5f;
    [SerializeField]
    private Animator transitionAnimator;
    [SerializeField]
    private Image image;
    private bool ableFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Timeアクティブ確認→delayTime秒経過→フェードアウト→TitleScene遷移
        if (clearTimeText.enabled && !ableFlag)
        {
            ableFlag = true;
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
        transitionAnimator.SetTrigger("FadeOut"); // フェードインアニメーションのトリガーをセット
        yield return new WaitForSeconds(delayTime);    // フェードインアニメーションの時間待ち
        SceneManager.LoadScene(0);     // Sceneをロード

    }
}
