using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanRespawn : MonoBehaviour
{
    private Vector3 defPos;
    private Quaternion defRota;
    private Rigidbody rb;
    [SerializeField] private float respawnPos = 30;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Ž©•ª‚Ì‚‚³‚ð•ÛŽ
        defPos = transform.position;
        defRota = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.y - defPos.y) > respawnPos)
        {
            rb.velocity = Vector3.zero;
            transform.localRotation = defRota;
            transform.position = defPos;
        }
    }
}
