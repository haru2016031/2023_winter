using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffect : MonoBehaviour
{
    public GameObject prefab;

    private float offsetY = 2.0f;

    private bool hasPlayed = false; // 再生フラグ

    void Start()
    {
    }

    void OnTriggerEnter(Collider  collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 spawnPos = transform.position + new Vector3(0,offsetY,0);
            Instantiate(prefab,spawnPos,transform.rotation);
        }
    }
}
