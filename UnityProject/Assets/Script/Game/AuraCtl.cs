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
    [SerializeField]
    private float ultDistance = 10.0f;

    private void Awake()
    {
        player = GameObject.Find("aura04");
    }
    void Start()
    {
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
        }

        // �����̃J���[��ۑ�
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
                    // this�i�������g��Transform�j��obj�̋������v�Z
                    float distance = Vector3.Distance(other.transform.position, obj.transform.position);
                    Debug.Log(distance);
                    // ���̋����ȓ��ɂ���ꍇ
                    if (distance < ultDistance)
                    {
                        if (obj.GetComponent<OutlineBehaviour>())
                        {
                            obj.GetComponent<OutlineBehaviour>().enabled = true;
                        }
                    }
                }
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


            }
        }
    }
}
