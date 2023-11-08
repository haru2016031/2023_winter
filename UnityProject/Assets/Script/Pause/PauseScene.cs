using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScene : MonoBehaviour
{
	[SerializeField]
	//�@�|�[�Y�������ɕ\������UI�̃v���n�u
	private GameObject pauseUIPrefab;
	//�@�|�[�YUI�̃C���X�^���X
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
