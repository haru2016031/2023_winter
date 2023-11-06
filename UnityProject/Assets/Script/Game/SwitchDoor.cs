using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor: MonoBehaviour
{
    private Vector3 m_targetPosition;
    private bool m_bOpen = false;

    private void DoorOpen()
    {
        m_bOpen = true;
    }
    private void Start()
    {
        m_targetPosition = transform.position + Vector3.up * 5f;
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
