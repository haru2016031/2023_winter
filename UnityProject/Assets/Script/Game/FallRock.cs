using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;
    private Quaternion rota;
    public float moveTime = 2.0f;    // 移動にかかる時間


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>().position;
        rota = GetComponent<Transform>().rotation;
        MoveObjectToTarget();

    }

    void MoveObjectToTarget()
    {

        // 一定時間後に元の位置に戻すメソッドを呼び出す
        Invoke("ReturnToInitialPosition", moveTime);
    }

    void ReturnToInitialPosition()
    {
        // 元の位置に戻る
        transform.position = pos;
        transform.rotation = rota; 
        rb.velocity = Vector3.zero;
        rb.Sleep();
        MoveObjectToTarget();
    }
}
