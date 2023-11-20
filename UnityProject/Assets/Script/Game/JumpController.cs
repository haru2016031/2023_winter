using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class JumpController : MonoBehaviour
{
    public UnityEvent<Collider> onCollisionEnter;
    public UnityEvent<Collider> onCollisionExit;

    private void OnCollisionStay(Collision other)
    {
        Debug.Log(other);
        // onColliderEnterに代入されたUnityEventを呼び出し、引数にotherを渡している
        //onCollisionEnter.Invoke(other);

    }
    private void OnCollisionExit(Collision other)
    {
        Debug.Log(other);
        //onCollisionExit.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.CompareTag("Ground"))
        {
            onCollisionEnter.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.CompareTag("Ground"))
        {
            onCollisionExit.Invoke(other);
        }
    }
}
