using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // 重新加载当前场景
    public void RestartCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // 跳转到指定场景（如初始场景）
    public void RestartToInitialScene()
    {
        SceneManager.LoadScene(0); // 0是初始场景的索引
    }
}