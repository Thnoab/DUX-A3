using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    public Button button;         // 按钮引用
    public Image buttonImage;     // 按钮的图像
    public Sprite normalSprite;   // 正常状态下的图像
    public Sprite clickedSprite;  // 点击后的图像

    void Start()
    {
        // 初始化按钮的点击事件
        button.onClick.AddListener(OnButtonClick);
    }

    // 按钮点击时调用的方法
    void OnButtonClick()
    {
        // 更换按钮的图像
        if (buttonImage.sprite == normalSprite)
        {
            buttonImage.sprite = clickedSprite;
        }
        else
        {
            buttonImage.sprite = normalSprite;
        }
    }
}
