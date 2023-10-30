using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
	// �W�����v����́i������̗́j���`
	[SerializeField] private float jumpForce = 20.0f;
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
