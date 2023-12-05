using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor: MonoBehaviour
{
    private Vector3 m_targetPosition;
    private bool m_bOpen = false;
    [SerializeField]
    private Vector3 m_moveDir = Vector3.up; //ˆÚ“®•ûŒü
    [SerializeField]
    private float m_speed = 5f; //ˆÚ“®‘¬“x

    public AudioClip audioClip;
    AudioSource audioSource;

    private void DoorOpen()
    {
        m_bOpen = true;
        audioSource.PlayOneShot(audioClip);
    }
    private void Start()
    {
        m_targetPosition = transform.position + m_moveDir * m_speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
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
        SwitchManager.OnSwitchFunc += DoorOpen;
    }
    private void OnDisable()
    {
        SwitchManager.OnSwitchFunc -= DoorOpen;
    }
}
