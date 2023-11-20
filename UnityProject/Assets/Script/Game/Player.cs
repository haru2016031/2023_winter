using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //移動速度
    public float jumpForce = 10.0f; // ジャンプ力
    public float pushForce = 10f;   // 吹っ飛ばす力
    public float cooldownTime = 2f; // 判定を取る間隔のクールダウン時間

    public bool isGrounded = true; // 地面に接地しているかどうかを示すフラグ
    public int jumpCnt;            //ジャンプ回数
    public int groundCollisionCnt;//ト
    private Rigidbody pRigid;       //プレイヤーのrigidbody
    private Transform pTrans;       //プレイヤーのtransform
    public Vector3 defPos;         //初期座標
    private Vector3 checkPPos;      //保持しているチェックポイント座標
    private int moveFloorTriggerCnt;//トリガー回数
    private bool canCollide = true; // 判定を取ることができるかどうかのフラグ
    private float lastCollisionTime; // 最後に判定を取った時間リガー回数

    // se
    public AudioClip jumpSE;
    AudioSource jumpSource;
    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        pTrans = GetComponent<Transform>();
        jumpSource = GetComponent<AudioSource>();
        jumpSource.volume = 0.01f;
        defPos = pTrans.position;
        checkPPos = defPos;
        moveFloorTriggerCnt = 0;
        groundCollisionCnt = 0;
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

        Fall();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // ジャンプアクションを実行
            Jump();

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
        if(pRigid.velocity.y <= 0)
        {
            pRigid.velocity = Vector3.zero;
        }

        jumpCnt++;

        if(jumpCnt >= 2)
        {
            // プレイヤーに上向きの力を加えてジャンプ
            pRigid.AddForce(Vector3.up * jumpForce*2, ForceMode.Impulse);

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

    public void JumpCollisionEnter(Collider collision)
    {
        // 衝突したオブジェクトが地面か持てるオブジェクトである場合
        groundCollisionCnt++;
        isGrounded = true;
        jumpCnt = 0;
    }

    public void JumpCollisionExit(Collider collision)
    {

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
            checkPPos = pTrans.position + new Vector3(0,2,0);
        }

        if (collision.tag == "MoveFloor")
        {
            Debug.Log("入る" + Time.time);
            this.gameObject.transform.SetParent(collision.gameObject.transform.parent.parent.transform,true);
            moveFloorTriggerCnt++;
        }

        if(collision.tag == "Balloon")
        {
            isGrounded = true;
            jumpCnt++;
        }

        if(canCollide && collision.tag == "Rock")
        {
            PushRock(collision);
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "MoveFloor")
        {
            Debug.Log("出た" + Time.time);
            if(moveFloorTriggerCnt == 1)
            {
                this.gameObject.transform.parent = null;
            }
            moveFloorTriggerCnt--;
        }
    }

    void Dead()
    {
        //高さが一定より下がるまたはRキーを押すことでプレイヤーリスポーン
        if(pTrans.position.y <= -20.0f || Input.GetKeyDown(KeyCode.R))
        {
            pTrans.position = checkPPos;
            pRigid.velocity = Vector3.zero;
        }
    }

    void Fall()
    {

    }

    void PushRock(Collider collision)
    {
        // 判定を取った後、クールダウンを開始
        canCollide = false;
        lastCollisionTime = Time.time;

        // 衝突したオブジェクトにRigidbodyがアタッチされているか確認
        Rigidbody rockRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (rockRigidbody != null)
        {
            // プレイヤーの進行方向に力を加えて吹っ飛ばす
            Vector3 pushDirection = collision.transform.forward; // 仮にプレイヤーの前方向とします
            pushDirection.x = 0.6f;
            Debug.Log(pushDirection);
            pRigid.velocity = Vector3.zero;
            pRigid.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
