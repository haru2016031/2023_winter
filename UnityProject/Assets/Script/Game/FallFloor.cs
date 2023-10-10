using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // �ڒn��̗����܂ł̒x������
    [SerializeField] private float backTime = 1.5f;
    private Rigidbody rb;
    private Vector3 pos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // �ŏ��͕����I�ȉe�����󂯂Ȃ�
        pos = GetComponent<Transform>().position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �v���C���[�ƐڐG�����ꍇ
        {
            Invoke("Fall", fallDelay); // �x����ɗ���������
        }
    }
    void Fall()
    {
        rb.isKinematic = false; // �����I�ȉe�����󂯂�
                                // 1�b��ɏ��I�u�W�F�N�g���A�N�e�B�u�ɂ���
        Invoke("DeactivatePlatform", backTime);
    }

    void DeactivatePlatform()
    {
        transform.position = pos;
        rb.isKinematic = true; 

    }
}
