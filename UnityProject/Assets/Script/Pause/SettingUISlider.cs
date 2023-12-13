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
}
