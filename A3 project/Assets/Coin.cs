using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("基础设置")]
    public int coinValue = 1; // 每个金币的分值

    [Header("视觉效果")]
    public ParticleSystem collectEffect; // 可选的收集粒子效果

    [Header("音效设置")]
    public AudioClip collectSound; // 可选的收集音效
    [Range(0, 1)] public float volume = 1f; // 音效音量控制

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // 播放粒子效果
        PlayCollectionEffect();

        // 播放音效
        PlayCollectionSound();

        // 增加金币
        AddCoinsToInventory();

        // 销毁对象
        Destroy(gameObject);
    }

    private void PlayCollectionEffect()
    {
        if (collectEffect != null)
        {
            ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // 自动清理粒子系统
        }
    }

    private void PlayCollectionSound()
    {
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
    }

    private void AddCoinsToInventory()
    {
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddCoins(coinValue);
        }
        else
        {
            Debug.LogError("PlayerInventory 实例未找到！");
        }
    }
}