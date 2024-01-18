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
        //Time�A�N�e�B�u�m�F��delayTime�b�o�߁��t�F�[�h�A�E�g��TitleScene�J��
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
        transitionAnimator.SetTrigger("FadeOut"); // �t�F�[�h�C���A�j���[�V�����̃g���K�[���Z�b�g
        yield return new WaitForSeconds(delayTime);    // �t�F�[�h�C���A�j���[�V�����̎��ԑ҂�
        SceneManager.LoadScene(0);     // Scene�����[�h

    }
}
