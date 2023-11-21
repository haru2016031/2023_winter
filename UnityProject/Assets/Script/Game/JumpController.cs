using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class JumpController : MonoBehaviour
{
    //Collision専用
    public UnityEvent<Collision> onCollisionEnter;
    public UnityEvent<Collision> onCollisionExit;

    //Trigger専用
    public UnityEvent<Collider> onColliderEnter;
    public UnityEvent<Collider> onColliderExit;
    int count = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            count++;
            //Debug.Log("入る" + Time.time+this.gameObject.name + count);
            // onColliderEnterに代入されたUnityEventを呼び出し、引数にotherを渡している
            onCollisionEnter.Invoke(other);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            count--;
            //Debug.Log("出た"+Time.time+this.gameObject.name);
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
