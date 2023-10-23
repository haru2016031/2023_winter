using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {

    }

    void Update()
    {
        Ultrahand();
    }

    void Ultrahand()
    {
        // �}�E�X�̈ʒu���烌�C���΂��A�I�u�W�F�N�g�����ޏ���
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
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

        if (Input.GetMouseButtonUp(1))
        {
            // �}�E�X�{�^���𗣂�����r�[��������
            isDrag = false;
            DestroyBeam();
        }

        if (selectObject != null)
        {
            // �I�u�W�F�N�g���}�E�X�̈ʒu�Ɉړ������A�r�[�����X�V
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = objectDepth;
            objectDepth += Input.GetAxis("Mouse ScrollWheel") * 5.0f;
            MoveObjectWithRigidbody(Camera.main.ScreenToWorldPoint(mousePos));

            UpdateBeamPos();

            if (!isDrag)
            {
                // �h���b�O���I��������I��������
                selectObject = null;
            }

            if (selectObject != null && beamInstance != null)
            {
                // �r�[���̒������Čv�Z
                float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
                float beamLength = distance / 30.0f;
                UpdateBeamLength(beamLength);
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

        // �r�[���̒����̏����ݒ�
        float distance = Vector3.Distance(playerTrans.position, selectObject.transform.position);
        float beamLength = distance / 10.0f;
        UpdateBeamLength(beamLength);
    }

    void MoveObjectWithRigidbody(Vector3 targetPosition)
    {
        // Rigidbody���g���ăI�u�W�F�N�g���ړ������鏈��
        Vector3 moveDirection = (targetPosition - selectObject.transform.position).normalized;
        selectObject.GetComponent<Rigidbody>().velocity = moveDirection * 5f; // velocity���g�p���Ĉړ�������
    }

    void UpdateBeamPos()
    {
        // �r�[����Ǐ]�����鏈��
        if (beamInstance != null)
        {
            Vector3 spawnPosition = playerTrans.position;
            Vector3 targetPosition = spawnPosition + (selectObject.transform.position - playerTrans.position);
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
}
