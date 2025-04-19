using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;  // 单例模式

    // 保存存档的场景名称
    private string[] savedScenes = new string[3];  // 3个存档槽

    void Awake()
    {
        // 确保 SaveManager 在全局只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 保持在场景之间存在
        }
        else
        {
            Destroy(gameObject);  // 销毁多余的实例
        }

        // 加载已保存的场景信息
        LoadAllSaves();
    }

    // 保存游戏到指定的存档槽
    public void SaveGame(int slot, string sceneName)
    {
        if (slot >= 1 && slot <= 3)
        {
            savedScenes[slot - 1] = sceneName;
            PlayerPrefs.SetString("SaveSlot" + slot, sceneName);  // 使用 PlayerPrefs 保存
        }
    }

    // 从指定的存档槽加载游戏
    public bool LoadGame(int slot, out string sceneName)
    {
        if (slot >= 1 && slot <= 3)
        {
            sceneName = PlayerPrefs.GetString("SaveSlot" + slot, "NoScene");
            return sceneName != "NoScene";  // 如果没有存档，则返回 false
        }

        sceneName = null;
        return false;
    }

    // 加载所有存档槽的游戏数据
    public void LoadAllSaves()
    {
        for (int i = 0; i < 3; i++)
        {
            savedScenes[i] = PlayerPrefs.GetString("SaveSlot" + (i + 1), "NoScene");
        }
    }

    // 清除存档
    public void ClearSave(int slot)
    {
        if (slot >= 1 && slot <= 3)
        {
            savedScenes[slot - 1] = "NoScene";
            PlayerPrefs.SetString("SaveSlot" + slot, "NoScene");
        }
    }
}
