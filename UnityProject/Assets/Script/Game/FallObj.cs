using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObj : MonoBehaviour
{
    public ParticleSystem effectPrefab; // �G�t�F�N�g�̃v���n�u
    private ParticleSystem effectInstance; // �G�t�F�N�g�̃C���X�^���X
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    [SerializeField]
    private float delayTime = 0f;
    [SerializeField]
    private bool respawnFlag = false;
    public float moveTime = 7.0f;    // �ړ��ɂ����鎞��

    public AudioClip respowanSE;
    AudioSource audioSource;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        initPos = transform.position;
        Init();
    }

    private void Init()
    {
        //�������Z����
        gameObject.SetActive(false);
        Invoke("MoveObject", delayTime);

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

    void MoveObject()
    {
        gameObject.SetActive(true);
        EffectInit();
        Invoke("ReturnToInitialPosition", moveTime);
    }



    void ReturnToInitialPosition()
    {
        EffectInit();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(respowanSE);
        }

        // ���̈ʒu�ɖ߂�
        if (respawnFlag)
        {
            Init();
            transform.position = initPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
