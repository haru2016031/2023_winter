using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBurst : MonoBehaviour
{
    public ParticleSystem popEffectPrefab; // �G�t�F�N�g��Prefab��Inspector����A�^�b�`����
    public float effectDuration = 2.0f; // �G�t�F�N�g�̍Đ�����
    public float respawnDelay = 2.0f; // ���X�|�[���܂ł̒x������
    public float growthDuration = 2.0f; // �����ɂ����鎞��

    // SE
    public AudioClip balloonSE;
    public AudioClip expandSE;
    AudioSource audioSource;

    private bool popped = false; // ���D�����łɊ��ꂽ���ǂ���
    [SerializeField] private Renderer[] balloonRenderer;
    private Collider balloonCollider;
    private float scale;             //�f�t�H���g�̃X�P�[��
    private float currentScale = 1.0f; // ���D�̌��݂̃X�P�[��
    private float growthStartTime; // �������n�܂�������

    private void Start()
    {
        // ���D��Renderer��Collider�R���|�[�l���g���擾
        balloonCollider = GetComponent<Collider>();

        // ������Ԃł͕`��Ɠ����蔻���L���ɂ���
        foreach (var renderer in balloonRenderer)
        {
            renderer.enabled = true;
        }
        balloonCollider.enabled = true;
        scale = transform.localScale.x;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    private void Update()
    {
        if (!popped)
        {
            // ���D������Ă���ꍇ�A�����A�j���[�V�����𐧌�
            float elapsedTime = Time.time - growthStartTime;
            if (elapsedTime < growthDuration)
            {
                // �w��̎��ԓ��ɏ��X�ɃX�P�[���𑝉�
                float growthProgress = elapsedTime / growthDuration;
                currentScale = Mathf.Lerp(currentScale, scale, growthProgress);
                transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !popped)
        {
            // �v���C���[�����D�ɐG�ꂽ��
            popped = true; // ���D���������ƃ}�[�N
            
            // �G�t�F�N�g���Đ�
            if (popEffectPrefab != null)
            {
                Vector3 pos = transform.position + new Vector3(0, 2, 0);
                ParticleSystem popEffect = Instantiate(popEffectPrefab, pos, Quaternion.identity);
                audioSource.PlayOneShot(balloonSE);
                Destroy(popEffect.gameObject, popEffect.main.duration);
            }
            // ���̒x�����Ԍ�Ƀ��X�|�[��
            StartCoroutine(RespawnBalloon());

            // �`��Ɠ����蔻��𖳌��ɂ���
            foreach (var renderer in balloonRenderer)
            {
                renderer.enabled = false;
            }
            balloonCollider.enabled = false;

            transform.localScale = Vector3.zero;

        }

        IEnumerator RespawnBalloon()
        {
            yield return new WaitForSeconds(respawnDelay);

            // ���X�|�[������
            foreach (var renderer in balloonRenderer)
            {
                renderer.enabled = true;
            }
            balloonCollider.enabled = true;

            popped = false; // �V�������D��������Ԃɂ���
            // �����A�j���[�V�������J�n
            growthStartTime = Time.time;
            currentScale = 1.0f;
            audioSource.PlayOneShot(expandSE);

        }


    }
}
