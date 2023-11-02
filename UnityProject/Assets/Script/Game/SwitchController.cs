using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Color newColor = Color.red; // 変更後の色
    private Color originalColor; // 初期の色
    private Renderer objectRenderer;
    public float lowerAmount = 0.5f; // 下に移動する量
    private bool isLowered = false; // 下に移動したかどうかのフラグ

    private void Start()
    {
        objectRenderer = GetComponent < Renderer>();
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hold") && !isLowered) // プレイヤーオブジェクトに触れた場合
        {
            // 新しい色に変更
            objectRenderer.material.color = newColor;

            // 下に移動
            Vector3 newPosition = transform.position;
            newPosition.y -= lowerAmount;
            transform.position = newPosition;

            isLowered = true;
            SwitchManager.DoorOpen();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hold") && isLowered) // プレイヤーオブジェクトから離れた場合
        {
            // 初期の色に戻す
            objectRenderer.material.color = originalColor;


        }
    }
}
