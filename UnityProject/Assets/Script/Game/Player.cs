using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //�ړ����x
    public float jumpForce = 10.0f; // �W�����v��
    public float pushForce = 10f;   // ������΂���
    public float cooldownTime = 2f; // ��������Ԋu�̃N�[���_�E������

    public bool isGrounded = true; // �n�ʂɐڒn���Ă��邩�ǂ����������t���O
    public int jumpCnt;            //�W�����v��
    public int groundCollisionCnt;//�g
    private Rigidbody pRigid;       //�v���C���[��rigidbody
    private Transform pTrans;       //�v���C���[��transform
    public Vector3 defPos;         //�������W
    private Vector3 checkPPos;      //�ێ����Ă���`�F�b�N�|�C���g���W
    private int moveFloorTriggerCnt;//�g���K�[��
    private bool canCollide = true; // �������邱�Ƃ��ł��邩�ǂ����̃t���O
    private float lastCollisionTime; // �Ō�ɔ������������ԃ��K�[��

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
            // �W�����v�A�N�V���������s
            Jump();

            jumpSource.PlayOneShot(jumpSE);
        }

        // �N�[���_�E�����I�������画����ēx����悤�ɂ���
        if (!canCollide && Time.time - lastCollisionTime >= cooldownTime)
        {
            canCollide = true;
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

    public void JumpCollisionEnter(Collider collision)
    {
        // �Փ˂����I�u�W�F�N�g���n�ʂ����Ă�I�u�W�F�N�g�ł���ꍇ
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

        if(canCollide && collision.tag == "Rock")
        {
            PushRock(collision);
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

    void PushRock(Collider collision)
    {
        // ������������A�N�[���_�E�����J�n
        canCollide = false;
        lastCollisionTime = Time.time;

        // �Փ˂����I�u�W�F�N�g��Rigidbody���A�^�b�`����Ă��邩�m�F
        Rigidbody rockRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (rockRigidbody != null)
        {
            // �v���C���[�̐i�s�����ɗ͂������Đ�����΂�
            Vector3 pushDirection = collision.transform.forward; // ���Ƀv���C���[�̑O�����Ƃ��܂�
            pushDirection.x = 0.6f;
            Debug.Log(pushDirection);
            pRigid.velocity = Vector3.zero;
            pRigid.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
