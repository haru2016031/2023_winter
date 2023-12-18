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
					//ゲームシーン→ポーズシーン
					Cursor.visible = true;
					pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
					Time.timeScale = 0f;
					Cursor.lockState = CursorLockMode.None;
					eventSystem.SetActive(false);
					addES = this.transform.Find("EventSystem").gameObject;
				}
				else
				{
					//ポーズシーン→ゲームシーン
					Cursor.visible = false;
					Destroy(pauseUIInstance);
					Time.timeScale = 1f;
					Cursor.lockState = CursorLockMode.Locked;
					eventSystem.SetActive(true);
				}
			}
            else
            {
				//設定シーンor出口シーン→ポーズシーン
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
