using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;

    [Header("��ת����")]
    [Tooltip("��ת�ٶȣ���/�룩")]
    public float rotationSpeed = 360f; // ֱ�ӿ�����ת�ٶ�

    private NavMeshAgent agent;
    private float targetRotationY; // Ŀ����ת�Ƕ�

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // ����NavMeshAgent�Դ�����ת
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
        // ��AD��ֱ�ӿ�����ת
        float rotateInput = Input.GetAxis("Horizontal");
        if (rotateInput != 0)
        {
            targetRotationY += rotateInput * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, targetRotationY, 0);

            // ǿ��ͬ��NavMeshAgent����ת�������ͻ��
            agent.angularSpeed = 0;
            agent.velocity = Vector3.zero;
        }
    }

    private void HandleMovement()
    {
        // ��WS�������ƶ������ڵ�ǰ����
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

    // �����ã���ʾ��ǰ����
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}