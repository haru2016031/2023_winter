using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraCtl : MonoBehaviour
{
    private GameObject player;
    private GameObject hold;
    private Renderer holdRenderer;
    private Color initHoldColor;
    private bool active = false;

    void Start()
    {
        player = GameObject.Find("aura04");
        player.SetActive(active);

        GameObject[] holdObjets = GameObject.FindGameObjectsWithTag("Hold");
        if(holdObjets.Length > 0)
        {
            hold = holdObjets[0];
            holdRenderer = hold.GetComponent<Renderer>();
            initHoldColor = holdRenderer.material.color;
        }

        // ‰Šú‚ÌƒJƒ‰[‚ğ•Û‘¶
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
                holdRenderer.material.color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.SetActive(active = false);
            holdRenderer.material.color = initHoldColor;
        }
    }
}
