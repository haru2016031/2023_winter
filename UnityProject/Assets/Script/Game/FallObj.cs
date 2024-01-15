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
    private float delayTime = 2.0f;
    public float moveTime = 2.0f;    // �ړ��ɂ����鎞��

    public AudioClip respowanSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
  
        //�������Z����
        Invoke("ReturnToInitialPosition", delayTime);

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



    void ReturnToInitialPosition()
    {
        EffectInit();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(respowanSE);
        }

        // ���̈ʒu�ɖ߂�
        Destroy(gameObject);
    }
}
