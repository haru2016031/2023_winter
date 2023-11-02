using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchManager : MonoBehaviour
{
    public static event Action OnDoorOpen;
    public static void DoorOpen()
    {
        OnDoorOpen?.Invoke();
    }
}
