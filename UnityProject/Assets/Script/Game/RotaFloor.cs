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
    public float rotationSpeed = 30.0f; // 床の回転速度
    public float stopDuration = 1.0f;  // 停止する時間
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
                // Z軸周りに回転
                var angle = rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(angle, (rotaDir == RotaDir.Forward ? Vector3.forward : Vector3.back)) * transform.rotation;
                //transform.Rotate((rotaDir == RotaDir.Forward ? Vector3.forward : Vector3.back) * rotationSpeed * Time.deltaTime);
                // 現在の回転角度を更新
                currentRotation += rotationSpeed * Time.deltaTime;

                // 半回転ごとに停止
                if (currentRotation >= 180.0f)
                {
                    isRotating = false;
                    stopTime = Time.time;

                    // オブジェクトの角度を取得
                    Vector3 currentRotation = transform.rotation.eulerAngles;

                    float roundedZ = Mathf.Round(currentRotation.z);

                    // 四捨五入された角度を使って新しい Quaternion を作成
                    Quaternion roundedRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, roundedZ);

                    // オブジェクトに四捨五入された角度を適用
                    transform.rotation = roundedRotation;
                }
            }
            else
            {
                // 停止中
                if (Time.time - stopTime >= stopDuration)
                {
                    isRotating = true;
                    currentRotation = 0.0f;
                }
            }
        }
    }
}
