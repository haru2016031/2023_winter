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
    [SerializeField] 
    AudioMixer audioMixer;
    [SerializeField] 
    Text cameraSpeedText;     //感度調整テキスト
    [SerializeField] 
    Text soundVolumeText;     //音量調整テキスト
    private Slider cameraSpeedSlider;          //感度調整スライダー
    private Slider soundVolumeSlider;          //音量調整スライダー

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
        cameraSpeedSlider = cameraSliderObj.GetComponent<Slider>();
        cameraSpeedSlider.value = cameraSpeed;
        UpdateSliderValueText(cameraSpeedSlider, cameraSpeedText);
    }

    void SoundVolumeInit()
    {
        Transform sliderTrans = this.transform.Find("SoundVolumeSlider");
        soundVolumeSlider = sliderTrans.GetComponent<Slider>();
        soundVolumeSlider.value = soundValue;
        UpdateSliderValueText(soundVolumeSlider, soundVolumeText);
    }

    //スライダーの変化で処理する関数
    public void OnCameraSliderValueChanged(float value)
    {
        cameraSpeed = value;
        controller.SetCameraSpeed(value);
        UpdateSliderValueText(cameraSpeedSlider,cameraSpeedText);
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
        UpdateSliderValueText(soundVolumeSlider, soundVolumeText);

    }

    void UpdateSliderValueText(Slider slider,Text text)
    {
        // Sliderの値の割合を計算し、Textに表示
        float percentage = slider.value / slider.maxValue * 100f;
        text.text = percentage.ToString("F0") + "%";
    }
}
