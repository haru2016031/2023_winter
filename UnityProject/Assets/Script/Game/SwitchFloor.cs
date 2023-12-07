using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFloor : MonoBehaviour
{
    private Vector3 m_targetPosition;
    private bool m_bMove = false;
    [SerializeField] private GameObject endObj;


    public AudioClip audioClip;
    AudioSource audioSource;

    private void FloorMove()
    {
        m_bMove = true;
        audioSource.PlayOneShot(audioClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_targetPosition = endObj.transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_targetPosition,
                10f * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        GetComponentInParent<SwitchManager>().OnSwitchFunc += FloorMove;
    }
}
