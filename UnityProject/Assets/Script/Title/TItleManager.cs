using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TItleManager : MonoBehaviour
{
    public string targetSceneName; // �J�ڐ�̃V�[����

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��V�[����J�ڂ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �V�[����J�ڂ���
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
