using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingUISlider : MonoBehaviour
{
    private static float cameraSpeed = 2.0f;
    private static float soundValue = 5.0f;
    CameraController controller;
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        CameraSpeedInit();
        SoundVolumeInit();
    }

    //感度調整スライダー初期化
    void CameraSpeedInit()
    {
        //メインカメラの取得
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Transform cameraCtl = camera.transform.Find("CameraController");
        controller = cameraCtl.GetComponent<CameraController>();
        Transform cameraSliderObj = this.transform.Find("CameraSpeedSlider");
        Slider slider = cameraSliderObj.GetComponent<Slider>();
        slider.value = cameraSpeed;
    }

    void SoundVolumeInit()
    {
        Transform sliderTrans = this.transform.Find("SoundVolumeSlider");
        Slider slider = sliderTrans.GetComponent<Slider>();
        slider.value = soundValue;
    }

    //スライダーの変化で処理する関数
    public void OnCameraSliderValueChanged(float value)
    {
        cameraSpeed = value;
        controller.SetCameraSpeed(value);
    }

    //BGM
    public void SetAudioMixerMaster(float value)
    {
        soundValue = value;

        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("Master", volume);
    }
}
