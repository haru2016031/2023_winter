using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaFloor : MonoBehaviour
{
    enum RotaDir
    {
        Right,
        Left
    }
    [SerializeField] private RotaDir rotaDir = RotaDir.Right;
    public float rotationSpeed = 30.0f; // è∞ÇÃâÒì]ë¨ìx

    void Update()
    {
        transform.Rotate((rotaDir == RotaDir.Right ? Vector3.up : Vector3.down) * rotationSpeed * Time.deltaTime);
    }
}
