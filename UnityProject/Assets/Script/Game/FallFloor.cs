using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public float fallDelay = 2.0f; // 接地後の落下までの遅延時間
    [SerializeField] private float backTime = 1.5f;
    private Rigidbody rb;
    private Vector3 pos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // 最初は物理的な影響を受けない
        pos = GetComponent<Transform>().position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // プレイヤーと接触した場合
        {
            Invoke("Fall", fallDelay); // 遅延後に落下させる
        }
    }
    void Fall()
    {
        rb.isKinematic = false; // 物理的な影響を受ける
                                // 1秒後に床オブジェクトを非アクティブにする
        Invoke("DeactivatePlatform", backTime);
    }

    void DeactivatePlatform()
    {
        transform.position = pos;
        rb.isKinematic = true; 

    }
}
