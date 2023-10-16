using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaFloor : MonoBehaviour
{
    enum RotaDir
    {
        Right,
        Left,
        Forward,
        Back
    }
    [SerializeField] private RotaDir rotaDir = RotaDir.Right;
    public float rotationSpeed = 30.0f; // ���̉�]���x
    public float stopDuration = 1.0f;  // ��~���鎞��
    private float currentRotation = 0.0f;
    private bool isRotating = true;
    private float stopTime = 0.0f;

    void Update()
    {
        if(rotaDir == RotaDir.Left || rotaDir == RotaDir.Right)
        {
            transform.Rotate((rotaDir == RotaDir.Right ? Vector3.up : Vector3.down) * rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (isRotating)
            {
                // Z������ɉ�]
                var angle = rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(angle, (rotaDir == RotaDir.Forward ? Vector3.forward : Vector3.back)) * transform.rotation;
                //transform.Rotate((rotaDir == RotaDir.Forward ? Vector3.forward : Vector3.back) * rotationSpeed * Time.deltaTime);
                // ���݂̉�]�p�x���X�V
                currentRotation += rotationSpeed * Time.deltaTime;

                // ����]���Ƃɒ�~
                if (currentRotation >= 180.0f)
                {
                    isRotating = false;
                    stopTime = Time.time;
                }
            }
            else
            {
                // ��~��
                if (Time.time - stopTime >= stopDuration)
                {
                    isRotating = true;
                    currentRotation = 0.0f;
                }
            }
        }
    }
}
