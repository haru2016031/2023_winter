using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationCtl : MonoBehaviour
{
    private Animator animator;
    CameraMove cameraMove;
    private void Start()
    {
        // アニメーションの取得
        animator = GetComponent<Animator>();

        cameraMove =  GetComponent<CameraMove>();

        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            PlayAnimation("Drink");
        }
        else if(SceneManager.GetActiveScene().name == "ClearScene")
        {
            PlayAnimation("Sleep");
        }
    }

    private void Update()
    {

    }

    void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
