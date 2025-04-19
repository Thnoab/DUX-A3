using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;  // 静音按钮
    private bool isMuted;      // 当前场景静音状态

    void Start()
    {
        // 检查当前场景是否已经有静音状态
        isMuted = PlayerPrefs.GetInt("SceneMute", 0) == 1;
        muteButton.onClick.AddListener(ToggleMute);
    }

    // 切换静音状态
    void ToggleMute()
    {
        // 使用 AudioManager.Instance 访问单例
        AudioManager.Instance.ToggleMute();
        isMuted = !isMuted;
    }
}
