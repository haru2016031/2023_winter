using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea : MonoBehaviour
{
    public float activationRange = 10.0f;  // Rigidbody��L���ɂ���͈͂̔��a
    private Rigidbody rb;
    private Vector3 initialPos = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        // Rigidbody���A�^�b�`����Ă��Ȃ��ꍇ�̓G���[���b�Z�[�W��\��
        if (rb == null)
        {
            Debug.LogError("Rigidbody not attached to the GameObject!");
        }
    }

    void Update()
    {
        // �I�u�W�F�N�g�̌��݂̈ʒu���珉���ʒu�܂ł̋������v�Z
        float distanceToInitialPosition = Vector3.Distance(transform.position, initialPos);
        // ������activationRange��菬�����ꍇ��Rigidbody��L���ɂ��A�傫���ꍇ�͖����ɂ���
        if (distanceToInitialPosition <= activationRange)
        {
            EnableRigidbody();
        }
        else
        {
            DisableRigidbody();
        }
    }

    void EnableRigidbody()
    {
        // Rigidbody��L���ɂ���
        if (rb != null && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }

    void DisableRigidbody()
    {
        // Rigidbody�𖳌��ɂ���
        if (rb != null && !rb.isKinematic)
        {
            rb.isKinematic = true;
        }
    }
}
