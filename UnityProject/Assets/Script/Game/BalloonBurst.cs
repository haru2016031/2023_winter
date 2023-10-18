using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBurst : MonoBehaviour
{
    public ParticleSystem popEffectPrefab; // エフェクトのPrefabをInspectorからアタッチする
    public float effectDuration = 2.0f; // エフェクトの再生時間

    private bool popped = false; // 風船がすでに割れたかどうか

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
                Destroy(popEffect.gameObject, popEffect.main.duration);
            }

            // 風船を削除
            Destroy(gameObject);
        }
    }
}
