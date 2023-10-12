using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // �ڒn��̗����܂ł̒x������
    public float shakeDuration = 1.0f; // �h�炵�����鎞��
    public float shakeInterval = 0.1f; // ����h�炷�Ԋu
    public float shakeMagnitude = 0.1f; // �h��̋���
    private Rigidbody rb;
    private Vector3 pos;

    private Coroutine _coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // �ŏ��͕����I�ȉe�����󂯂Ȃ�
        pos = GetComponent<Transform>().position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �v���C���[�ƐڐG�����ꍇ
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(FallWithShake());
            }
        }
    }

    IEnumerator FallWithShake()
    {
        // �h���
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomShake = new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * shakeMagnitude;
            transform.position += randomShake;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �����I�ȉe�����󂯂�
        rb.isKinematic = false;

        // 1�b��ɏ��I�u�W�F�N�g�����̏ꏊ�ɖ߂�
        yield return new WaitForSeconds(fallDelay);
        DeactivatePlatform();
    }

    void DeactivatePlatform()
    {
        transform.position = pos;
        rb.isKinematic = true;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
