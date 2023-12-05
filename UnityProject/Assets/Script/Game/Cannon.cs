using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject shootObj; // 発射するオブジェクトのプレハブ
    public Transform spawnPoint; // 発射位置
    public float shootingForce = 10f; // 発射する力

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // シューターの前方向に力を加えてオブジェクトを発射する
        GameObject projectile = Instantiate(shootObj, spawnPoint.position, spawnPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectile.GetComponent<UltObj>().SetRespawnFlag(false);
            projectileRb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        }
    }
}
