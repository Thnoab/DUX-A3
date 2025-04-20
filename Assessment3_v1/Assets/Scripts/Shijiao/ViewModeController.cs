using UnityEngine;

public class ViewModeController : MonoBehaviour
{
    [Header("Camera References")]
    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;

    void Start()
    {
        // 初始化视角
        SwitchViewMode(GlobalSettingsManager.Instance.currentViewMode);
    }

    // 切换视角模式
    public void SwitchViewMode(GlobalSettingsManager.ViewMode mode)
    {
        firstPersonCam.SetActive(mode == GlobalSettingsManager.ViewMode.FirstPerson);
        thirdPersonCam.SetActive(mode == GlobalSettingsManager.ViewMode.ThirdPerson);
        GlobalSettingsManager.Instance.currentViewMode = mode;
        GlobalSettingsManager.Instance.SaveSettings();
    }

    // 按钮调用方法
    public void ToggleViewMode()
    {
        var newMode = GlobalSettingsManager.Instance.currentViewMode == 
                     GlobalSettingsManager.ViewMode.FirstPerson ?
                     GlobalSettingsManager.ViewMode.ThirdPerson :
                     GlobalSettingsManager.ViewMode.FirstPerson;
        SwitchViewMode(newMode);
    }
}