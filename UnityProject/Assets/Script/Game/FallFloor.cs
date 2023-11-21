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

    public AudioClip fallFloorSE;
    public AudioClip fallSE;
    public AudioClip respawnSE;
    AudioSource fallFloorSource;
    AudioSource fallSouce;
    AudioSource respowanSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // �ŏ��͕����I�ȉe�����󂯂Ȃ�
        pos = GetComponent<Transform>().position;
        fallFloorSource = GetComponent<AudioSource>();
        fallSouce = GetComponent<AudioSource>();
        respowanSource = GetComponent<AudioSource>();
        fallFloorSource.volume = 0.3f;
        fallSouce.volume = 0.3f;
        respowanSource.volume = 0.3f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �v���C���[�ƐڐG�����ꍇ
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(FallWithShake());
                fallFloorSource.PlayOneShot(fallFloorSE);
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

        if(!rb.isKinematic)
        {
            fallFloorSource.Stop();
            fallSouce.PlayOneShot(fallSE);
        }

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
        respowanSource.PlayOneShot(respawnSE);
    }
}
