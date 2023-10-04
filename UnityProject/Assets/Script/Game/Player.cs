using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //�ړ����x
    public float jumpForce = 10.0f; // �W�����v��

    private bool isGrounded = true; // �n�ʂɐڒn���Ă��邩�ǂ����������t���O
    private Rigidbody pRigid;       //�v���C���[��rigidbody
    private Transform pTrans;       //�v���C���[��transform
    private Vector3 defPos;         //�������W
    private Vector3 checkPPos;      //�ێ����Ă���`�F�b�N�|�C���g���W

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
            // �W�����v�A�N�V���������s
            Jump();
        }
    }

    //�O�㍶�E�ړ�
    void Move()
    {
        // WASD�L�[�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        pRigid.velocity = moveForward * moveSpeed + new Vector3(0, pRigid.velocity.y, 0);
        //pRigid.(moveForward * moveSpeed + pRigid.position);
        //   pRigid.AddForce(moveForward * moveSpeed,ForceMode.Force);// + new Vector3(0, pRigid.velocity.y, 0);
    }

    //�W�����v
    void Jump()
    {
        // �v���C���[�ɏ�����̗͂������ăW�����v
        pRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // �n�ʂɐڒn���Ă��Ȃ���Ԃɐݒ�
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)

    {
        // �Փ˂����I�u�W�F�N�g���n�ʂł���ꍇ
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }




    private void OnTriggerEnter(Collider collision)
    {
        // �Փ˂����I�u�W�F�N�g���`�F�b�N�|�C���g�ł���ꍇ
        if (collision.gameObject.CompareTag("CheckPointCollider"))
        {
            //���ݒn�_��ێ�
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
