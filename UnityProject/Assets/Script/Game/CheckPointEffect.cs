using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffect : MonoBehaviour
{
    public GameObject prefab;
    public AudioClip checkPointSE;
    public AudioClip checkPointSE_2;

    AudioSource audioSource;
    private float offsetY = 2.0f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 spawnPos = transform.position + new Vector3(0, offsetY, 0);
            ShowEffect(spawnPos, transform.rotation);

            audioSource.PlayOneShot(checkPointSE);
            audioSource.PlayOneShot(checkPointSE_2);
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
        Destroy(effect); // エフェクトを削除
    }
}
