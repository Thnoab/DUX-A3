using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随设置")]
    public float distance = 5f;  // 摄像机与玩家的距离
    public float height = 2f;    // 摄像机高度
    public string playerTag = "Player"; // 玩家标签

    private Transform target;    // 玩家对象

    void Start()
    {
        // 通过标签查找玩家对象
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObj != null)
        {
            target = playerObj.transform;
            Debug.Log("成功找到玩家对象: " + target.name);
        }
        else
        {
            Debug.LogError("找不到带有'" + playerTag + "'标签的对象！");
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            // 尝试重新查找玩家（防止运行时玩家被销毁）
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null) target = playerObj.transform;
            return;
        }

        // 计算摄像机位置（在玩家后方上方）
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        transform.position = desiredPosition;

        // 摄像机看向玩家
        transform.LookAt(target.position + Vector3.up * 0.5f); // 稍微抬高观察点
    }
}