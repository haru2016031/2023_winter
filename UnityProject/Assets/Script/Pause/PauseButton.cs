using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseButton : MonoBehaviour
{
    //　ゲーム再開ボタン
    [SerializeField]
    private GameObject ToTitleButton;
    public void ToTitle()
    {
        Debug.Log("クリック");
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
