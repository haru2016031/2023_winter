using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea : MonoBehaviour
{
    public float activationRange = 10.0f;  // Rigidbodyを有効にする範囲の半径
    private Rigidbody rb;
    private Vector3 initialPos = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        // Rigidbodyがアタッチされていない場合はエラーメッセージを表示
        if (rb == null)
        {
            Debug.LogError("Rigidbody not attached to the GameObject!");
        }
    }

    void Update()
    {
        // オブジェクトの現在の位置から初期位置までの距離を計算
        float distanceToInitialPosition = Vector3.Distance(transform.position, initialPos);
        // 距離がactivationRangeより小さい場合はRigidbodyを有効にし、大きい場合は無効にする
        if (distanceToInitialPosition <= activationRange)
        {
            EnableRigidbody();
        }
        else
        {
            DisableRigidbody();
        }
    }

    void EnableRigidbody()
    {
        // Rigidbodyを有効にする
        if (rb != null && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }

    void DisableRigidbody()
    {
        // Rigidbodyを無効にする
        if (rb != null && !rb.isKinematic)
        {
            rb.isKinematic = true;
        }
    }
}
