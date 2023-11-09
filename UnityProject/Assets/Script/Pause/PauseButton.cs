using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseButton : MonoBehaviour
{
    //　ゲーム再開ボタン
    [SerializeField]
    private GameObject toTitleButton;

    [SerializeField]
    private GameObject exitPrefab;

    public GameObject exitInstance;
    public bool CancelFlag = false;


    public void ExitCanvas()
    {
        if (exitInstance == null)
        {
            exitInstance = GameObject.Instantiate(exitPrefab) as GameObject;
        }
    }

    public void ToTitle()
    {
        Debug.Log("クリック");

        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");

    }

    public void CancelToTitle()
    {
        CancelFlag = true;
        Debug.Log("クリック");
        //Time.timeScale = 1f;
       Destroy(exitInstance);
    }
}
