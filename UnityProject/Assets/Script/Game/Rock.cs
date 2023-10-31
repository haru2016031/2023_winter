using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Vector3 inistPos;
    private Rigidbody rb;
    private float fallHeight = -10.0f;
    void Start()
    {
        inistPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.position.y < fallHeight)
        {
            transform.position = inistPos;

            if(rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
