using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossCtl : MonoBehaviour
{
    public Image image;
    public MoveObject moveObject;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (moveObject.useUltraHundFlag == true)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
