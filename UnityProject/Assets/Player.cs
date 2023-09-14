using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;  //�ړ����x
    public float jumpForce = 10.0f; // �W�����v��

    private bool isGrounded = true; // �n�ʂɐڒn���Ă��邩�ǂ����������t���O
    private Rigidbody pRigid;
    private Transform pTrans;

    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        pTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()

    {
        //CameraMove();

        Move();

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
}
