using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //�ړ����x
    public float jumpForce = 10.0f; // �W�����v��
    public float pushForce = 10f;   // ������΂���
    public float pushHeight = 10f;
    public float cooldownTime = 2f; // ��������Ԋu�̃N�[���_�E������

    private bool isGrounded = true; // �n�ʂɐڒn���Ă��邩�ǂ����������t���O
    private int jumpCnt;            //�W�����v��
    private int groundCollisionCnt;//�g
    private Rigidbody pRigid;       //�v���C���[��rigidbody
    private Transform pTrans;       //�v���C���[��transform
    public Vector3 defPos;         //�������W
    private Vector3 checkPPos;      //�ێ����Ă���`�F�b�N�|�C���g���W
    private int moveFloorTriggerCnt;//�g���K�[��
    private bool canCollide = true; // ��Ƃ̔������邱�Ƃ��ł��邩�ǂ����̃t���O
    private float lastCollisionTime; // �Ō�ɔ������������ԃ��K�[��
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

            // �W�����v�A�N�V���������s
            Jump();

            // �W�����vSE�̒ǉ�
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
        if (pRigid.velocity.y <= 0)
        {
            pRigid.velocity = Vector3.zero;
        }

        jumpCnt++;

        if (jumpCnt >= 2)
        {
            // �v���C���[�ɏ�����̗͂������ăW�����v
            pRigid.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);

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


    public void JumpCollisionEnter()
    {
        // �Փ˂����I�u�W�F�N�g���n�ʂ����Ă�I�u�W�F�N�g�ł���ꍇ
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
        // �Փ˂����I�u�W�F�N�g���`�F�b�N�|�C���g�ł���ꍇ
        if (collision.gameObject.CompareTag("CheckPointCollider"))
        {
            //���ݒn�_��ێ�
            checkPPos = pTrans.position + new Vector3(0, 2, 0);
        }

        if (collision.tag == "MoveFloor")
        {
            Debug.Log("����" + Time.time);
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
            Debug.Log("�o��" + Time.time);
            if (moveFloorTriggerCnt == 1)
            {
                this.gameObject.transform.parent = null;
            }
            moveFloorTriggerCnt--;
        }
    }

    void Dead()
    {
        //����������艺����܂���R�L�[���������ƂŃv���C���[���X�|�[��
        if (pTrans.position.y <= -20.0f || Input.GetKeyDown(KeyCode.R))
        {
            pTrans.position = checkPPos;
            pRigid.velocity = Vector3.zero;
        }
    }

    void FallCheck()
    {
        // ���݂̈ʒu
        Vector3 nowPos = transform.position + new Vector3(0.0f,0.5f,-1.5f);

        // �I�u�W�F�N�g�����邩�ǂ���
        bool isGround = Physics.Raycast(nowPos, Vector3.down, 0.5f);

        // ��������
        if(!isGround)
        {
            if(!isFall && nowPos.y < lastYpos - 0.3f)
            {
                isFall = true;
                fallSource.PlayOneShot(fallVoiceSE);               
                Debug.Log("����");
            }
        }
        else 
        {
            if(isFall)
            {
                isFall = false;
                landingSource.PlayOneShot(landingSE);
                fallSource.Stop();
                Debug.Log("���n");
            }
 
        }

        Debug.DrawRay(nowPos, Vector3.down * 1.5f, Color.green);

        // y���W�̕ۑ�
        lastYpos = nowPos.y;
    }

    void Push(Collider collision)
    {
        // ������������A�N�[���_�E�����J�n
        canCollide = false;
        lastCollisionTime = Time.time;

        // �Փ˂����I�u�W�F�N�g��Rigidbody���A�^�b�`����Ă��邩�m�F
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        //if (rigidbody != null)
        {
            // �v���C���[�̐i�s�����ɗ͂������Đ�����΂�
            //Debug.Log(pRigid.velocity);
            //������΂����������߂�(�G�ꂽ���̂���v���C���[�̕���)
            Vector3 toVec = GetAngleVec(gameObject,collision.gameObject);

            //Y�����𑫂�
            toVec = toVec + new Vector3(0, pushHeight, 0);
            pRigid.velocity = Vector3.zero;

            //�ӂ��Ƃׂ���
            pRigid.AddForce(toVec * pushForce, ForceMode.Impulse); Vector3 pushDirection = -pRigid.velocity; // �v���C���[�̑��x�x�N�g���̋t����
            //Debug.Log(toVec);
            //pushDirection.x = 0.6f;
            //pRigid.velocity = Vector3.zero;
            //pRigid.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    Vector3 GetAngleVec(GameObject _from, GameObject _to)
    {
        //�����̊T�O�����Ȃ��x�N�g�������
        Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
        Vector3 toVec = new Vector3(_to.transform.position.x, 0, _to.transform.position.z);
        Debug.Log((fromVec, toVec));
        var n = Vector3.Normalize(toVec - fromVec);
        Debug.Log(n);
        return n;
    }
};
