using UnityEngine;

public class GlobalSettingsManager : MonoBehaviour
{
    public static GlobalSettingsManager Instance { get; private set; }

    // 视角模式
    public enum ViewMode { FirstPerson, ThirdPerson }
    public ViewMode currentViewMode = ViewMode.ThirdPerson;

    // 鼠标灵敏度
    [Header("Mouse Settings")]
    [Range(0.1f, 10f)] public float mouseXSensitivity = 2f;
    [Range(0.1f, 10f)] public float mouseYSensitivity = 2f;

    void Awake()
    {
        // 单例模式初始化（唯一入口）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings(); // 仅在首次初始化时加载设置
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 保存设置到 PlayerPrefs（唯一定义）
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ViewMode", (int)currentViewMode);
        PlayerPrefs.SetFloat("MouseX", mouseXSensitivity);
        PlayerPrefs.SetFloat("MouseY", mouseYSensitivity);
    }

    // 加载设置（唯一定义）
    private void LoadSettings()
    {
        currentViewMode = (ViewMode)PlayerPrefs.GetInt("ViewMode", 0);
        mouseXSensitivity = PlayerPrefs.GetFloat("MouseX", 2f);
        mouseYSensitivity = PlayerPrefs.GetFloat("MouseY", 2f);
    }
}
