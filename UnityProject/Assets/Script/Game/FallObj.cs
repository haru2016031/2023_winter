using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObj : MonoBehaviour
{
    public ParticleSystem effectPrefab; // エフェクトのプレハブ
    private ParticleSystem effectInstance; // エフェクトのインスタンス
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    [SerializeField]
    private float delayTime = 2.0f;
    public float moveTime = 2.0f;    // 移動にかかる時間

    public AudioClip respowanSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
  
        //物理演算拒否
        Invoke("ReturnToInitialPosition", delayTime);

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



    void ReturnToInitialPosition()
    {
        EffectInit();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(respowanSE);
        }

        // 元の位置に戻る
        Destroy(gameObject);
    }
}
