using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseButton : MonoBehaviour
{
    //�@�Q�[���ĊJ�{�^��
    [SerializeField]
    private GameObject ToTitleButton;
    public void ToTitle()
    {
        Debug.Log("�N���b�N");
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
