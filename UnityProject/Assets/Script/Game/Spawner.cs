using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefabToSpawn;    //����������prefab
    public float spawnInterval = 2f;    //�����Ԋu
    public int SpawnCnt = 2;            //����������
    public Vector3 spawnRange = new Vector3(0f, 0f, 2.5f);
    private float elapsedTime = 0f;     //�o�ߎ���
    private Collider spawnAreaCollider; //�����͈͂̃R���C�_�[
    private Vector3 oldRandPoint = Vector3.zero;
    private Vector3 randomPoint = Vector3.zero;

    void Start()
    {
        // Collider���擾
        spawnAreaCollider = GetComponent<Collider>();

        if (spawnAreaCollider == null)
        {
            Debug.LogError("Collider���A�^�b�`����Ă��Ȃ�");
        }
    }

    void Update()
    {
        // �o�ߎ��Ԃ��X�V
        elapsedTime += Time.deltaTime;

        // ���̊Ԋu��Prefab�𐶐�
        if (elapsedTime >= spawnInterval)
        {
            for(int i = 0; i < SpawnCnt; i++)
            {
                SpawnPrefab();
            }
            elapsedTime = 0f; // �o�ߎ��Ԃ����Z�b�g
        }
    }

    void SpawnPrefab()
    {
        if (spawnAreaCollider != null)
        {
            // Collider���̃����_���Ȉʒu���v�Z
            Vector3 spawnPosition = RandomPointInCollider(spawnAreaCollider);

            // �����_���Ȋp�x�𐶐�
            Quaternion spawnRotation = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            // Prefab�𐶐�
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(0f,90f,0f));
            //Debug.Log(spawnPosition);
        }
    }

    Vector3 RandomPointInCollider(Collider collider)
    {
        while(Mathf.Abs( oldRandPoint.z - randomPoint.z) < spawnRange.z)
        {
            oldRandPoint = randomPoint;
            // Collider���̃����_���ȍ��W���v�Z
            randomPoint = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        }
        return randomPoint;
    }
}
