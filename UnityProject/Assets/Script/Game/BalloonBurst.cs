using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBurst : MonoBehaviour
{
    public ParticleSystem popEffectPrefab; // �G�t�F�N�g��Prefab��Inspector����A�^�b�`����
    public float effectDuration = 2.0f; // �G�t�F�N�g�̍Đ�����

    private bool popped = false; // ���D�����łɊ��ꂽ���ǂ���

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
                Destroy(popEffect.gameObject, popEffect.main.duration);
            }

            // ���D���폜
            Destroy(gameObject);
        }
    }
}
