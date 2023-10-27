using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    public MoveFloor moveFloor;
    public bool gimmickFlag = false;

    void Start()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Hold")
        {
            gimmickFlag = true;
        }
        else
        {
            gimmickFlag = false;
        }
    }
}
