using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    public float moveTime = 2.0f;    // �ړ��ɂ����鎞��


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>().position;
        rota = GetComponent<Transform>().rotation;
        MoveObjectToTarget();

    }

    void MoveObjectToTarget()
    {

        // ��莞�Ԍ�Ɍ��̈ʒu�ɖ߂����\�b�h���Ăяo��
        Invoke("ReturnToInitialPosition", moveTime);
    }

    void ReturnToInitialPosition()
    {
        // ���̈ʒu�ɖ߂�
        transform.position = pos;
        transform.rotation = rota; 
        rb.velocity = Vector3.zero;
        rb.Sleep();
        MoveObjectToTarget();
    }
}
