using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // ���¼��ص�ǰ����
    public void RestartCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // ��ת��ָ�����������ʼ������
    public void RestartToInitialScene()
    {
        SceneManager.LoadScene(0); // 0�ǳ�ʼ����������
    }
}