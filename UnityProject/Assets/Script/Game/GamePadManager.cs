using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GamePadManager : MonoBehaviour
{ 
    void Start()
    {
        string[] joystickNames = Input.GetJoystickNames();
        foreach(string joystickName in joystickNames)
        {
            if(joystickName.Contains("Xbox"))
            {

            }
            else if(joystickName.Contains("Wireless Controller"))
            {

            }
        }
    }

    void SetXboxControllerInput()
    {
        //InputManagerEntry.Set
    }
}
