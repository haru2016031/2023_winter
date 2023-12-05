using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera mainCam;
    public Camera subCam;
    public GameObject player;
    private Animator animator;
    public bool switchCamflag;

    private float speed = 0.1f;
    private float minPosY = 0.0f;
    private float maxPosY = 2.0f;
    private float switchTime = 1.5f;
    private float currentTime = 0.0f;
    void Start()
    {
        mainCam.enabled = true;
        subCam.enabled = false;
        switchCamflag = false;
        player.GetComponent<GameObject>();
        animator = player.GetComponent<Animator>();
    }
    void Update()
    {
        float downSpeed = speed * (1.0f - speed / switchTime);

        Vector3 nowPos = transform.position;

        nowPos.y += downSpeed * Time.deltaTime;

        transform.position = new Vector3(nowPos.x, Mathf.Clamp(nowPos.y, minPosY, maxPosY),nowPos.z);

        if (nowPos.y >= maxPosY)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= switchTime)
            {
                switchCamflag = true;
                mainCam.enabled = false;
                subCam.enabled = true;
                animator.SetTrigger("ChangeAnimatioin");
            }
        }
    }
}
