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
        //���C���J�����̎擾
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Transform cameraCtl = camera.transform.Find("CameraController");
        controller = cameraCtl.GetComponent<CameraController>();
        Transform sliderObj = this.transform.Find("CameraSpeedSlider");
        Slider slider = sliderObj.GetComponent<Slider>();
        slider.value = controller.rotationSpeed;
    }

    //�X���C�_�[�̕ω��ŏ�������֐�
    public void OnCameraSliderValueChanged(float value)
    {
        controller.SetCameraSpeed(value);
    }

    //BGM
    public void SetAudioMixerMaster(float value)
    {
        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("Master", volume);
    }
}
