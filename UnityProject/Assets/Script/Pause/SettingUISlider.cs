using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingUISlider : MonoBehaviour
{
    CameraController controller;
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        //メインカメラの取得
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Transform cameraCtl = camera.transform.Find("CameraController");
        controller = cameraCtl.GetComponent<CameraController>();
        Transform sliderObj = this.transform.Find("CameraSpeedSlider");
        Slider slider = sliderObj.GetComponent<Slider>();
        slider.value = controller.rotationSpeed;
    }

    //スライダーの変化で処理する関数
    public void OnCameraSliderValueChanged(float value)
    {
        controller.SetCameraSpeed(value);
    }

    //BGM
    public void SetAudioMixerMaster(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("Master", volume);
    }
}
