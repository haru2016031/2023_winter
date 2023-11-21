using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class AuraCtl : MonoBehaviour
{
    private GameObject player;
    private GameObject hold;
    private GameObject[] holdObjects;
    private Renderer holdRenderer;
    private Color initHoldColor;
    private bool active = false;

    void Start()
    {
        player = GameObject.Find("aura04");
        player.SetActive(active);

        holdObjects = GameObject.FindGameObjectsWithTag("Hold");
        if(holdObjects.Length > 0)
        {
            foreach (var obj in holdObjects)
            {
                holdRenderer = obj.GetComponent<Renderer>();
                if(obj.GetComponent<OutlineBehaviour>())
                {
                  obj.GetComponent<OutlineBehaviour>().enabled = false;
                }

                initHoldColor = holdRenderer.material.color;
            }
            //hold = holdObjects[0];
        }

        // èâä˙ÇÃÉJÉâÅ[Çï€ë∂
        if(holdRenderer != null)
        {
            initHoldColor = holdRenderer.material.color;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.SetActive(active = true);

            if (active)
            {
                foreach (var obj in holdObjects)
                {
                    if (obj.GetComponent<OutlineBehaviour>())
                    {
                        obj.GetComponent<OutlineBehaviour>().enabled = true;
                    }
                    //obj.GetComponent<Renderer>().material.color = Color.blue;

                }
                //holdRenderer.material.color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.SetActive(active = false);
            foreach (var obj in holdObjects)
            {
                if (obj.GetComponent<OutlineBehaviour>())
                {
                    obj.GetComponent<OutlineBehaviour>().enabled = false;
                }

                //obj.GetComponent<Renderer>().material.color = initHoldColor;

            }
            //holdRenderer.material.color = initHoldColor;
        }
    }
}
