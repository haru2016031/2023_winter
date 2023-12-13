using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseButton : MonoBehaviour
{
    //　ゲーム再開ボタン
    [SerializeField]
    private GameObject toTitleButton;

    //初期座標に戻すボタン
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private GameObject exitPrefab;      //タイトルへ戻る画面
    private GameObject exitInstance;
    
    [SerializeField]
    private GameObject settingPrefab;      //設定画面
    private GameObject settingInstance;

    private Player playerComponent;
    private Vector3 defPos = new Vector3(-15.0f, -3.0f, -14.0f);    //プレイヤーの初期値

    public void ResetPlayer()
    {
        GameObject playerController = GameObject.FindWithTag("Player");
        playerComponent = playerController.GetComponent<Player>();
        // プレイヤーを初期座標に戻す
        Time.timeScale = 1f;
        playerComponent.transform.position = defPos;
        Destroy(this.gameObject);
    }
    public void ExitCanvas()
    {
        GameObject pauseSceneObject = GameObject.FindWithTag("MainCamera");
        exitInstance = Instantiate(exitPrefab);
        pauseSceneObject.GetComponent<PauseScene>().SetExitUI(exitInstance);
    }

    public void SettingCanvas()
    {
        GameObject pauseSceneObject = GameObject.FindWithTag("MainCamera");
        settingInstance = Instantiate(settingPrefab);
        pauseSceneObject.GetComponent<PauseScene>().SetSettingUI(settingInstance);
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

    public void DestroyExit()
    {
        Destroy(exitInstance);
    }
}
