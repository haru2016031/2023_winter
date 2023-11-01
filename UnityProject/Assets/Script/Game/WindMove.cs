using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    public float windStrength = 10f; // •—‚Ì‹­‚³
    public AudioClip windSE;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // •—‚Ì•ûŒü‚É—Í‚ð‰Á‚¦‚é
            Vector3 windDirection = transform.forward;
            rb.AddForce(windDirection * windStrength);
            audioSource.PlayOneShot(windSE);
        }
    }
}
