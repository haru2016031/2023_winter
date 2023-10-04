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

    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        pTrans = GetComponent<Transform>();
        defPos = pTrans.position;
        checkPPos = defPos;
    }

    // Update is called once per frame
    void Update()

    {
        //CameraMove();
        Move();

        Dead();

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
        //pRigid.(moveForward * moveSpeed + pRigid.position);
        //   pRigid.AddForce(moveForward * moveSpeed,ForceMode.Force);// + new Vector3(0, pRigid.velocity.y, 0);
    }

    //ジャンプ
    void Jump()
    {
        // プレイヤーに上向きの力を加えてジャンプ
        pRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // 地面に接地していない状態に設定
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)

    {
        // 衝突したオブジェクトが地面である場合
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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
            this.gameObject.transform.parent = collision.gameObject.transform;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "MoveFloor")
        {
            this.gameObject.transform.parent = null;
        }
    }

    void Dead()
    {
        if(pTrans.position.y <= -20.0f)
        {
            pTrans.position = checkPPos;
            pRigid.velocity = Vector3.zero;
        }
    }
}
