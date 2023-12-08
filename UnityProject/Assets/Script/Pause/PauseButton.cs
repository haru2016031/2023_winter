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

    private GameObject exitInstance;

    private Player playerComponent;

    private Vector3 defPos = new Vector3(-15.0f, -3.0f, -14.0f);

    public void ResetPlayer()
    {
        GameObject playerController = GameObject.FindWithTag("Player");
        playerComponent = playerController.GetComponent<Player>();
        Rigidbody playerRb = playerController.GetComponent<Rigidbody>();

        // �v���C���[���������W�ɖ߂�
        Time.timeScale = 1f;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;

        playerComponent.transform.position = defPos;
        playerRb.Sleep(); // Rigidbody�̉^�����~

        Cursor.lockState = CursorLockMode.Locked;
        Destroy(this.gameObject);

    }
    public void ExitCanvas()
    {
        GameObject pauseSceneObject = GameObject.FindWithTag("MainCamera");
        exitInstance = Instantiate(exitPrefab);
        pauseSceneObject.GetComponent<PauseScene>().SetExitUI(exitInstance);
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
