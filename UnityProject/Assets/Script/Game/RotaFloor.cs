using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaFloor : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // °‚Ì‰ñ“]‘¬“x

    void Update()
    {
        // °‚ğ‰ñ“]‚³‚¹‚é
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
