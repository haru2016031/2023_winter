using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{

    public Vector3 pPos_ = new Vector3(0, 4, 0);
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = (GameObject)Resources.Load("Player");
        // Player�v���n�u�����ɁA�C���X�^���X�𐶐��A
        Instantiate(obj, pPos_, Quaternion.identity);

        //�J�����̐���
        GameObject camera = (GameObject)Resources.Load("MainCamera");

        Instantiate(camera);


    }
}
