using UnityEngine;
using UnityEngine.SceneManagement;

public class _10 : MonoBehaviour  // �������������ֿ�ͷ����Ϊ_01��Scene01
{
    // ����1��ͨ������������ת���Ƽ���
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Scene0"); // ȷ����������Build Settings��һ��
    }

    // ����2��ͨ������������ת��������Build Settings��
    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(0); // ����0��������0������1��������1
    }
}