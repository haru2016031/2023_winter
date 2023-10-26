using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    public float windStrength = 10f; // ���̋���

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // ���̕����ɗ͂�������
            Vector3 windDirection = transform.forward;
            rb.AddForce(windDirection * windStrength);
        }
    }
}
