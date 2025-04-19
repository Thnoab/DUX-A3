using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("��������")]
    public float interactRange = 3f;          // ����������
    public KeyCode interactKey = KeyCode.F;   // ��������
    public LayerMask interactableLayer;       // �ɽ�������㼶

    [Header("��������")]
    public AudioClip interactSound;          // ������Ч
    public GameObject interactPrompt;        // ������ʾUI����ѡ��

    private Ally currentNearbyAlly;           // ��ǰ�������ѷ���λ
    private Enemy currentNearbyEnemy;        // ��ǰ�����ĵ���

    // ��ӵ���ģʽ
    public static PlayerInteraction Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // ���õ���ʵ��
        }
        else
        {
            Destroy(gameObject); // ȷ��ֻ��һ��ʵ��
        }
    }

    private void Update()
    {
        CheckNearbyUnits();
        HandleInteractionInput();
    }

    // ��⸽���ĵ��˺��ѷ���λ
    private void CheckNearbyUnits()
    {
        // ���μ�ⷶΧ�ڵĵ���
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            interactRange,
            interactableLayer
        );

        // ���õ�ǰ���˺��ѷ���λ����
        Ally closestAlly = null;
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // �ҳ�����ĵ��˻��ѷ���λ
        foreach (var hitCollider in hitColliders)
        {
            Ally ally = hitCollider.GetComponent<Ally>();
            if (ally != null)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestAlly = ally;
                }
            }

            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        // ���µ�ǰ���˺��ѷ���λ����
        if (currentNearbyAlly != closestAlly)
        {
            currentNearbyAlly = closestAlly;
            UpdateInteractPrompt();
        }

        if (currentNearbyEnemy != closestEnemy)
        {
            currentNearbyEnemy = closestEnemy;
            UpdateInteractPrompt();
        }
    }

    // ����������
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey))
        {
            // �������ѷ���λ�Ľ���
            if (currentNearbyAlly != null)
            {
                currentNearbyAlly.Interact(); // ���ѷ���λ����
                UpdateInteractPrompt();
            }

            // ��������˵Ľ���
            if (currentNearbyEnemy != null)
            {
                currentNearbyEnemy.Interact(); // ����˽���
                UpdateInteractPrompt();
            }
        }
    }

    // ���½�����ʾUI
    private void UpdateInteractPrompt()
    {
        if (interactPrompt != null)
        {
            bool isPromptActive = currentNearbyAlly != null || currentNearbyEnemy != null;
            interactPrompt.SetActive(isPromptActive);

            if (currentNearbyAlly != null)
            {
                // ʹ��ʾ���������
                interactPrompt.transform.position = currentNearbyAlly.transform.position + Vector3.up * 2f;
                interactPrompt.transform.rotation = Camera.main.transform.rotation;
            }

            if (currentNearbyEnemy != null)
            {
                // ʹ��ʾ���������
                interactPrompt.transform.position = currentNearbyEnemy.transform.position + Vector3.up * 2f;
                interactPrompt.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    // ���õ�ǰ�ѷ���λ
    public void SetNearbyAlly(Ally ally)
    {
        currentNearbyAlly = ally;
        UpdateInteractPrompt();  // ���½�����ʾ
    }

    // �����ǰ�ѷ���λ
    public void ClearNearbyAlly(Ally ally)
    {
        if (currentNearbyAlly == ally)
        {
            currentNearbyAlly = null;
            UpdateInteractPrompt();  // ���������ʾ
        }
    }

    // ���õ�ǰ����
    public void SetNearbyEnemy(Enemy enemy)
    {
        currentNearbyEnemy = enemy;
        UpdateInteractPrompt();  // ���½�����ʾ
    }

    // �����ǰ����
    public void ClearNearbyEnemy(Enemy enemy)
    {
        if (currentNearbyEnemy == enemy)
        {
            currentNearbyEnemy = null;
            UpdateInteractPrompt();  // ���������ʾ
        }
    }

    // ���Կ��ӻ�
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);

        if (currentNearbyAlly != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, currentNearbyAlly.transform.position);
        }

        if (currentNearbyEnemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentNearbyEnemy.transform.position);
        }
    }
}
