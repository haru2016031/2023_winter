using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
	// ジャンプする力（上向きの力）を定義
	[SerializeField] private float jumpForce = 20.0f;
    /// <summary>
    /// Colliderが他のトリガーに入った時に呼び出される
    /// </summary>
    /// <param name="other">当たった相手のオブジェクト</param>
    private void OnTriggerEnter(Collider other)
	{
		// 当たった相手のタグがPlayerだった場合
		if (other.gameObject.CompareTag("Player"))
		{
			// 当たった相手のRigidbodyコンポーネントを取得して、上向きの力を加える
			var r = other.gameObject.GetComponent<Rigidbody>();
			r.velocity = Vector3.zero;//new Vector3(r.velocity.x, 0, r.velocity.z);
			r.Sleep();
			//Vector3 vec = transform.rotation * Vector3.up * jumpForce;
			Vector3 vec = transform.up * jumpForce;
			if (GetComponent<LineRenderer>())
            {
				var line = GetComponent<LineRenderer>();
				line.SetPosition(0, transform.position);
				line.SetPosition(1, transform.position + vec);
			}
			r.AddForce(vec, ForceMode.Impulse);
		}
	}
}
