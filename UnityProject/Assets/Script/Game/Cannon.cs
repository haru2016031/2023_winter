using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject shootObj; // ���˂���I�u�W�F�N�g�̃v���n�u
    public Transform spawnPoint; // ���ˈʒu
    public float shootingForce = 10f; // ���˂����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �V���[�^�[�̑O�����ɗ͂������ăI�u�W�F�N�g�𔭎˂���
        GameObject projectile = Instantiate(shootObj, spawnPoint.position, spawnPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectile.GetComponent<UltObj>().SetRespawnFlag(false);
            projectileRb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        }
    }
}
