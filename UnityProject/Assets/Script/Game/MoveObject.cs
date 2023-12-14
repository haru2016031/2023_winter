using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    private GameObject selectObject = null; // �I�𒆂̃I�u�W�F�N�g
    private const string holdTag = "Hold"; // �I�u�W�F�N�g�̃^�O

    private float objectDepth = 10.0f; // �I�u�W�F�N�g�̉��s��
    private bool isDrag = false; // �h���b�O�����ǂ���

    // �r�[���v���n�u�ƃv���C���[�̈ʒu
    public GameObject beamPrefab;
    public Transform playerTrans;
    private GameObject beamInstance;

    public bool useUltraHundFlag;
    private bool isFreeze = false;
    [SerializeField]
    private float ultRange = 20.0f;     //�E���n���̎g�p�͈�
    private Vector3 oldMousePos;

    void Start()
    {

    }

    void Update()
    {
        Ultrahand();
    }

    void Ultrahand()
    {
        if (useUltraHundFlag == true)
        {
            // �}�E�X�̈ʒu���烌�C���΂��A�I�u�W�F�N�g�����ޏ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (Input.GetButtonDown("Fire1"))
                {
                    if (hitObject.CompareTag(holdTag))
                    {
                        // �I�u�W�F�N�g�����݁A�r�[���𐶐�
                        selectObject = hitObject;
                        isDrag = true;
                        objectDepth = hit.distance;
                        CreateBeam();
                    }
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                // �}�E�X�{�^���𗣂�����r�[��������
                isDrag = false;
                DestroyBeam();
            }

            if (isDrag)
            {
                
                MoveObjectWithRigidbody();

                UpdateBeamPos();

                // �r�[���̒������Čv�Z
                float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
                float beamLength = distance / 30.0f;
                UpdateBeamLength(beamLength);

                //�͂�ł�I�u�W�F�N�g�̌Œ菈��
                FreezeObject();
            }
            else
            {
                selectObject = null;
                DestroyBeam();
            }


        }
    }

    void CreateBeam()
    {
        // �r�[���𐶐����鏈��
        Vector3 spawnPosition = playerTrans.position;
        Vector3 direction = selectObject.transform.position - playerTrans.position;
        DestroyBeam(); // �����̃r�[�����폜
        beamInstance = Instantiate(beamPrefab, spawnPosition, Quaternion.LookRotation(direction));
    }

    void MoveObjectWithRigidbody()
    {
        // �I�u�W�F�N�g���}�E�X�̈ʒu�Ɉړ������A�r�[�����X�V
        Vector3 mousePos = Input.mousePosition;
        objectDepth += Input.GetAxis("Mouse ScrollWheel") * 5.0f;

        if (Input.GetButtonDown("Scrool L1"))
        {
            objectDepth += 5.0f;
        }

        if (Input.GetButtonDown("Scrool R1"))
        {
            objectDepth -= 5.0f;
        }

        mousePos.z = objectDepth;
        var targetPosition = Camera.main.ScreenToWorldPoint(mousePos);

        //�v���C���[�����苗������Ă�����A�ȉ������𖳎�
        var distance = Vector3.Distance(transform.position, targetPosition);
        if(ultRange < distance)
        {
            objectDepth -= Input.GetAxis("Mouse ScrollWheel") * 5.0f;

            targetPosition = oldMousePos;
        }
        if(oldMousePos != targetPosition)
        {
            oldMousePos = targetPosition;
        }
        // Rigidbody���g���ăI�u�W�F�N�g���ړ������鏈��
        Vector3 moveDirection = (targetPosition - selectObject.transform.position).normalized;

        // ���݂̑��x���擾
        Vector3 currentVelocity = selectObject.GetComponent<Rigidbody>().velocity;

        // ���炩�Ȉړ�
        Vector3 smoothVelocity = Vector3.Lerp(currentVelocity,moveDirection * 10.0f,Time.deltaTime * 10.0f);

        // velocity���g�p���Ĉړ�������
        selectObject.GetComponent<Rigidbody>().velocity = smoothVelocity;
    }

    void FreezeObject()
    {
        if(Input.GetButtonDown("Fire2"))
        {

            isFreeze = !isFreeze;
            Rigidbody rb = selectObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                //rb.constraints = isFreeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
                rb.isKinematic = isFreeze ? true : false;
            }
   
        }
    }

    void UpdateBeamPos()
    {
        // �r�[����Ǐ]�����鏈��
        if (beamInstance != null)
        {
            Vector3 spawnPosition = playerTrans.position;
            Vector3 direction = selectObject.transform.position - playerTrans.position;
            beamInstance.transform.position = spawnPosition;
            beamInstance.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void UpdateBeamLength(float beamLength)
    {
        // �r�[���̒�����ύX���鏈��
        if (beamInstance != null)
        {
            Vector3 newScale = beamInstance.transform.localScale;
            newScale.z = beamLength;
            beamInstance.transform.localScale = newScale;
        }
    }

    void DestroyBeam()
    {
        // �r�[�����폜���鏈��
        if (beamInstance != null)
        {
            Destroy(beamInstance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UltraHundAria")
        {
            useUltraHundFlag = true;
            //Debug.Log("�E���g���g����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UltraHundAria")
        {
            useUltraHundFlag = false;
            isDrag = false;
            DestroyBeam();
            //Debug.Log("�E���g���g���Ȃ�");
        }
    }
}
