using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UICoinDisplay : MonoBehaviour
{
    [Header("显示设置")]
    public string format = "金币: {0}";
    public AudioClip celebrationSound10; // 10金币音效
    public AudioClip celebrationSound25; // 25金币音效

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
        coinText.text = string.Format(format, coins);  // 继续显示金币数量

        // 检测庆祝条件
        CheckCelebration(coins);
    }

    private void CheckCelebration(int currentCoins)
    {
        // 当金币达到10时播放音效
        if (!hasCelebrated10 && currentCoins >= 10)
        {
            Celebrate(celebrationSound10);
            hasCelebrated10 = true;
        }
        // 当金币达到25时播放音效
        else if (!hasCelebrated25 && currentCoins >= 25)
        {
            Celebrate(celebrationSound25);
            hasCelebrated25 = true;
        }
    }

    private void Celebrate(AudioClip sound)
    {
        // 播放庆祝音效
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        }
    }
}
