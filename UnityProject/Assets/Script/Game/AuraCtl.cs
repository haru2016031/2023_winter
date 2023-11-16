using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraCtl : MonoBehaviour
{
    public GameObject auraObject;
    void Start()
    {
        auraObject = GetComponent<GameObject>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("UltraHundAria"))
        {
            auraObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UltraHundAria"))
        {
            auraObject.SetActive(false);
        }
    }
}
