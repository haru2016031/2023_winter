using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtl : MonoBehaviour
{
    public Text clertText;
    public Text clerTimeText;
    public GameObject player;
    void Start()
    {
        clertText.enabled = false;
        clerTimeText.enabled = false;
    }

    void Update()
    {

    }
}
