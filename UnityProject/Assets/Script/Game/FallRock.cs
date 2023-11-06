using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    public ParticleSystem effectPrefab; // エフェクトのプレハブ
    private ParticleSystem effectInstance; // エフェクトのインスタンス
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    public float moveTime = 2.0f;    // 移動にかかる時間

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
        EffectInit();

        MoveObjectToTarget();

    }

    void EffectInit()
    {
        // エフェクトのプレハブが設定されている場合
        if (effectPrefab != null)
        {
            // エフェクトのプレハブから新しいインスタンスを生成
            effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // エフェクトが再生し終わったらインスタンスを破棄
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }

    }

    void MoveObjectToTarget()
    {

        // 一定時間後に元の位置に戻すメソッドを呼び出す
        Invoke("ReturnToInitialPosition", moveTime);
    }

    void ReturnToInitialPosition()
    {
        EffectInit();

        // 元の位置に戻る
        transform.position = pos;
        transform.rotation = rota; 
        rb.velocity = Vector3.zero;
        audioSource.PlayOneShot(respowanSE);
        rb.Sleep();
        EffectInit();

        MoveObjectToTarget();
    }
}
