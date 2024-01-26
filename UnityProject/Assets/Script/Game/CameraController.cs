using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private GameObject mainCamera;              //���C���J�����i�[�p
    private GameObject playerObject;            //��]�̒��S�ƂȂ�v���C���[�i�[�p

    private Vector3 oldTrans;
    public Transform target; // �v���C���[��Transform
    public float rotationSpeed = 4.0f; // �J�����̉�]���x
    public Vector3 offset = new Vector3(0, 2, -5); // �J�����̑��ΓI�Ȉʒu
    public float minYAngle = -30.0f; // �ŏ���Y���p�x
    public float maxYAngle = 60.0f; // �ő��Y���p�x
    private float minXAngle = -20.0f;
    private float maxXAngle = 45.0f;

    private float currentX = 0;
    private float currentY = 0;

    private PlayerAction playerAction;
    private InputAction cameraRoll;
    private void OnEnable()
    {
        playerAction.Enable();
    }

    private void OnDisable()
    {
        playerAction.Disable();
    }
    private void Awake()
    {
        playerAction = new PlayerAction();
        cameraRoll = playerAction.Player.CameraRoll;
        cameraRoll.performed += ctx => RotateCamera(ctx.ReadValue<Vector3>());
    }


    // Start is called before the first frame update
    void Start()
    {
        //���C���J�����ƃ��j�e�B���������ꂼ��擾
        mainCamera = Camera.main.gameObject;
        playerObject = GameObject.FindWithTag("Player");
        oldTrans = playerObject.transform.position;
        mainCamera.transform.position = playerObject.transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //MoveCamera();

    }

    private void RotateCamera(Vector3 input)
    {
        currentX += input.x * rotationSpeed;
        currentY += -input.y * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minXAngle, maxXAngle);


        // �J�����̈ʒu��ݒ�
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        mainCamera.transform.position = target.position + rotation * offset;
        Vector3 t = new Vector3( target.position.x, target.position.y+2, target.position.z );
        mainCamera.transform.LookAt(t);
    }

    private void MoveCamera()
    {
       
        //�J�����̓v���C���[�Ɠ����ʒu�ɂ���
        mainCamera.transform.position += playerObject.transform.position - oldTrans;
        oldTrans = playerObject.transform.position;
    }

    public void SetCameraSpeed(float value)
    {
        rotationSpeed = value;
    }
}
