using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    // フェードイン終了後の処理
    public void EndFadeInAnimation()
    {
        Destroy(this.gameObject);
    }
}
