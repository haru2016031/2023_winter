using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ParticleSystem effectPrefab; // �G�t�F�N�g�̃v���n�u
    private ParticleSystem effectInstance; // �G�t�F�N�g�̃C���X�^���X
    public GameObject shootObj; // ���˂���I�u�W�F�N�g�̃v���n�u
    public Transform spawnPoint; // ���ˈʒu
    public float shootingForce = 10f; // ���˂����
    public AudioClip audioClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    void PlayEffect()
    {
        // �G�t�F�N�g�̃v���n�u���ݒ肳��Ă���ꍇ
        if (effectPrefab != null)
        {
            // �G�t�F�N�g�̃v���n�u����V�����C���X�^���X�𐶐�
            effectInstance = Instantiate(effectPrefab, spawnPoint.position, Quaternion.identity);

            // �G�t�F�N�g���Đ����I�������C���X�^���X��j��
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }

    }

    void Shoot()
    {
        // �V���[�^�[�̑O�����ɗ͂������ăI�u�W�F�N�g�𔭎˂���
        GameObject projectile = Instantiate(shootObj, spawnPoint.position, spawnPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectile.GetComponent<UltObj>().SetRespawnFlag(false);
            var dir = transform.forward + new Vector3(0, 0.3f, 0);
            projectileRb.AddForce(dir * shootingForce, ForceMode.Impulse);
            audioSource.PlayOneShot(audioClip);
            PlayEffect();
        }
    }
    private void OnEnable()
    {
        GetComponentInParent<SwitchManager>().OnSwitchFunc += Shoot;
    }
}
