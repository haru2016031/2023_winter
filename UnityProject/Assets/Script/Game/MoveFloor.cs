using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float speed = 2.0f; // 移動速度
    private Vector3 startPosition;  //スタート地点
    private Vector3 endPosition;    //終点
    private bool movingRight = true;    //進行方向フラグ
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject endObj;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; //  初期座標保持
        endPosition = startPosition + endObj.transform.position - startObj.transform.position;    //終点座標保持
    }

    // Update is called once per frame
    void Update()
    {
        //現時点から終点まで移動させる
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
