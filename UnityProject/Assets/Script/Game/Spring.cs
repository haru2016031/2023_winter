using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
	public AudioClip springSE;
	AudioSource audioSource;

	// �W�����v����́i������̗́j���`
	[SerializeField] private float jumpForce = 20.0f;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// Collider�����̃g���K�[�ɓ��������ɌĂяo�����
	/// </summary>
	/// <param name="other">������������̃I�u�W�F�N�g</param>
	private void OnTriggerEnter(Collider other)
	{
		// ������������̃^�O��Player�������ꍇ
		if (other.gameObject.CompareTag("Player"))
		{
			// �������������Rigidbody�R���|�[�l���g���擾���āA������̗͂�������
			var r = other.gameObject.GetComponent<Rigidbody>();
			r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
			r.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			audioSource.PlayOneShot(springSE);
		}
	}
}
