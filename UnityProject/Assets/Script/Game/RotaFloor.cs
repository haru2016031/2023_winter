using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaFloor : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // ���̉�]���x

    void Update()
    {
        // ������]������
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
