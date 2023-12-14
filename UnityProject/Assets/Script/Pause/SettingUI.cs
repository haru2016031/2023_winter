using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingUI : MonoBehaviour
{
    public RectTransform separatorPanelPrefab; // 区分線用のパネルのPrefab
    [SerializeField]
    private Vector2[] separateList = { new Vector2(0, 200), new Vector2(0, 0) };
    [SerializeField]
    private Color separateColor = new Color(0.7f, 0.7f, 0.7f, 0.1f);

    void Start()
    {
        // 区分線を作成
        foreach(var vec in separateList)
        {
            CreateSeparatorPanel(vec, separateColor);
        }
    }

    void CreateSeparatorPanel(Vector2 position, Color color)
    {
        // 区分線用のパネルを作成
        RectTransform separatorPanel = Instantiate(separatorPanelPrefab, transform);
        separatorPanel.anchoredPosition = position;

        // 背景色を設定
        Image separatorImage = separatorPanel.GetComponent<Image>();
        if (separatorImage != null)
        {
            separatorImage.color = color;
        }
    }
}
