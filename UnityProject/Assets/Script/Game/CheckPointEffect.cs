using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffect : MonoBehaviour
{
    public GameObject prefab;

    private float offsetY = 2.0f;
    public AudioClip audioClip;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 spawnPos = transform.position + new Vector3(0, offsetY, 0);
            ShowEffect(spawnPos, transform.rotation);
            audioSource.PlayOneShot(audioClip);
        }
    }

    void ShowEffect(Vector3 spawnPos, Quaternion rotation)
    {
        GameObject effect = Instantiate(prefab, spawnPos, rotation);
        StartCoroutine(DestroyEffect(effect, 2.5f)); // エフェクトを1秒後に削除するCoroutineを呼び出す
    }

    IEnumerator DestroyEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
        Destroy(effect); // エフェクトを削除
    }
}
