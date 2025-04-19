using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("基本设置")]
    public float interactRange = 3f;          // 交互最大距离
    public KeyCode interactKey = KeyCode.F;   // 交互按键
    public LayerMask interactableLayer;       // 可交互物体层级

    [Header("反馈设置")]
    public AudioClip interactSound;          // 交互音效
    public GameObject interactPrompt;        // 交互提示UI（可选）

    private Ally currentNearbyAlly;           // 当前附近的友方单位
    private Enemy currentNearbyEnemy;        // 当前附近的敌人

    // 添加单例模式
    public static PlayerInteraction Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 设置单例实例
        }
        else
        {
            Destroy(gameObject); // 确保只有一个实例
        }
    }

    private void Update()
    {
        CheckNearbyUnits();
        HandleInteractionInput();
    }

    // 检测附近的敌人和友方单位
    private void CheckNearbyUnits()
    {
        // 球形检测范围内的敌人
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            interactRange,
            interactableLayer
        );

        // 重置当前敌人和友方单位引用
        Ally closestAlly = null;
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // 找出最近的敌人或友方单位
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

        // 更新当前敌人和友方单位引用
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

    // 处理交互输入
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey))
        {
            // 触发与友方单位的交互
            if (currentNearbyAlly != null)
            {
                currentNearbyAlly.Interact(); // 与友方单位交互
                UpdateInteractPrompt();
            }

            // 触发与敌人的交互
            if (currentNearbyEnemy != null)
            {
                currentNearbyEnemy.Interact(); // 与敌人交互
                UpdateInteractPrompt();
            }
        }
    }

    // 更新交互提示UI
    private void UpdateInteractPrompt()
    {
        if (interactPrompt != null)
        {
            bool isPromptActive = currentNearbyAlly != null || currentNearbyEnemy != null;
            interactPrompt.SetActive(isPromptActive);

            if (currentNearbyAlly != null)
            {
                // 使提示朝向摄像机
                interactPrompt.transform.position = currentNearbyAlly.transform.position + Vector3.up * 2f;
                interactPrompt.transform.rotation = Camera.main.transform.rotation;
            }

            if (currentNearbyEnemy != null)
            {
                // 使提示朝向摄像机
                interactPrompt.transform.position = currentNearbyEnemy.transform.position + Vector3.up * 2f;
                interactPrompt.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    // 设置当前友方单位
    public void SetNearbyAlly(Ally ally)
    {
        currentNearbyAlly = ally;
        UpdateInteractPrompt();  // 更新交互提示
    }

    // 清除当前友方单位
    public void ClearNearbyAlly(Ally ally)
    {
        if (currentNearbyAlly == ally)
        {
            currentNearbyAlly = null;
            UpdateInteractPrompt();  // 清除交互提示
        }
    }

    // 设置当前敌人
    public void SetNearbyEnemy(Enemy enemy)
    {
        currentNearbyEnemy = enemy;
        UpdateInteractPrompt();  // 更新交互提示
    }

    // 清除当前敌人
    public void ClearNearbyEnemy(Enemy enemy)
    {
        if (currentNearbyEnemy == enemy)
        {
            currentNearbyEnemy = null;
            UpdateInteractPrompt();  // 清除交互提示
        }
    }

    // 调试可视化
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
