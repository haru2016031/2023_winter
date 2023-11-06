using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Color newColor = Color.red; // �ύX��̐F
    private Color originalColor; // �����̐F
    private Renderer objectRenderer;
    public float lowerAmount = 0.5f; // ���Ɉړ������
    private bool isLowered = false; // ���Ɉړ��������ǂ����̃t���O

    private void Start()
    {
        objectRenderer = GetComponent < Renderer>();
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hold") && !isLowered) // �v���C���[�I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        {
            // �V�����F�ɕύX
            objectRenderer.material.color = newColor;

            // ���Ɉړ�
            Vector3 newPosition = transform.position;
            newPosition.y -= lowerAmount;
            transform.position = newPosition;

            isLowered = true;
            SwitchManager.DoorOpen();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hold") && isLowered) // �v���C���[�I�u�W�F�N�g���痣�ꂽ�ꍇ
        {
            // �����̐F�ɖ߂�
            objectRenderer.material.color = originalColor;


        }
    }
}
