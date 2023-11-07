using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBurst : MonoBehaviour
{
    public ParticleSystem popEffectPrefab; // エフェクトのPrefabをInspectorからアタッチする
    public float effectDuration = 2.0f; // エフェクトの再生時間
    public float respawnDelay = 2.0f; // リスポーンまでの遅延時間
    public float growthDuration = 2.0f; // 成長にかかる時間

    // SE
    public AudioClip balloonSE;
    public AudioClip expandSE;
    AudioSource audioSource;

    private bool popped = false; // 風船がすでに割れたかどうか
    [SerializeField] private Renderer[] balloonRenderer;
    private Collider balloonCollider;
    private float scale;             //デフォルトのスケール
    private float currentScale = 1.0f; // 風船の現在のスケール
    private float growthStartTime; // 成長が始まった時間

    private void Start()
    {
        // 風船のRendererとColliderコンポーネントを取得
        balloonCollider = GetComponent<Collider>();

        // 初期状態では描画と当たり判定を有効にする
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
            // 風船が割れている場合、成長アニメーションを制御
            float elapsedTime = Time.time - growthStartTime;
            if (elapsedTime < growthDuration)
            {
                // 指定の時間内に徐々にスケールを増加
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
            // プレイヤーが風船に触れたら
            popped = true; // 風船を割ったとマーク
            
            // エフェクトを再生
            if (popEffectPrefab != null)
            {
                Vector3 pos = transform.position + new Vector3(0, 2, 0);
                ParticleSystem popEffect = Instantiate(popEffectPrefab, pos, Quaternion.identity);
                audioSource.PlayOneShot(balloonSE);
                Destroy(popEffect.gameObject, popEffect.main.duration);
            }
            // 一定の遅延時間後にリスポーン
            StartCoroutine(RespawnBalloon());

            // 描画と当たり判定を無効にする
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

            // リスポーン処理
            foreach (var renderer in balloonRenderer)
            {
                renderer.enabled = true;
            }
            balloonCollider.enabled = true;

            popped = false; // 新しい風船を割れる状態にする
            // 成長アニメーションを開始
            growthStartTime = Time.time;
            currentScale = 1.0f;
            audioSource.PlayOneShot(expandSE);

        }


    }
}
