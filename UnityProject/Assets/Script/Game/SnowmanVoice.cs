using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanVoice : MonoBehaviour
{
    private bool isCollied = false;
    public AudioClip snowManVoice;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isCollied)
            {
                audioSource.PlayOneShot(snowManVoice);
                isCollied = true;
            }
        }
    }
}
