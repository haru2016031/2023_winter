using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchManager : MonoBehaviour
{
    public static event Action OnSwitchFunc;
    public static void SwitchFunc()
    {
        OnSwitchFunc?.Invoke();
    }
}
