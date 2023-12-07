using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchManager : MonoBehaviour
{
    public  event Action OnSwitchFunc;
    public  void SwitchFunc()
    {
        OnSwitchFunc?.Invoke();
    }
}
