using UnityEngine;
using UnityEngine.SceneManagement;

public class _10 : MonoBehaviour  // 类名不能以数字开头，改为_01或Scene01
{
    // 方法1：通过场景名称跳转（推荐）
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Scene0"); // 确保场景名与Build Settings中一致
    }

    // 方法2：通过场景索引跳转（需配置Build Settings）
    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(0); // 场景0的索引是0，场景1的索引是1
    }
}