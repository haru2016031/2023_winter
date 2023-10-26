using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanJump : MonoBehaviour
{
    public float jumpForce = 10f; // ジャンプ力
    public float jumpInterval = 2f; // ジャンプ間隔
    public int jumpCnt = 1;     //ジャンプ可能回数

    private Rigidbody rb;
    private Coroutine _coroutine;
    private int currentJumpCnt; //現在のジャンプ回数

    private float nextJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextJumpTime = Time.time + jumpInterval;
        currentJumpCnt = jumpCnt;
    }

    IEnumerator Jump()
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(jumpInterval);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _coroutine = null;
            currentJumpCnt--;
            if(currentJumpCnt > 0)
            {
                _coroutine = StartCoroutine(Jump());
            }
            else
            {
                currentJumpCnt = jumpCnt;
            }
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
