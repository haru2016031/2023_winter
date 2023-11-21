using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // Ú’nŒã‚Ì—‰º‚Ü‚Å‚Ì’x‰„ŠÔ
    public float shakeDuration = 1.0f; // —h‚ç‚µ‘±‚¯‚éŠÔ
    public float shakeInterval = 0.1f; // °‚ğ—h‚ç‚·ŠÔŠu
    public float shakeMagnitude = 0.1f; // —h‚ê‚Ì‹­‚³
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
        rb.isKinematic = true; // Å‰‚Í•¨—“I‚È‰e‹¿‚ğó‚¯‚È‚¢
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
        if (collision.gameObject.CompareTag("Player")) // ƒvƒŒƒCƒ„[‚ÆÚG‚µ‚½ê‡
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
        // —h‚ê‚é
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomShake = new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * shakeMagnitude;
            transform.position += randomShake;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // •¨—“I‚È‰e‹¿‚ğó‚¯‚é
        rb.isKinematic = false;

        if(!rb.isKinematic)
        {
            fallFloorSource.Stop();
            fallSouce.PlayOneShot(fallSE);
        }

        // 1•bŒã‚É°ƒIƒuƒWƒFƒNƒg‚ğŒ³‚ÌêŠ‚É–ß‚·
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
