using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入 TMP 命名空间

public class VSyncSliderControl : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider vsyncSlider;         // 滑动条
    [SerializeField] private TMP_Text vsyncOnText;       // 开启状态文本（绿色）
    [SerializeField] private TMP_Text vsyncOffText;      // 关闭状态文本（红色）

    private void Start()
    {
        // 初始化滑动条（WebGL仅支持两种模式）
        vsyncSlider.wholeNumbers = true;
        vsyncSlider.minValue = 0;
        vsyncSlider.maxValue = 1;

        // 加载保存的VSync设置（默认开启）
        int savedMode = PlayerPrefs.GetInt("WebGLVSync", 1);
        vsyncSlider.value = savedMode;
        UpdateVSyncMode(savedMode);

        // 绑定事件
        vsyncSlider.onValueChanged.AddListener(UpdateVSyncMode);
    }

    private void UpdateVSyncMode(float value)
    {
        int mode = (int)value;
        
        // WebGL平台设置逻辑
#if UNITY_WEBGL
        QualitySettings.vSyncCount = mode;
        Application.targetFrameRate = mode == 0 ? -1 : 60;
#endif

        PlayerPrefs.SetInt("WebGLVSync", mode);

        // 根据模式切换文本显示
        vsyncOnText.gameObject.SetActive(mode == 1);  // 开启时显示绿色文本
        vsyncOffText.gameObject.SetActive(mode == 0); // 关闭时显示红色文本

        Debug.Log($"垂直同步已{(mode == 1 ? "开启" : "关闭")}");
    }
}