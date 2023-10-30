using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //移動速度
    public float jumpForce = 10.0f; // ジャンプ力

    private bool isGrounded = true; // 地面に接地しているかどうかを示すフラグ
    private Rigidbody pRigid;       //プレイヤーのrigidbody
    private Transform pTrans;       //プレイヤーのtransform
    private Vector3 defPos;         //初期座標
    private Vector3 checkPPos;      //保持しているチェックポイント座標
    private int jumpCnt;            //ジャンプ回数
    private int moveFloorTriggerCnt;//トリガー回数
    private int groundCollisionCnt;//トリガー回数
    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        pTrans = GetComponent<Transform>();
        defPos = pTrans.position;
        checkPPos = defPos;
        moveFloorTriggerCnt = 0;
        groundCollisionCnt = 0;
    }

    // Update is called once per frame
    void Update()

    {
        //CameraMove();
        Move();

        Dead();

        Fall();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // ジャンプアクションを実行
            Jump();
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


    private void OnCollisionEnter(Collision collision)

    {
      
        // 衝突したオブジェクトが地面である場合
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCollisionCnt++;
            isGrounded = true;
            jumpCnt = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (groundCollisionCnt == 1)
            {
                isGrounded = false;
            }
            groundCollisionCnt--;
        }

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
}
