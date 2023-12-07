using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TagList
{
    Hold,
    Player

}
public class SwitchController : MonoBehaviour
{
    public Color newColor = Color.red;  // �ύX��̐F
    private Color originalColor;        // �����̐F
    private Renderer objectRenderer;    // �X�C�b�`�̃����_���[
    public float lowerAmount = 0.5f;    // ���Ɉړ������
    [SerializeField]
    private float pushPower = 1.0f;     // ������
    [SerializeField]
    private TagList tag;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private Vector3 defPos;             // �������W
    private bool isPressed = false;     // ������Ă��邩
    private bool isStarted = false;  // �M�~�b�N���N��������
    private Vector3 m_targetPosition;   // ���������
    private float currentTime;          //�v������
    private float limitTime = 0.2f;     //���ꂽ����̗P�\����

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        originalColor = objectRenderer.material.color;
        defPos = transform.position;
    }

    private void Update()
    {

        //�{�^���̏オ�艺����
        if (isPressed)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_targetPosition,
                pushPower * Time.deltaTime);

        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                defPos,
                (pushPower/2) * Time.deltaTime);

        }

        if (!isPressed && isStarted)
        {
            //�{�^�����痣�ꂽ����ɗP�\����������
            if(currentTime > limitTime)
            {
                isStarted = false;
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }

        }

        //�{�^����������違�܂��e��ł��ĂȂ��ꍇ
        if (transform.position == m_targetPosition && !isStarted)
        {
            audioSource.PlayOneShot(audioClip);
            GetComponentInParent<SwitchManager>().SwitchFunc();
            isStarted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnterFunc(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitFunc(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnEnterFunc(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        OnExitFunc(other.gameObject);
    }


    private void OnEnterFunc(GameObject obj)
    {
        if (obj.CompareTag(tag.ToString()) && !isPressed) // �v���C���[�I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        {
            // �V�����F�ɕύX
            objectRenderer.material.color = newColor;

            // ���Ɉړ�
            Vector3 newPosition = defPos;
            newPosition.y -= lowerAmount;
            m_targetPosition = newPosition;


            isPressed = true;
        }
    }

    private void OnExitFunc(GameObject obj)
    {
        if (obj.CompareTag(tag.ToString()) && isPressed) // �v���C���[�I�u�W�F�N�g���痣�ꂽ�ꍇ
        {
            // �����̐F�ɖ߂�
            objectRenderer.material.color = originalColor;
            isPressed = false;
        }

    }
}
