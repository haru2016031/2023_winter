using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScene : MonoBehaviour
{
	[SerializeField]
	//　ポーズした時に表示するUIのプレハブ
	private GameObject pauseUIPrefab;
	//　ポーズUIのインスタンス
	private GameObject pauseUIInstance;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (pauseUIInstance == null)
			{
				Cursor.visible = true;
				pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
				Cursor.lockState = CursorLockMode.None;

			}
			else
			{
				Cursor.visible = false;
				Destroy(pauseUIInstance);
				Time.timeScale = 1f;
				Cursor.lockState = CursorLockMode.Locked;

			}
		}
	}
}
