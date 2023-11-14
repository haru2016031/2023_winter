using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor: MonoBehaviour
{
    private Vector3 m_targetPosition;
    private bool m_bOpen = false;
    [SerializeField]
    private Vector3 m_moveDir = Vector3.up; //�ړ�����
    [SerializeField]
    private float m_speed = 5f; //�ړ����x


    private void DoorOpen()
    {
        m_bOpen = true;
    }
    private void Start()
    {
        m_targetPosition = transform.position + m_moveDir * m_speed;
    }

    private void Update()
    {
        if (m_bOpen)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_targetPosition,
                10f * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        SwitchManager.OnDoorOpen += DoorOpen;
    }
    private void OnDisable()
    {
        SwitchManager.OnDoorOpen -= DoorOpen;
    }
}
