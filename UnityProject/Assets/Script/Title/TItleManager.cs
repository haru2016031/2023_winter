using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string targetSceneName; // �J�ڐ�̃V�[����

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��V�[����J�ڂ���
        if (Input.anyKeyDown)
        {
            // �V�[����J�ڂ���
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
