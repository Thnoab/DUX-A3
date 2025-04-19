using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer References")]
    public AudioMixer globalMixer;

    // 参数名称必须与Audio Mixer中的Exposed Parameters一致
    private const string MASTER_VOL = "Master";
    private const string BGM_VOL = "BGM";
    private const string SFX_VOL = "SFX";

    private bool isMuted = false;  // 当前场景的静音状态
    private float previousMasterVolume = 1f;  // 保存静音前的音量
    private float previousBGMVolume = 1f;
    private float previousSFXVolume = 1f;

    void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 主音量控制
    public void SetMasterVolume(float volume)
    {
        if (!isMuted)
        {
            globalMixer.SetFloat(MASTER_VOL, VolumeToDecibel(volume));
            PlayerPrefs.SetFloat(MASTER_VOL, volume);
        }
        previousMasterVolume = volume;
    }

    // 背景音乐音量控制
    public void SetBGMVolume(float volume)
    {
        if (!isMuted)
        {
            globalMixer.SetFloat(BGM_VOL, VolumeToDecibel(volume));
            PlayerPrefs.SetFloat(BGM_VOL, volume);
        }
        previousBGMVolume = volume;
    }

    // 音效音量控制
    public void SetSFXVolume(float volume)
    {
        if (!isMuted)
        {
            globalMixer.SetFloat(SFX_VOL, VolumeToDecibel(volume));
            PlayerPrefs.SetFloat(SFX_VOL, volume);
        }
        previousSFXVolume = volume;
    }

    // 音量转分贝公式
    private float VolumeToDecibel(float volume)
    {
        return volume <= 0 ? -80f : Mathf.Log10(volume) * 20f;
    }

    // 加载保存的音量设置
    private void LoadVolumeSettings()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOL, 1f));
        SetBGMVolume(PlayerPrefs.GetFloat(BGM_VOL, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOL, 1f));

        // 恢复静音状态
        isMuted = PlayerPrefs.GetInt("SceneMute", 0) == 1;
        if (isMuted)
        {
            MuteAudio();
        }
    }

    // 切换静音状态
    public void ToggleMute()
    {
        isMuted = !isMuted;

        // 保存静音状态
        PlayerPrefs.SetInt("SceneMute", isMuted ? 1 : 0);

        if (isMuted)
        {
            MuteAudio();
        }
        else
        {
            RestoreAudio();
        }
    }

    // 静音音频
    private void MuteAudio()
    {
        globalMixer.SetFloat(MASTER_VOL, VolumeToDecibel(0));  // 设置音量为0
        globalMixer.SetFloat(BGM_VOL, VolumeToDecibel(0));
        globalMixer.SetFloat(SFX_VOL, VolumeToDecibel(0));
    }

    // 恢复音量
    private void RestoreAudio()
    {
        globalMixer.SetFloat(MASTER_VOL, VolumeToDecibel(previousMasterVolume));
        globalMixer.SetFloat(BGM_VOL, VolumeToDecibel(previousBGMVolume));
        globalMixer.SetFloat(SFX_VOL, VolumeToDecibel(previousSFXVolume));
    }
}
