using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class JumpController : MonoBehaviour
{
    //Collision��p
    public UnityEvent<Collision> onCollisionEnter;
    public UnityEvent<Collision> onCollisionExit;

    //Trigger��p
    public UnityEvent<Collider> onColliderEnter;
    public UnityEvent<Collider> onColliderExit;
    int count = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            count++;
            //Debug.Log("����" + Time.time+this.gameObject.name + count);
            // onColliderEnter�ɑ�����ꂽUnityEvent���Ăяo���A������other��n���Ă���
            onCollisionEnter.Invoke(other);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            count--;
            //Debug.Log("�o��"+Time.time+this.gameObject.name);
            onCollisionExit.Invoke(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            onColliderEnter.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            onColliderExit.Invoke(other);
        }
    }
}
