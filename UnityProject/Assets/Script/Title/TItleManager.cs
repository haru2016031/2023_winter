using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TItleManager : MonoBehaviour
{
    public string targetSceneName; // 遷移先のシーン名

    void Update()
    {
        // スペースキーが押されたらシーンを遷移する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // シーンを遷移する
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
