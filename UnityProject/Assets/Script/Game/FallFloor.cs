using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // 接地後の落下までの遅延時間
    public float shakeDuration = 1.0f; // 揺らし続ける時間
    public float shakeInterval = 0.1f; // 床を揺らす間隔
    public float shakeMagnitude = 0.1f; // 揺れの強さ
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
        rb.isKinematic = true; // 最初は物理的な影響を受けない
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
        if (collision.gameObject.CompareTag("Player")) // プレイヤーと接触した場合
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
        // 揺れる
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomShake = new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * shakeMagnitude;
            transform.position += randomShake;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 物理的な影響を受ける
        rb.isKinematic = false;

        if(!rb.isKinematic)
        {
            fallFloorSource.Stop();
            fallSouce.PlayOneShot(fallSE);
        }

        // 1秒後に床オブジェクトを元の場所に戻す
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
