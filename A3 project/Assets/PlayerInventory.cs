using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public int coinCount = 0;
    public UnityEvent<int> OnCoinCollected; // 金币收集事件

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 添加金币
    public void AddCoins(int amount)
    {
        coinCount += amount;
        Debug.Log("金币数量：" + coinCount);
        OnCoinCollected.Invoke(coinCount); // 触发事件
    }

    // 减少金币
    public void RemoveCoins(int amount)
    {
        coinCount -= amount;
        Debug.Log("金币数量：" + coinCount);
        OnCoinCollected.Invoke(coinCount); // 触发事件
    }
}
