using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    // �t�F�[�h�C���I����̏���
    public void EndFadeInAnimation()
    {
        Destroy(this.gameObject);
    }
}
