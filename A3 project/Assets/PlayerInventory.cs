using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public int coinCount = 0;
    public UnityEvent<int> OnCoinCollected; // ����ռ��¼�

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

    // ��ӽ��
    public void AddCoins(int amount)
    {
        coinCount += amount;
        Debug.Log("���������" + coinCount);
        OnCoinCollected.Invoke(coinCount); // �����¼�
    }

    // ���ٽ��
    public void RemoveCoins(int amount)
    {
        coinCount -= amount;
        Debug.Log("���������" + coinCount);
        OnCoinCollected.Invoke(coinCount); // �����¼�
    }
}
