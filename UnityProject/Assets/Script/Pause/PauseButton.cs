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

    public void ExitCanvas()
    {
         GameObject.Instantiate(exitPrefab);
    }

    public void ToTitle()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");

    }

    public void CancelToTitle()
    {
        Destroy(this.gameObject);

    }
}
