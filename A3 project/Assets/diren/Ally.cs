using UnityEngine;

public class Ally : MonoBehaviour
{
    [Header("��������")]
    public int requiredInteractions = 2; // ��Ҫ�Ľ�������
    public float interactionCooldown = 1f; // ÿ�ν�������ȴʱ��
    public AudioClip interactSound; // ����ʱ����Ч
    [Range(0f, 1f)] public float audioVolume = 1f; // ��Ч������0 - 1֮�䣩

    private int currentInteractions = 0; // ��ǰ��������
    private float lastInteractTime = 0f; // �ϴν�����ʱ��
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
                playerInteraction.SetNearbyAlly(this); // �趨Ϊ������ѷ���λ
            }
        }
    }

    // ����뿪������Χʱ����
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �����ѷ�����
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.ClearNearbyAlly(this); // �����ǰ�ѷ���λ
            }
        }
    }

    // ������
    public void Interact()
    {
        if (isDefeated || Time.time < lastInteractTime + interactionCooldown) return;

        currentInteractions++;
        lastInteractTime = Time.time;

        // ���Ž�����Ч
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position, audioVolume); // ʹ��audioVolume����������
        }

        // ����Ƿ�ﵽ��������
        if (currentInteractions >= requiredInteractions)
        {
            Defeat();
        }
    }

    // �����ѷ���λ�����Ľ�ң�
    void Defeat()
    {
        isDefeated = true;

        // �۳�5���
        PlayerInventory.Instance.RemoveCoins(5); // ����� RemoveCoins ������Ҫ��֮ǰ�Ѿ�����

        // ������������ӻ���ʱ��Ч������������Ч����
        Destroy(gameObject); // �����ѷ���λ
    }
}
