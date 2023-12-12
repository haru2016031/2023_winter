using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public Image image;
    private float fadeSpeed = 0.5f;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = 1.0f;

        while(alpha > 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            image.color = new Color(0,0,0, alpha);
            yield return null;
        }

        image.gameObject.SetActive(false);
    }}
