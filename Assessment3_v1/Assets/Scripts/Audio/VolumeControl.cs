using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // 初始化滑动条位置（加载保存的值）
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);

        // 动态绑定事件
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}