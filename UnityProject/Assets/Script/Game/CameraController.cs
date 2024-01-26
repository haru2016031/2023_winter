using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private GameObject mainCamera;              //メインカメラ格納用
    private GameObject playerObject;            //回転の中心となるプレイヤー格納用

    private Vector3 oldTrans;
    public Transform target; // プレイヤーのTransform
    public float rotationSpeed = 4.0f; // カメラの回転速度
    public Vector3 offset = new Vector3(0, 2, -5); // カメラの相対的な位置
    public float minYAngle = -30.0f; // 最小のY軸角度
    public float maxYAngle = 60.0f; // 最大のY軸角度
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
        //メインカメラとユニティちゃんをそれぞれ取得
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


        // カメラの位置を設定
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        mainCamera.transform.position = target.position + rotation * offset;
        Vector3 t = new Vector3( target.position.x, target.position.y+2, target.position.z );
        mainCamera.transform.LookAt(t);
    }

    private void MoveCamera()
    {
       
        //カメラはプレイヤーと同じ位置にする
        mainCamera.transform.position += playerObject.transform.position - oldTrans;
        oldTrans = playerObject.transform.position;
    }

    public void SetCameraSpeed(float value)
    {
        rotationSpeed = value;
    }
}
