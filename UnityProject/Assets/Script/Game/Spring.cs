using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
	public AudioClip springSE;
	AudioSource audioSource;

	// ジャンプする力（上向きの力）を定義
	[SerializeField] private float jumpForce = 20.0f;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

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
			r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
			r.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			audioSource.PlayOneShot(springSE);
		}
	}
}
