using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private GameObject selectObject = null; // 選択中のオブジェクト
    private const string holdTag = "Hold"; // オブジェクトのタグ

    private float objectDepth = 10.0f; // オブジェクトの奥行き
    private bool isDrag = false; // ドラッグ中かどうか

    // ビームプレハブとプレイヤーの位置
    public GameObject beamPrefab;
    public Transform playerTrans;
    private GameObject beamInstance;

    void Start()
    {

    }

    void Update()
    {
        Ultrahand();
    }

    void Ultrahand()
    {
        // マウスの位置からレイを飛ばし、オブジェクトをつかむ処理
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
            {
                if (hitObject.CompareTag(holdTag))
                {
                    // オブジェクトをつかみ、ビームを生成
                    selectObject = hitObject;
                    isDrag = true;
                    objectDepth = hit.distance;
                    CreateBeam();
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            // マウスボタンを離したらビームを消す
            isDrag = false;
            DestroyBeam();
        }

        if (selectObject != null)
        {
            // オブジェクトをマウスの位置に移動させ、ビームを更新
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = objectDepth;
            objectDepth += Input.GetAxis("Mouse ScrollWheel") * 5.0f;
            MoveObjectWithRigidbody(Camera.main.ScreenToWorldPoint(mousePos));

            UpdateBeamPos();

            if (!isDrag)
            {
                // ドラッグが終了したら選択を解除
                selectObject = null;
            }

            if (selectObject != null && beamInstance != null)
            {
                // ビームの長さを再計算
                float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
                float beamLength = distance / 30.0f;
                UpdateBeamLength(beamLength);
            }
        }
    }

    void CreateBeam()
    {
        // ビームを生成する処理
        Vector3 spawnPosition = playerTrans.position;
        Vector3 direction = selectObject.transform.position - playerTrans.position;
        DestroyBeam(); // 既存のビームを削除
        beamInstance = Instantiate(beamPrefab, spawnPosition, Quaternion.LookRotation(direction));

        // ビームの長さの初期設定
        float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
        float beamLength = distance / 10.0f;
        UpdateBeamLength(beamLength);
    }

    void MoveObjectWithRigidbody(Vector3 targetPosition)
    {
        // Rigidbodyを使ってオブジェクトを移動させる処理
        Vector3 moveDirection = (targetPosition - selectObject.transform.position).normalized;
        selectObject.GetComponent<Rigidbody>().velocity = moveDirection * 5f; // velocityを使用して移動させる
    }

    void UpdateBeamPos()
    {
        // ビームを追従させる処理
        if (beamInstance != null)
        {
            Vector3 spawnPosition = playerTrans.position;
            Vector3 targetPosition = spawnPosition + (selectObject.transform.position - playerTrans.position);
            Vector3 direction = selectObject.transform.position - playerTrans.position;
            beamInstance.transform.position = spawnPosition;
            beamInstance.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void UpdateBeamLength(float beamLength)
    {
        // ビームの長さを変更する処理
        if (beamInstance != null)
        {
            Vector3 newScale = beamInstance.transform.localScale;
            newScale.z = beamLength;
            beamInstance.transform.localScale = newScale;
        }
    }

    void DestroyBeam()
    {
        // ビームを削除する処理
        if (beamInstance != null)
        {
            Destroy(beamInstance);
        }
    }
}
