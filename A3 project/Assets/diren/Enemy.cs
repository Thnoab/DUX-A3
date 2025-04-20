using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("交互设置")]
    public AudioClip interactSound; // 交互时的音效

    private bool isDefeated = false; // 是否被击败

    // 玩家进入触发范围时调用
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 确保玩家正在交互
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.SetNearbyEnemy(this);
            }
        }
    }

    // 玩家离开触发范围时调用
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 清理敌人引用
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.ClearNearbyEnemy(this);
            }
        }
    }

    // 处理交互
    public void Interact()
    {
        if (isDefeated) return; // 如果已经被击败，不做任何事情

        // 播放交互音效
        if (interactSound != null)
            AudioSource.PlayClipAtPoint(interactSound, transform.position);

        // 给金币增加5
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddCoins(5);  // 增加5金币
        }

        // 击败敌人
        Defeat();
    }

    // 击败敌人
    void Defeat()
    {
        isDefeated = true;
        // 可以在这里添加击败时的效果，例如粒子效果等
        Destroy(gameObject); // 销毁敌人
    }
}
