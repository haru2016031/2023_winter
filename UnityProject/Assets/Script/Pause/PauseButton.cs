using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseButton : MonoBehaviour
{
    //�@�Q�[���ĊJ�{�^��
    [SerializeField]
    private GameObject toTitleButton;

    //�������W�ɖ߂��{�^��
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private GameObject exitPrefab;

    private Player playerComponent;

    private Vector3 defPos = new Vector3(-15.0f, -3.0f, -14.0f);



    public void ResetPlayer()
    {
        GameObject playerController = GameObject.FindWithTag("Player");
        playerComponent = playerController.GetComponent<Player>();
        // �v���C���[���������W�ɖ߂�
        Time.timeScale = 1f;
        playerComponent.transform.position = defPos;
        Destroy(this.gameObject);
    }
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
