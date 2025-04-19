using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("��������")]
    public int coinValue = 1; // ÿ����ҵķ�ֵ

    [Header("�Ӿ�Ч��")]
    public ParticleSystem collectEffect; // ��ѡ���ռ�����Ч��

    [Header("��Ч����")]
    public AudioClip collectSound; // ��ѡ���ռ���Ч
    [Range(0, 1)] public float volume = 1f; // ��Ч��������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // ��������Ч��
        PlayCollectionEffect();

        // ������Ч
        PlayCollectionSound();

        // ���ӽ��
        AddCoinsToInventory();

        // ���ٶ���
        Destroy(gameObject);
    }

    private void PlayCollectionEffect()
    {
        if (collectEffect != null)
        {
            ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // �Զ���������ϵͳ
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
            Debug.LogError("PlayerInventory ʵ��δ�ҵ���");
        }
    }
}