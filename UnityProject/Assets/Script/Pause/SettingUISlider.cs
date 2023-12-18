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
    Text cameraSpeedText;     //���x�����e�L�X�g
    [SerializeField] 
    Text soundVolumeText;     //���ʒ����e�L�X�g
    private Slider cameraSpeedSlider;          //���x�����X���C�_�[
    private Slider soundVolumeSlider;          //���ʒ����X���C�_�[

    // Start is called before the first frame update
    void Start()
    {
        CameraSpeedInit();
        SoundVolumeInit();
    }

    //���x�����X���C�_�[������
    void CameraSpeedInit()
    {
        //���C���J�����̎擾
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

    //�X���C�_�[�̕ω��ŏ�������֐�
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

        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("Master", volume);
        UpdateSliderValueText(soundVolumeSlider, soundVolumeText);

    }

    void UpdateSliderValueText(Slider slider,Text text)
    {
        // Slider�̒l�̊������v�Z���AText�ɕ\��
        float percentage = slider.value / slider.maxValue * 100f;
        text.text = percentage.ToString("F0") + "%";
    }
}
