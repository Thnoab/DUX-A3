using UnityEngine;

public class Ally : MonoBehaviour
{
    [Header("交互设置")]
    public int requiredInteractions = 2; // 需要的交互次数
    public float interactionCooldown = 1f; // 每次交互的冷却时间
    public AudioClip interactSound; // 交互时的音效
    [Range(0f, 1f)] public float audioVolume = 1f; // 音效音量（0 - 1之间）

    private int currentInteractions = 0; // 当前交互次数
    private float lastInteractTime = 0f; // 上次交互的时间
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
                playerInteraction.SetNearbyAlly(this); // 设定为最近的友方单位
            }
        }
    }

    // 玩家离开触发范围时调用
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 清理友方引用
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.ClearNearbyAlly(this); // 清除当前友方单位
            }
        }
    }

    // 处理交互
    public void Interact()
    {
        if (isDefeated || Time.time < lastInteractTime + interactionCooldown) return;

        currentInteractions++;
        lastInteractTime = Time.time;

        // 播放交互音效
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position, audioVolume); // 使用audioVolume来控制音量
        }

        // 检查是否达到击败条件
        if (currentInteractions >= requiredInteractions)
        {
            Defeat();
        }
    }

    // 击败友方单位（消耗金币）
    void Defeat()
    {
        isDefeated = true;

        // 扣除5金币
        PlayerInventory.Instance.RemoveCoins(5); // 这里的 RemoveCoins 方法需要你之前已经定义

        // 可以在这里添加击败时的效果，例如粒子效果等
        Destroy(gameObject); // 销毁友方单位
    }
}
