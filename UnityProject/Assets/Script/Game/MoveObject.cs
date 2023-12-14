using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool useUltraHundFlag;
    private bool isFreeze = false;
    [SerializeField]
    private float ultRange = 20.0f;     //ウルハンの使用範囲
    private Vector3 oldMousePos;

    void Start()
    {

    }

    void Update()
    {
        Ultrahand();
    }

    void Ultrahand()
    {
        if (useUltraHundFlag == true)
        {
            // マウスの位置からレイを飛ばし、オブジェクトをつかむ処理
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (Input.GetButtonDown("Fire1"))
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

            if (Input.GetButtonUp("Fire1"))
            {
                // マウスボタンを離したらビームを消す
                isDrag = false;
                DestroyBeam();
            }

            if (isDrag)
            {
                
                MoveObjectWithRigidbody();

                UpdateBeamPos();

                // ビームの長さを再計算
                float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
                float beamLength = distance / 30.0f;
                UpdateBeamLength(beamLength);

                //掴んでるオブジェクトの固定処理
                FreezeObject();
            }
            else
            {
                selectObject = null;
                DestroyBeam();
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
    }

    void MoveObjectWithRigidbody()
    {
        // オブジェクトをマウスの位置に移動させ、ビームを更新
        Vector3 mousePos = Input.mousePosition;
        objectDepth += Input.GetAxis("Mouse ScrollWheel") * 5.0f;

        if (Input.GetButtonDown("Scrool L1"))
        {
            objectDepth += 5.0f;
        }

        if (Input.GetButtonDown("Scrool R1"))
        {
            objectDepth -= 5.0f;
        }

        mousePos.z = objectDepth;
        var targetPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //プレイヤーから一定距離離れていたら、以下処理を無視
        var distance = Vector3.Distance(transform.position, targetPosition);
        if(ultRange < distance)
        {
            objectDepth -= Input.GetAxis("Mouse ScrollWheel") * 5.0f;

            targetPosition = oldMousePos;
        }
        if(oldMousePos != targetPosition)
        {
            oldMousePos = targetPosition;
        }
        // Rigidbodyを使ってオブジェクトを移動させる処理
        Vector3 moveDirection = (targetPosition - selectObject.transform.position).normalized;

        // 現在の速度を取得
        Vector3 currentVelocity = selectObject.GetComponent<Rigidbody>().velocity;

        // 滑らかな移動
        Vector3 smoothVelocity = Vector3.Lerp(currentVelocity,moveDirection * 10.0f,Time.deltaTime * 10.0f);

        // velocityを使用して移動させる
        selectObject.GetComponent<Rigidbody>().velocity = smoothVelocity;
    }

    void FreezeObject()
    {
        if(Input.GetButtonDown("Fire2"))
        {

            isFreeze = !isFreeze;
            Rigidbody rb = selectObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                //rb.constraints = isFreeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
                rb.isKinematic = isFreeze ? true : false;
            }
   
        }
    }

    void UpdateBeamPos()
    {
        // ビームを追従させる処理
        if (beamInstance != null)
        {
            Vector3 spawnPosition = playerTrans.position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UltraHundAria")
        {
            useUltraHundFlag = true;
            //Debug.Log("ウルトラ使える");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UltraHundAria")
        {
            useUltraHundFlag = false;
            isDrag = false;
            DestroyBeam();
            //Debug.Log("ウルトラ使えない");
        }
    }
}
