using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefabToSpawn;    //����������prefab
    public float spawnInterval = 2f;    //�����Ԋu
    public int SpawnCnt = 1;            //����������
    public Vector3 spawnangle = Vector3.zero;
    private float elapsedTime = 0f;     //�o�ߎ���
    private Collider spawnAreaCollider; //�����͈͂̃R���C�_�[
    private Vector3 point = Vector3.zero;
    private bool reverseFlag;
    [SerializeField]
    private float pointIntervel = 5.0f;

    void Start()
    {
        // Collider���擾
        spawnAreaCollider = GetComponent<Collider>();
       
            Bounds colliderBounds = spawnAreaCollider.bounds;
        point = colliderBounds.center;
        point.z = spawnAreaCollider.bounds.max.z + pointIntervel/2;
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

            // Prefab�𐶐�
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(spawnangle));
        }
    }

    Vector3 RandomPointInCollider(Collider collider)
    {
        //reverceFlag�̔���
        if (reverseFlag)
        {
            
            //min����max
            point.z += pointIntervel;
            if(point.z >= collider.bounds.max.z - pointIntervel)
            {
                reverseFlag = !reverseFlag;
            }
        }
        else
        {
            //max����min
            point.z -= pointIntervel;
            if(point.z <= collider.bounds.min.z + pointIntervel)
            {
                reverseFlag = !reverseFlag;
            }
        }

        return point;
    }
}
