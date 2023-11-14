using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    public ParticleSystem effectPrefab; // �G�t�F�N�g�̃v���n�u
    private ParticleSystem effectInstance; // �G�t�F�N�g�̃C���X�^���X
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    [SerializeField]
    private float delayTime = 2.0f;
    public float moveTime = 2.0f;    // �ړ��ɂ����鎞��

    public AudioClip respowanSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>().position;
        rota = GetComponent<Transform>().rotation;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        //��\��
        //�������Z����
        Invoke("MoveObjectToTarget", delayTime);
        this.gameObject.SetActive(false);

    }

    void EffectInit()
    {
        // �G�t�F�N�g�̃v���n�u���ݒ肳��Ă���ꍇ
        if (effectPrefab != null)
        {
            // �G�t�F�N�g�̃v���n�u����V�����C���X�^���X�𐶐�
            effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // �G�t�F�N�g���Đ����I�������C���X�^���X��j��
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }

    }

    void MoveObjectToTarget()
    {
        this.gameObject.SetActive(true);
        EffectInit();
        // ��莞�Ԍ�Ɍ��̈ʒu�ɖ߂����\�b�h���Ăяo��
        Invoke("ReturnToInitialPosition", moveTime);
    }

    void ReturnToInitialPosition()
    {
        EffectInit();

        // ���̈ʒu�ɖ߂�
        transform.position = pos;
        transform.rotation = rota; 
        rb.velocity = Vector3.zero;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(respowanSE);
        }
        rb.Sleep();
        EffectInit();

        MoveObjectToTarget();
    }
}
