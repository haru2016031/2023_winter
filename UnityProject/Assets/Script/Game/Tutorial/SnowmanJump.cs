using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanJump : MonoBehaviour
{
    public float jumpForce = 10f; // �W�����v��
    public float jumpInterval = 2f; // �W�����v�Ԋu
    public int jumpCnt = 1;     //�W�����v�\��

    private Rigidbody rb;
    private Coroutine _coroutine;
    private int currentJumpCnt; //���݂̃W�����v��

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
