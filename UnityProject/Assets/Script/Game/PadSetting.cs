using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PadSetting : MonoBehaviour
{
    void Start()
    {
        string[] joystickName = Input.GetJoystickNames();

        if(joystickName.Length > 0 && !string.IsNullOrEmpty(joystickName[0]))
        {
            for(int i = 0; i < joystickName.Length; i++)
            {
                if(joystickName[i].ToLower().Contains("xbox"))
                {
                    SetXInputSetting();
                }
                else if(joystickName[i].ToLower().Contains("dualshock"))
                {
                    SetDualShcokSetting();
                }
            }
        }
    }

    void SetXInputSetting()
    {
        Debug.Log("Xboxコントローラーの設定を適用");
    }

    void SetDualShcokSetting()
    {
        Debug.Log("PSコントローラーの設定を適用");
    }
}
