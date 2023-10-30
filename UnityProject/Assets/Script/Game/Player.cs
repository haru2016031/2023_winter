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
    private int jumpCnt;            //�W�����v��
    private int moveFloorTriggerCnt;//�g���K�[��
    private int groundCollisionCnt;//�g���K�[��
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

    }

    //�W�����v
    void Jump()
    {
        if(pRigid.velocity.y <= 0)
        {
            pRigid.velocity = Vector3.zero;
        }

        jumpCnt++;

        if(jumpCnt >= 2)
        {
            // �v���C���[�ɏ�����̗͂������ăW�����v
            pRigid.AddForce(Vector3.up * jumpForce*2, ForceMode.Impulse);

        }
        else
        {
            pRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // �n�ʂɐڒn���Ă��Ȃ���Ԃɐݒ�
        isGrounded = false;
    }

    // �v���C���[�������̒n�ʂɐG�ꂽ�Ƃ��ɌĂяo�����\�b�h
    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }


    private void OnCollisionEnter(Collision collision)

    {
      
        // �Փ˂����I�u�W�F�N�g���n�ʂł���ꍇ
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
        // �Փ˂����I�u�W�F�N�g���`�F�b�N�|�C���g�ł���ꍇ
        if (collision.gameObject.CompareTag("CheckPointCollider"))
        {
            //���ݒn�_��ێ�
            checkPPos = pTrans.position + new Vector3(0,2,0);
        }

        if (collision.tag == "MoveFloor")
        {
            Debug.Log("����" + Time.time);
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
            Debug.Log("�o��" + Time.time);
            if(moveFloorTriggerCnt == 1)
            {
                this.gameObject.transform.parent = null;
            }
            moveFloorTriggerCnt--;
        }
    }

    void Dead()
    {
        //����������艺����܂���R�L�[���������ƂŃv���C���[���X�|�[��
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
