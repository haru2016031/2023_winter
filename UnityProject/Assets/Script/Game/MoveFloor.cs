using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float speed = 2.0f; // �ړ����x
    private Vector3 startPosition;  //�X�^�[�g�n�_
    private Vector3 endPosition;    //�I�_
    private bool movingRight = true;    //�i�s�����t���O
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject endObj;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; //  �������W�ێ�
        endPosition = startPosition + endObj.transform.position - startObj.transform.position;    //�I�_���W�ێ�
    }

    // Update is called once per frame
    void Update()
    {
        //�����_����I�_�܂ňړ�������
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
                movingRight = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
                movingRight = true;
        }
    }
}
