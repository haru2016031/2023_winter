using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanJump : MonoBehaviour
{
    public float jumpForce = 10f; // ジャンプ力
    public float jumpInterval = 2f; // ジャンプ間隔

    private Rigidbody rb;
    private Coroutine _coroutine;

    private float nextJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextJumpTime = Time.time + jumpInterval;
    }

    IEnumerator Jump()
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(jumpInterval);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _coroutine = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Jump());
            }
        }
    }
}
