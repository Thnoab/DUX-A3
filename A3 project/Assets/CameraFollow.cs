using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("��������")]
    public float distance = 5f;  // ���������ҵľ���
    public float height = 2f;    // ������߶�
    public string playerTag = "Player"; // ��ұ�ǩ

    private Transform target;    // ��Ҷ���

    void Start()
    {
        // ͨ����ǩ������Ҷ���
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObj != null)
        {
            target = playerObj.transform;
            Debug.Log("�ɹ��ҵ���Ҷ���: " + target.name);
        }
        else
        {
            Debug.LogError("�Ҳ�������'" + playerTag + "'��ǩ�Ķ���");
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            // �������²�����ң���ֹ����ʱ��ұ����٣�
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null) target = playerObj.transform;
            return;
        }

        // ���������λ�ã�����Һ��Ϸ���
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        transform.position = desiredPosition;

        // ������������
        transform.LookAt(target.position + Vector3.up * 0.5f); // ��΢̧�߹۲��
    }
}