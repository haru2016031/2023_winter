using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    public float windStrength = 10f; // 風の強さ
    public Transform player;
    public Transform soundPos;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    void Update()
    {
        audioSource.Play();


        float audiodistance = audioSource.maxDistance;
        float playerdistance = Vector3.Distance(player.position, soundPos.position);

        if (playerdistance <= audiodistance)
        {
            //Debug.Log("Maxdistance内");
        }
        else
        {
            //Debug.Log("Maxdistance外");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 風の方向に力を加える
            Vector3 windDirection = transform.forward;
            rb.AddForce(windDirection * windStrength);
        }
    }
}
