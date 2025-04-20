using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;

    [Header("旋转设置")]
    [Tooltip("旋转速度（度/秒）")]
    public float rotationSpeed = 360f; // 直接控制旋转速度

    private NavMeshAgent agent;
    private float targetRotationY; // 目标旋转角度

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // 禁用NavMeshAgent自带的旋转
        agent.speed = moveSpeed;
        targetRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        // 用AD键直接控制旋转
        float rotateInput = Input.GetAxis("Horizontal");
        if (rotateInput != 0)
        {
            targetRotationY += rotateInput * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, targetRotationY, 0);

            // 强制同步NavMeshAgent的旋转（避免冲突）
            agent.angularSpeed = 0;
            agent.velocity = Vector3.zero;
        }
    }

    private void HandleMovement()
    {
        // 用WS键控制移动（基于当前朝向）
        float moveInput = Input.GetAxis("Vertical");
        if (moveInput != 0)
        {
            Vector3 moveDirection = transform.forward * moveInput;
            agent.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            agent.velocity = Vector3.zero;
        }
    }

    // 调试用：显示当前朝向
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}