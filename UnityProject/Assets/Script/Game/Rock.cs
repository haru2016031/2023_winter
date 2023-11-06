using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Vector3 inistPos;
    private Rigidbody rb;
    private float fallHeight;
    void Start()
    {
        inistPos = transform.position;
        rb = GetComponent<Rigidbody>();
        fallHeight = inistPos.y - 20.0f;
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
