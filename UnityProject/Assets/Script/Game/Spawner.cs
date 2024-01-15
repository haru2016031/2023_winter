using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefabToSpawn;    //生成したいprefab
    public float spawnInterval = 2f;    //生成間隔
    public int SpawnCnt = 1;            //同時生成個数
    public Vector3 spawnRange = new Vector3(0f, 0f, 2.5f);
    private float elapsedTime = 0f;     //経過時間
    private Collider spawnAreaCollider; //生成範囲のコライダー
    private Vector3 oldPoint = Vector3.zero;
    private Vector3 point = Vector3.zero;
    private bool reverseFlag;
    [SerializeField]
    private float pointIntervel = 5.0f;

    void Start()
    {
        // Colliderを取得
        spawnAreaCollider = GetComponent<Collider>();
       
            Bounds colliderBounds = spawnAreaCollider.bounds;
        point = colliderBounds.center;
        point.z = spawnAreaCollider.bounds.max.z;
        if (spawnAreaCollider == null)
        {
            Debug.LogError("Colliderがアタッチされていない");
        }
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        // 一定の間隔でPrefabを生成
        if (elapsedTime >= spawnInterval)
        {
            for(int i = 0; i < SpawnCnt; i++)
            {
                SpawnPrefab();
            }
            elapsedTime = 0f; // 経過時間をリセット
        }
    }

    void SpawnPrefab()
    {
        if (spawnAreaCollider != null)
        {
            // Collider内のランダムな位置を計算
            Vector3 spawnPosition = RandomPointInCollider(spawnAreaCollider);

            // ランダムな角度を生成
            Quaternion spawnRotation = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            // Prefabを生成
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(0f,90f,0f));
            //Debug.Log(spawnPosition);
        }
    }

    Vector3 RandomPointInCollider(Collider collider)
    {
        //reverceFlagの判定
        if (reverseFlag)
        {
            
            //minからmax
            point.z += pointIntervel;
            if(point.z >= collider.bounds.max.z - pointIntervel)
            {
                reverseFlag = !reverseFlag;
            }
        }
        else
        {
            //maxからmin
            point.z -= pointIntervel;
            if(point.z <= collider.bounds.min.z + pointIntervel)
            {
                reverseFlag = !reverseFlag;
            }
        }

        return point;
    }
}
