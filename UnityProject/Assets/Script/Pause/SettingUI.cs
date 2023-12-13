using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingUI : MonoBehaviour
{
    public RectTransform separatorPanelPrefab; // �敪���p�̃p�l����Prefab
    [SerializeField]
    private Vector2[] separateList = { new Vector2(0, 200), new Vector2(0, 0) };
    [SerializeField]
    private Color separateColor = new Color(0.7f, 0.7f, 0.7f, 0.1f);

    void Start()
    {
        // �敪�����쐬
        foreach(var vec in separateList)
        {
            CreateSeparatorPanel(vec, separateColor);
        }
    }

    void CreateSeparatorPanel(Vector2 position, Color color)
    {
        // �敪���p�̃p�l�����쐬
        RectTransform separatorPanel = Instantiate(separatorPanelPrefab, transform);
        separatorPanel.anchoredPosition = position;

        // �w�i�F��ݒ�
        Image separatorImage = separatorPanel.GetComponent<Image>();
        if (separatorImage != null)
        {
            separatorImage.color = color;
        }
    }
}
