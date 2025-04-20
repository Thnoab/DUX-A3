using UnityEngine;
using UnityEngine.UI;
using TMPro;  // 导入 TextMeshPro 命名空间

public class SaveMenu : MonoBehaviour
{
    public Button[] saveButtons;  // 存档按钮（3个）
    public Button[] loadButtons;  // 加载按钮（3个）
    public TextMeshProUGUI noSaveText;  // 底部显示“没有存档”提示文本 (TextMeshProUGUI)
    private SaveManager saveManager;  // 存档管理器

    void Start()
    {
        // 获取 SaveManager 脚本
        saveManager = SaveManager.Instance;

        // 确保按钮数组长度一致
        if (saveButtons.Length != loadButtons.Length || saveButtons.Length == 0)
        {
            Debug.LogError("保存按钮和加载按钮数量不匹配，或者数组未初始化！");
            return;
        }

        // 初始化按钮的监听事件
        for (int i = 0; i < saveButtons.Length; i++)  // 使用 saveButtons.Length 而不是硬编码的 3
        {
            int index = i;  // 捕获按钮的索引
            saveButtons[i].onClick.AddListener(() => SaveGame(index + 1));  // 保存到对应存档槽
            loadButtons[i].onClick.AddListener(() => LoadGame(index + 1));  // 从对应存档槽加载
        }

        // 初始化文本状态
        noSaveText.gameObject.SetActive(false);  // 默认不显示提示文本
    }

    // 保存游戏到指定的存档槽
    void SaveGame(int saveSlot)
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; // 获取当前场景名称
        saveManager.SaveGame(saveSlot, currentScene);

        // 更新存档槽状态
        UpdateSaveSlotTexts();
    }

    // 从指定的存档槽加载游戏
    void LoadGame(int saveSlot)
    {
        string sceneName;
        bool hasSave = saveManager.LoadGame(saveSlot, out sceneName);

        if (hasSave)
        {
            // 加载存档成功，隐藏提示文本
            noSaveText.gameObject.SetActive(false);
            // 这里你可以使用 SceneManager 来加载保存的场景
            // UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        else
        {
            // 如果没有存档，显示提示文本
            noSaveText.gameObject.SetActive(true);
        }
    }

    // 更新每个存档槽的文本显示
    void UpdateSaveSlotTexts()
    {
        // 更新存档槽的状态
        // 这个方法会被用于保存后更新每个存档槽的提示信息
    }
}
