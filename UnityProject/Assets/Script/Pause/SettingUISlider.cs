using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUISlider : MonoBehaviour
{
    CameraController controller;
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
}
