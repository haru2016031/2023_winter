using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossCtl : MonoBehaviour
{
    public Image image;
    void Start()
    {

    }

    void Update()
    {
        if (FindObjectOfType<MoveObject>() != null)
        {
            bool isUltraHundActive = FindObjectOfType<MoveObject>().useUltraHundFlag;
            image.enabled = isUltraHundActive;
        }
    }
}

