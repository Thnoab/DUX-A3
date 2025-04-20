using UnityEngine;
using UnityEngine.UI;

public class QualityPresetSlider : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider qualitySlider; // 滑动条（范围0~2，整数步进）

    private void Start()
    {
        // 初始化滑动条
        qualitySlider.wholeNumbers = true; // 仅允许整数值
        qualitySlider.minValue = 0;
        qualitySlider.maxValue = 2;

        // 加载保存的画质设置（默认中画质）
        int savedPreset = PlayerPrefs.GetInt("QualityPreset", 1);
        qualitySlider.value = savedPreset;
        SetQualityPreset((int)savedPreset);

        // 绑定事件
        qualitySlider.onValueChanged.AddListener((value) => SetQualityPreset((int)value));
    }

    // 设置画质等级（0:低, 1:中, 2:高）
    private void SetQualityPreset(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("QualityPreset", index);
        Debug.Log($"画质已切换至: {QualitySettings.names[index]}");
    }
}