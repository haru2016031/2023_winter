using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ParticleSystem effectPrefab; // エフェクトのプレハブ
    private ParticleSystem effectInstance; // エフェクトのインスタンス
    public GameObject shootObj; // 発射するオブジェクトのプレハブ
    public Transform spawnPoint; // 発射位置
    public float shootingForce = 10f; // 発射する力
    public AudioClip audioClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    void PlayEffect()
    {
        // エフェクトのプレハブが設定されている場合
        if (effectPrefab != null)
        {
            // エフェクトのプレハブから新しいインスタンスを生成
            effectInstance = Instantiate(effectPrefab, spawnPoint.position, Quaternion.identity);

            // エフェクトが再生し終わったらインスタンスを破棄
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }

    }

    void Shoot()
    {
        // シューターの前方向に力を加えてオブジェクトを発射する
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
