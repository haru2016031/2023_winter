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

	private GameObject exitUI;
	private GameObject settingUI;

	[SerializeField]
	private GameObject eventSystem;
	private GameObject addES;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Option"))
		{
            if (!exitUI && !settingUI)
            {
				if (pauseUIInstance == null)
				{
					//�Q�[���V�[�����|�[�Y�V�[��
					Cursor.visible = true;
					pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
					Time.timeScale = 0f;
					Cursor.lockState = CursorLockMode.None;
					eventSystem.SetActive(false);
					addES = this.transform.Find("EventSystem").gameObject;
				}
				else
				{
					//�|�[�Y�V�[�����Q�[���V�[��
					Cursor.visible = false;
					Destroy(pauseUIInstance);
					Time.timeScale = 1f;
					Cursor.lockState = CursorLockMode.Locked;
					eventSystem.SetActive(true);
				}
			}
            else
            {
				//�ݒ�V�[��or�o���V�[�����|�[�Y�V�[��
				Destroy(exitUI);
				Destroy(settingUI);
				addES.SetActive(true);
            }
		}
	}

	public void SetExitUI(GameObject obj)
    {
		exitUI = obj;
		addES.SetActive(false);

	}

	public void SetSettingUI(GameObject obj)
    {
		settingUI = obj;
		addES.SetActive(false);
	}
}
