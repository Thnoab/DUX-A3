using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("��������")]
    public AudioClip interactSound; // ����ʱ����Ч

    private bool isDefeated = false; // �Ƿ񱻻���

    // ��ҽ��봥����Χʱ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ȷ��������ڽ���
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.SetNearbyEnemy(this);
            }
        }
    }

    // ����뿪������Χʱ����
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �����������
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.ClearNearbyEnemy(this);
            }
        }
    }

    // ������
    public void Interact()
    {
        if (isDefeated) return; // ����Ѿ������ܣ������κ�����

        // ���Ž�����Ч
        if (interactSound != null)
            AudioSource.PlayClipAtPoint(interactSound, transform.position);

        // ���������5
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddCoins(5);  // ����5���
        }

        // ���ܵ���
        Defeat();
    }

    // ���ܵ���
    void Defeat()
    {
        isDefeated = true;
        // ������������ӻ���ʱ��Ч������������Ч����
        Destroy(gameObject); // ���ٵ���
    }
}
