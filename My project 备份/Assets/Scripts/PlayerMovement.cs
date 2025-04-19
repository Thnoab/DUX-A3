using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.1f;          // �ƶ��ٶ�
    public float rotationSpeed = 10f;     // ��תƽ����
    public float minMoveThreshold = 0.1f; // ��С�ƶ���ֵ

    [Header("Component References")]
    private Rigidbody rb;                // �������
    private Vector3 lastMoveDirection;   // ��¼��һ���ƶ�����

    void Start()
    {
        // ��ȡ�����Rigidbody���
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // ���ø�������
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // ƽ����ֵ
    }

    void Update()
    {
        HandleMovementAndRotation();
    }

    private void HandleMovementAndRotation()
    {
        // ��ȡWASD����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �����ƶ�����������������������ϵ��
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // ֻ��������ʱ���ƶ�����ת
        if (moveDirection.magnitude > minMoveThreshold)
        {
            // �ƶ�����
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            // ����Ŀ����ת�Ƕ�
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // ƽ����ת��Ŀ�귽��
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // ��¼����ƶ�����
            lastMoveDirection = moveDirection;
        }
    }
}