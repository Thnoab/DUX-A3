using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.1f;          // 移动速度
    public float rotationSpeed = 10f;     // 旋转平滑度
    public float minMoveThreshold = 0.1f; // 最小移动阈值

    [Header("Component References")]
    private Rigidbody rb;                // 刚体组件
    private Vector3 lastMoveDirection;   // 记录上一次移动方向

    void Start()
    {
        // 获取或添加Rigidbody组件
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // 设置刚体属性
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // 平滑插值
    }

    void Update()
    {
        HandleMovementAndRotation();
    }

    private void HandleMovementAndRotation()
    {
        // 获取WASD输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 创建移动方向向量（基于世界坐标系）
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 只有有输入时才移动和旋转
        if (moveDirection.magnitude > minMoveThreshold)
        {
            // 移动物体
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            // 计算目标旋转角度
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // 平滑旋转到目标方向
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // 记录最后移动方向
            lastMoveDirection = moveDirection;
        }
    }
}