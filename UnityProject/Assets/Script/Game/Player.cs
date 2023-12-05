using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //移動速度
    public float jumpForce = 10.0f; // ジャンプ力
    public float pushForce = 10f;   // 吹っ飛ばす力
    public float pushHeight = 10f;
    public float cooldownTime = 2f; // 判定を取る間隔のクールダウン時間

    private bool isGrounded = true; // 地面に接地しているかどうかを示すフラグ
    private int jumpCnt;            //ジャンプ回数
    private int groundCollisionCnt;//ト
    private Rigidbody pRigid;       //プレイヤーのrigidbody
    private Transform pTrans;       //プレイヤーのtransform
    public Vector3 defPos;         //初期座標
    private Vector3 checkPPos;      //保持しているチェックポイント座標
    private int moveFloorTriggerCnt;//トリガー回数
    private bool canCollide = true; // 岩との判定を取ることができるかどうかのフラグ
    private float lastCollisionTime; // 最後に判定を取った時間リガー回数
    private float lastYpos;
    private bool isFall = true;

    // se
    public AudioClip jumpSE;
    public AudioClip fallVoiceSE;
    public AudioClip landingSE;
    public AudioSource jumpSource;
    public AudioSource fallSource;
    public AudioSource landingSource;

    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        pTrans = GetComponent<Transform>();
        defPos = pTrans.position;
        checkPPos = defPos;
        moveFloorTriggerCnt = 0;
        groundCollisionCnt = 0;
        jumpSource.volume = 0.5f;
        fallSource.volume = 0.5f;
        landingSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //CameraMove();
        if (canCollide)
        {
            Move();
        }

        Dead();

        FallCheck();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            // ジャンプアクションを実行
            Jump();

            // ジャンプSEの追加
            jumpSource.PlayOneShot(jumpSE);
        }

        // クールダウンが終了したら判定を再度取れるようにする
        if (!canCollide && Time.time - lastCollisionTime >= cooldownTime)
        {
            canCollide = true;
        }
    }

    //前後左右移動
    void Move()
    {
        // WASDキーの入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        pRigid.velocity = moveForward * moveSpeed + new Vector3(0, pRigid.velocity.y, 0);

    }

    //ジャンプ
    void Jump()
    {
        if (pRigid.velocity.y <= 0)
        {
            pRigid.velocity = Vector3.zero;
        }

        jumpCnt++;

        if (jumpCnt >= 2)
        {
            // プレイヤーに上向きの力を加えてジャンプ
            pRigid.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);

        }
        else
        {
            pRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 地面に接地していない状態に設定
        isGrounded = false;
    }

    // プレイヤーが何かの地面に触れたときに呼び出すメソッド
    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }


    public void JumpCollisionEnter()
    {
        // 衝突したオブジェクトが地面か持てるオブジェクトである場合
        groundCollisionCnt++;
        isGrounded = true;
        jumpCnt = 0;
        //Debug.Log(groundCollisionCnt);
    }

    public void JumpCollisionExit()
    {
        //Debug.Log(this);
        if (groundCollisionCnt == 1)
        {
            isGrounded = false;

        }
        groundCollisionCnt--;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // 衝突したオブジェクトがチェックポイントである場合
        if (collision.gameObject.CompareTag("CheckPointCollider"))
        {
            //現在地点を保持
            checkPPos = pTrans.position + new Vector3(0, 2, 0);
        }

        if (collision.tag == "MoveFloor")
        {
            Debug.Log("入る" + Time.time);
            this.gameObject.transform.SetParent(collision.gameObject.transform.parent.parent.transform, true);
            moveFloorTriggerCnt++;
        }

        if (collision.tag == "Balloon")
        {
            isGrounded = true;
            jumpCnt++;
        }

        if (canCollide && collision.tag == "Push")
        {
            Push(collision);
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "MoveFloor")
        {
            Debug.Log("出た" + Time.time);
            if (moveFloorTriggerCnt == 1)
            {
                this.gameObject.transform.parent = null;
            }
            moveFloorTriggerCnt--;
        }
    }

    void Dead()
    {
        //高さが一定より下がるまたはRキーを押すことでプレイヤーリスポーン
        if (pTrans.position.y <= -20.0f || Input.GetKeyDown(KeyCode.R))
        {
            pTrans.position = checkPPos;
            pRigid.velocity = Vector3.zero;
        }
    }

    void FallCheck()
    {
        // 現在の位置
        Vector3 nowPos = transform.position + new Vector3(0.0f,0.5f,-1.5f);

        // オブジェクトがあるかどうか
        bool isGround = Physics.Raycast(nowPos, Vector3.down, 0.5f);

        // 落下判定
        if(!isGround)
        {
            if(!isFall && nowPos.y < lastYpos - 0.3f)
            {
                isFall = true;
                fallSource.PlayOneShot(fallVoiceSE);               
                Debug.Log("落下");
            }
        }
        else 
        {
            if(isFall)
            {
                isFall = false;
                landingSource.PlayOneShot(landingSE);
                fallSource.Stop();
                Debug.Log("着地");
            }
 
        }

        Debug.DrawRay(nowPos, Vector3.down * 1.5f, Color.green);

        // y座標の保存
        lastYpos = nowPos.y;
    }

    void Push(Collider collision)
    {
        // 判定を取った後、クールダウンを開始
        canCollide = false;
        lastCollisionTime = Time.time;

        // 衝突したオブジェクトにRigidbodyがアタッチされているか確認
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        //if (rigidbody != null)
        {
            // プレイヤーの進行方向に力を加えて吹っ飛ばす
            //Debug.Log(pRigid.velocity);
            //吹き飛ばす方向を求める(触れたものからプレイヤーの方向)
            Vector3 toVec = GetAngleVec(gameObject,collision.gameObject);

            //Y方向を足す
            toVec = toVec + new Vector3(0, pushHeight, 0);
            pRigid.velocity = Vector3.zero;

            //ふきとべええ
            pRigid.AddForce(toVec * pushForce, ForceMode.Impulse); Vector3 pushDirection = -pRigid.velocity; // プレイヤーの速度ベクトルの逆方向
            //Debug.Log(toVec);
            //pushDirection.x = 0.6f;
            //pRigid.velocity = Vector3.zero;
            //pRigid.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    Vector3 GetAngleVec(GameObject _from, GameObject _to)
    {
        //高さの概念を入れないベクトルを作る
        Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
        Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);
        Debug.Log((fromVec, toVec));
        var n = Vector3.Normalize(toVec - fromVec);
        Debug.Log(n);
        return n;
    }
};
