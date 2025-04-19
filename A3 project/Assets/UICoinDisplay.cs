using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UICoinDisplay : MonoBehaviour
{
    [Header("��ʾ����")]
    public string format = "���: {0}";
    public AudioClip celebrationSound10; // 10�����Ч
    public AudioClip celebrationSound25; // 25�����Ч

    private Text coinText;
    private bool hasCelebrated10 = false;
    private bool hasCelebrated25 = false;

    private void Awake()
    {
        coinText = GetComponent<Text>();
    }

    private void Update()
    {
        if (PlayerInventory.Instance == null) return;

        int coins = PlayerInventory.Instance.coinCount;
        coinText.text = string.Format(format, coins);  // ������ʾ�������

        // �����ף����
        CheckCelebration(coins);
    }

    private void CheckCelebration(int currentCoins)
    {
        // ����Ҵﵽ10ʱ������Ч
        if (!hasCelebrated10 && currentCoins >= 10)
        {
            Celebrate(celebrationSound10);
            hasCelebrated10 = true;
        }
        // ����Ҵﵽ25ʱ������Ч
        else if (!hasCelebrated25 && currentCoins >= 25)
        {
            Celebrate(celebrationSound25);
            hasCelebrated25 = true;
        }
    }

    private void Celebrate(AudioClip sound)
    {
        // ������ף��Ч
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        }
    }
}
