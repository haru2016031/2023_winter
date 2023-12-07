using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TagList
{
    Hold,
    Player

}
public class SwitchController : MonoBehaviour
{
    public Color newColor = Color.red;  // 変更後の色
    private Color originalColor;        // 初期の色
    private Renderer objectRenderer;    // スイッチのレンダラー
    public float lowerAmount = 0.5f;    // 下に移動する量
    [SerializeField]
    private float pushPower = 1.0f;     // 押す力
    [SerializeField]
    private TagList tag;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private Vector3 defPos;             // 初期座標
    private bool isPressed = false;     // 押されているか
    private bool isStarted = false;  // ギミックが起動したか
    private Vector3 m_targetPosition;   // 押される上限
    private float currentTime;          //計測時間
    private float limitTime = 0.2f;     //離れた判定の猶予時間

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        originalColor = objectRenderer.material.color;
        defPos = transform.position;
    }

    private void Update()
    {

        //ボタンの上がり下がり
        if (isPressed)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_targetPosition,
                pushPower * Time.deltaTime);

        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                defPos,
                (pushPower/2) * Time.deltaTime);

        }

        if (!isPressed && isStarted)
        {
            //ボタンから離れた判定に猶予を持たせる
            if(currentTime > limitTime)
            {
                isStarted = false;
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }

        }

        //ボタンが押される＆まだ弾を打ってない場合
        if (transform.position == m_targetPosition && !isStarted)
        {
            audioSource.PlayOneShot(audioClip);
            GetComponentInParent<SwitchManager>().SwitchFunc();
            isStarted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnterFunc(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitFunc(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnEnterFunc(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        OnExitFunc(other.gameObject);
    }


    private void OnEnterFunc(GameObject obj)
    {
        if (obj.CompareTag(tag.ToString()) && !isPressed) // プレイヤーオブジェクトに触れた場合
        {
            // 新しい色に変更
            objectRenderer.material.color = newColor;

            // 下に移動
            Vector3 newPosition = defPos;
            newPosition.y -= lowerAmount;
            m_targetPosition = newPosition;


            isPressed = true;
        }
    }

    private void OnExitFunc(GameObject obj)
    {
        if (obj.CompareTag(tag.ToString()) && isPressed) // プレイヤーオブジェクトから離れた場合
        {
            // 初期の色に戻す
            objectRenderer.material.color = originalColor;
            isPressed = false;
        }

    }
}
